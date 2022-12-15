using DataLayer;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.IO;

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

        // GET API / Koppel User aan Party
        [EnableCors]
        [HttpGet("{userid}/{partyid}")]
        public string ZoekParty(int partyid, int userid)
        {
            string teruggeven = "-";
            IEnumerable<Party> feestjeslokaal = _partydbContext.parties;
            Party party = null;
            foreach (Party feestjeloc in feestjeslokaal)
            {
                if (feestjeloc.Id.Equals(partyid)) //Foreach Loop check op Party
                {
                    teruggeven += feestjeloc.Id + "-";
                    party = feestjeloc;
                    IEnumerable<User> userslokaal = _partydbContext.users;
                    foreach (User userloc in userslokaal)
                    {
                        if (userloc.Id.Equals(userid)) //Foreach Loop check op User
                        {
                            teruggeven += userloc.Id + "-";
                            feestjeloc.Users.Add(userloc);
                        }
                        else
                        {                          
                        }
                    }
                }
                else
                {
                }
            }
            _partydbContext.SaveChanges();
            return teruggeven;
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
    }
}
