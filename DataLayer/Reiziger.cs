using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Reiziger
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public List<Trein> Treinen { get; set;} = new List<Trein>();

    }
}
