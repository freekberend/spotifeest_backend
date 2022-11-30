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
            return _partydbContext.party;
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
