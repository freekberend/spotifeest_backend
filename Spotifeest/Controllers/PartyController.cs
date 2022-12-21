using DataLayer;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spotifeest.Classes;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

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

        // GET: api/<PartyController>/{feestcode}/{token}
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

        // POST API / Create a Party
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

        // GET Api/Partycontroller/
        [HttpGet("ledenlijst/{feestcode}")]
        public UsersPartyDTO Hallo(string feestcode)
        {
            Debug.WriteLine("Ik ben er");
            //geef terug alle Ledenamen (gebaseerd op Id) van
            //de huidige actieve party (wat de feestcode uit local storage is, gekoppeld aan het partyId, via de PartyUser tussentabel
            Party feestje = _partydbContext.parties.Include("Users").Where(p => p.FeestCode.Equals(feestcode)).First();
            Debug.WriteLine(feestje.Users.Count());
            UsersPartyDTO o = new UsersPartyDTO(feestje);
        return o;   


        }
    }
}
