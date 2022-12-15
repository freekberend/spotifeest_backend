using DataLayer;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.IO;

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



        // GET api/<PartyController>/5
        [EnableCors]
        [HttpGet("{userid}/{partyid}")]
        public string ZoekParty(int partyid, int userid)
        {
            string teruggeven = "-";
            IEnumerable<Party> feestjeslokaal = _partydbContext.parties;
            Party party = null;
            foreach (Party feestjeloc in feestjeslokaal)
            {
                if (feestjeloc.Id.Equals(partyid)) //Als de u.ID gelijk is aan de meegegeven ID)
                {
                    teruggeven += feestjeloc.Id + "-";
                    party = feestjeloc;
                    //foreach loop waar gezocht word in de lijst met alle users, om te kijken of de user al bestaat
                    IEnumerable<User> userslokaal = _partydbContext.users;
                    foreach (User userloc in userslokaal)
                    {
                        if (userloc.Id.Equals(userid))
                        {
                            teruggeven += userloc.Id + "-";
                            feestjeloc.Users.Add(userloc);
                            //voeg toe aan lijst
                        }
                        else
                        {                          
                        }
                        //error message 'user not found'
                    }

                    /*DataLayer.User user = new DataLayer.User();
                    user.Username = "Abc";
                    user.Password = "123";
                    user.Email = "abc@gmail.com";
                    user.Token = "eojrqweueqowiejqo";*/

                    //_partydbContext.Add(feestjeloc);
                    
                    //return "Party bestaat wel";
                    //return wel als string
                }
                else
                {
                    //return "Party bestaat niet";
                    //return niet als string
                }
            }
            _partydbContext.SaveChanges();
            return teruggeven;
        }

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
