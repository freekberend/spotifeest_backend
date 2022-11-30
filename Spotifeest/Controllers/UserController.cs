using DataLayer;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Spotifeest.Controllers
{
    [Route("")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private DatabaseContext _mdc;

        public UserController(DatabaseContext mdc)
        {
            _mdc = mdc;
        }
        // GET: api/User
        [EnableCors]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [EnableCors]
        [Route("createuser")]
        [HttpPost]
        public User Post([FromBody] User user)
        {
            _mdc.Add(user);
            _mdc.SaveChanges();
            return user;
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
