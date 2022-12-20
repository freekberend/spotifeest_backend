using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

        public virtual ICollection<Party> Parties { get; set; }
        public virtual ICollection<Preference> Preferences { get; set; }

        public User()
        {
            this.Parties = new HashSet<Party>();
        }

    }
}
