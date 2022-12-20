using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Party
    {
        public int Id { get; set; }
        public string FeestCode { get; set; }
        public string FeestNaam { get; set; }
        public Boolean Closed { get; set; }
        /*public List<User> FeestVisitors
        {
            get
            {
                return FeestVisitors;
            }
            set
            {
                FeestVisitors = value;
            }
        }*/
        public string FeestOwner { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public Party()
        {
            this.Users = new HashSet<User>();
        }
        //public string Playlist
    }
}
