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
        public string Genre1 { get; set; }
        public string Genre2 { get; set;}
        public string Genre3 { get; set;}

    }
}
