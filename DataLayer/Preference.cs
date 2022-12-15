using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Preference
    {
        public int Id { get; set; }

        public Party Feest { get; set; }
        public string Nummer1 { get; set; }
        public string Nummer2 { get; set;}
        public string Nummer3 { get; set;}
        public string Oorsprong1 { get; set; }
        public string Oorsprong2 { get; set; }
        public string Oorsprong3 { get; set; }
        public virtual ICollection<User> Users { get; set; }

    }
}
