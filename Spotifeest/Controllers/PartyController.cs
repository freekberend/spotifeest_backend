using DataLayer;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
