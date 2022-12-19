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
        public string TreinNaam { get; set; }
        public List<Reiziger> Reizigers { get; set; } = new List<Reiziger>();
    }
}
