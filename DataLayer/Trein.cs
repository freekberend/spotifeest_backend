using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Trein
    {
        public int Id { get; set; }
        public string Conducteur { get; set; }
        public int Snelheid { get; set; }
        public string ThuisStation { get; set; }
    }
}
