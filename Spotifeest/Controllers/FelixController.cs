using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Spotifeest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FelixController : ControllerBase
    {

        private DatabaseContext _partydbContext;

        public FelixController(DatabaseContext partydbContext)
        {
            _partydbContext = partydbContext;   
        }


        // GET: api/<FelixController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            List<Trein> ttemp = _partydbContext.trains.Include("Reizigers").Where(t=>t.TreinNaam.Equals("rood")).ToList();
            Debug.WriteLine(">1>"+ttemp.Count());
            Debug.WriteLine(">1>"+ttemp.First().TreinNaam);
            Debug.WriteLine(">1>"+ttemp.First().Reizigers.Count());
 //           var trainss = (from t in _partydbContext.trains.Include("Reizigers")
 //               where t.TreinNaam == "rood"
 //               select t).FirstOrDefault<Trein>();
 //           Debug.WriteLine(">DIT IS M>:"+trainss.Reizigers.Count());
            return new string[] { "value1", "value2" };
        }

        // GET api/<FelixController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FelixController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FelixController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FelixController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
