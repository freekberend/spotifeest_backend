using DataLayer;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;

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

        // POST API / Register User
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

        // POST API / Login User
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

        [HttpPost("slarechisop/{userid}")]
        public void slarechisop(int userid, [FromBody] RHDTO rhdto)
        {   
            Debug.WriteLine("IN POST FELIX");
            User gevondenUser = _mdc.users.Where(u => u.Id.Equals(userid)).Single();
            Debug.WriteLine(gevondenUser.Email);
            RecommendationHistory rh = new RecommendationHistory();
            rh.Artist = rhdto.Artist;
            rh.Keuze = rhdto.Keuze;
            rh.Spotifytrackid = rhdto.Spotifytrackid;
            rh.Track = rhdto.Track;
            rh.Jsonstring = rhdto.Jsonstring;
            rh.Eigenaar = gevondenUser;
            _mdc.recommendationhistories.Add(rh);
            _mdc.SaveChanges();
            
           
        }
        [HttpGet("allerh/{userid}")]
        public IEnumerable<RecommendationHistory> allerh(int userid) {      
            // filter allen van de user eruit

            return _mdc.recommendationhistories;
        }
        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
