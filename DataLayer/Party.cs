using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Party
    {
        public int Id { get; set; }
        public PartyPreference FeestVoorkeur { get; set; }
        public string FeestCode { get; set; }
        public string FeestNaam { get; set; }
        public List<User> FeestVisitors
        {
            get
            {
                return FeestVisitors;
            }
            set
            {
                FeestVisitors = value;
            }
        }
        public User FeestOwner { get; set; }
        public List<Song> Playlist
        {
            get
            {
                return Playlist;
            }
            set
            {
                Playlist = value;
            }
        }



    }
}
