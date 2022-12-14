using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    [Keyless]
    public class UserPreference
    {
        public User user { get; set; }
        public Preference preference { get; set; }  

    }
}
