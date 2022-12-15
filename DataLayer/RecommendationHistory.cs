using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class RecommendationHistory
    {
        public int Id { get; set; }
        public string Artist { get; set; }
        public string Track { get; set; }
        public string Spotifytrackid { get; set; }
        public string Keuze { get; set; }
        public string Jsonstring { get; set; }

        public User Eigenaar { get; set;}
    }
}
