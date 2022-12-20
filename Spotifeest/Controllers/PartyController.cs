using DataLayer;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Spotifeest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartyController : ControllerBase
    {
        private DatabaseContext _partydbContext;

        public PartyController(DatabaseContext partydbContext)
        {
            _partydbContext = partydbContext;   
        }
        // GET: api/<PartyController>
        [EnableCors]
        [HttpGet]
        public IEnumerable<Party> Get()
        {
            return _partydbContext.parties;
        }


        [EnableCors]
        [HttpGet("{feestcode}/{token}")]
        public string ZoekParty2(string feestcode, string token)
        {
            string foutzoeker = "";
            //IEnumerable<User> userToCheck = _partydbContext.users.Include("Parties").Where(u => u.Id.Equals(4));
            //Debug.WriteLine("TESTPUNT: " + userToCheck.First().Parties.Count());

            // Let op: Include("Users") is nodig omdat EF anders de inhoud achter de gelegde relation niet legt
            // Bij het achterwege laten zal een gevraagd record leeg terugkomen.
            // zie: https://learn.microsoft.com/en-us/ef/core/querying/related-data/
            IEnumerable<Party> doesPartyExist = _partydbContext.parties.Include("Users").Where(p => p.FeestCode.Equals(feestcode));

            if (doesPartyExist.Count() == 1) {
                // aangenomen dat de party-code uniek is, kan de Party geselecteerd worden met First()
                Party huidigFeestje = doesPartyExist.First();

                // Controleer of de gegeven gebruiker(stoken) aanwezig is in de users-lijst van de gegeven party
                IEnumerable<User> userInFeestje = huidigFeestje.Users.Where(u => u.Token.Equals(token));
                //Debug.WriteLine("HIER: " + userInFeestje.Count());

                // vanwege de many-to-many relatie kan het party object niet 'gewoon' als json-object teruggegeven worden.
                // zonder onderstaande opties schiet JsonSerializer in een cirkel/oneindige loop vast.
                // meer informatie: https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/preserve-references?pivots=dotnet-6-0
                // hier geplaatst omdat de opties hieronder op twee plekken gebruikt worden, maar het mag niet op top-niveau.
                JsonSerializerOptions options = new()
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };

                if (userInFeestje.Count() == 1) {
                    //foutzoeker += "gebruiker zit al in het feest";
                    // als de gebruiker al in het feestje zit, kunnen we het feestje teruggeven zonder verdere toevoegingen.
                    return JsonSerializer.Serialize(huidigFeestje, options);
                }
                else
                {
                    //check of de gegeven token valide is, door deze als record op te vragen uit de db-context
                    IEnumerable<User> userToAdd = _partydbContext.users.Where(u => u.Token.Equals(token));
                    if (userToAdd.Count() == 1)
                    {
                        // voeg de gebruiker uit de aanroep toe aan het feestje
                        huidigFeestje.Users.Add(userToAdd.First());
                        _partydbContext.SaveChanges();
                        //foutzoeker += "gebruiker toegevoegd";
                        // geef het ge-updatete feestje terug, met de gebruiker toegevoegd.
                        return JsonSerializer.Serialize(huidigFeestje, options);
                    }
                    else
                    {
                        foutzoeker += "gebruiker(stoken) bestaat niet";
                    }
                }
            
            }
            else
            {
                foutzoeker += "dit feest bestaat niet";
            }
            Console.WriteLine("Foutzoeker: " + foutzoeker);
            // return een zelfgeformatteerd JSON-object met fout
            return "{\"Fout\": \""+ foutzoeker + "\"}";
        }
/*
        // GET api/<PartyController>/5
        [EnableCors]
        [HttpGet("{feestcode}/{token}")]
        public string ZoekParty(string feestcode, string token)
        {
            Console.WriteLine(feestcode);
            Console.WriteLine(token);
            string teruggeven = "-";
            IEnumerable<Party> feestjeslokaal = _partydbContext.parties;
            // Party party = null;
            foreach (Party feestjeloc in feestjeslokaal)
            {
                Console.WriteLine("FeestID: ");
                Console.WriteLine(feestjeloc.FeestCode);
                if (feestjeloc.FeestCode.Equals(feestcode)) //Als de u.ID gelijk is aan de meegegeven ID)
                {
                    teruggeven += feestjeloc.Id + "{{-";
                    // party = feestjeloc;
                    //foreach loop waar gezocht word in de lijst met alle users, om te kijken of de user al bestaat
                    IEnumerable<User> userslokaal = _partydbContext.users;
                    foreach (User userloc in userslokaal)
                    {
                        Console.WriteLine("UserID: ");
                        Console.WriteLine(userloc.Id);
                        if (userloc.Token.Equals(token))
                        {

                            IEnumerable<User> temp = feestjeloc.Users.Where(u => u.Id.Equals(userloc.Id));
                            if (temp.Count() != 0)
                            {
                                teruggeven += userloc.Id + "-";
                                feestjeloc.Users.Add(userloc);
                                _partydbContext.SaveChanges();
                            }
                            else
                            {
                                teruggeven += "fout";
                                return teruggeven;
                            }
            
                            //voeg toe aan lijst
                        }
                        else
                        {
                        }
                    }
                 
                }
                else
                {
                    //return "je bent al lid of de party bestaat niet";
                }
            }
            
            return teruggeven;
        }*/

        // POST api/<PartyController>
        [EnableCors]
        [Route("createparty")]
        [HttpPost]
        public Party Post([FromBody] Party party)
        {

            PartyCodeGenerator pcg = new PartyCodeGenerator();
            string code = pcg.Main();
            
            IEnumerable<Party> test = _partydbContext.parties;
            
            foreach(Party u in test)
            {
                if(u.FeestCode.Equals(code)) {
                    code = pcg.Main();
                }
                else {
                    party.FeestCode = code;
                }
            }

            _partydbContext.Add(party);
            _partydbContext.SaveChanges();
            return party;
        }

        // PUT api/<PartyController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PartyController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
