using DataLayer;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.IO;

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
            return new string[] { "value1", "value2", "value3", "value4" };
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
            TokenGenerator utg = new TokenGenerator();
            string token = utg.Main();

            IEnumerable<User> test = _mdc.users;

            foreach (User u in test)
            {
                if (u.Token.Equals(token))
                {
                    token = utg.Main();
                }
                else
                {
                    user.Token = token;
                }
            }
            _mdc.Add(user);
            _mdc.SaveChanges();
            return user;
        }

        // POST api/<UserController>
        [EnableCors]
        [Route("loginuser")]
        [HttpPost]
        public User LoginPost([FromBody] User user)
        {
            try
            {
                User gevondenUser = _mdc.users.Where(u => u.Email.Equals(user.Email)).Where(u => u.Password.Equals(user.Password)).Single();
                return gevondenUser;
            }
            catch (Exception ex)
            {
                return null;
            }
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
