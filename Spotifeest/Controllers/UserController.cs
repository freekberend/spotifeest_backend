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

        [HttpPost("slarechisop/{token}/{feestcode}")]
        public void slarechisop(string token, string feestCode, [FromBody] RHDTO rhdto)
        {
            User gevondenUser;
            try
            {
                gevondenUser = _mdc.users.Where(u => u.Token.Equals(token)).Single();
            }
            catch (Exception ex)
            {
                return;
            }
            Debug.WriteLine("IN POST FELIX");
            try
            {
                Party gevondenParty = _mdc.parties.Where(p => p.FeestCode.Equals(feestCode)).Single();
            }
            catch (Exception ex)
            {
                return;
            }
            // binnen een if om te controleren of gebruiker bestaat
            // User gevondenUser = _mdc.users.Where(u => u.Token.Equals(token)).Single();
            // Debug.WriteLine(gevondenUser.Email);
            RecommendationHistory rh = new RecommendationHistory();
            rh.Artist = rhdto.Artist;
            rh.Keuze = rhdto.Keuze;
            rh.Spotifytrackid = rhdto.Spotifytrackid;
            rh.Track = rhdto.Track;
            // moet nog gevalideerd worden
            rh.feestCode = feestCode;
            rh.Eigenaar = gevondenUser;
            _mdc.recommendationhistories.Add(rh);
            _mdc.SaveChanges();
            
           
        }
        [HttpGet("allerh/{userid}")]
        public IEnumerable<RecommendationHistory> allerh(int userid) {      
            // filter allen van de user eruit

            return _mdc.recommendationhistories;
        }

        [HttpGet("groeprecs/{feesttoken}")]
        public IEnumerable<RecommendationHistory> groeprecs(string feesttoken)
        {

            // filter allen van de user eruit

            return _mdc.recommendationhistories.Where(p => p.feestCode.Equals(feesttoken));
        }

        [HttpGet("userrecs/{usertoken}/{feesttoken}")]
        public IEnumerable<RecommendationHistory> userrecs(string usertoken, string feesttoken)
        {
            User gevondenUser;
            try
            {
                gevondenUser = _mdc.users.Where(u => u.Token.Equals(usertoken)).Single();
                return _mdc.recommendationhistories.Where(p => p.Eigenaar.Equals(gevondenUser)).Where(p => p.feestCode.Equals(feesttoken));
            }
            catch (Exception ex)
            {
                Console.WriteLine("een fout");
                // return "er is een fout";
                return null;
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
