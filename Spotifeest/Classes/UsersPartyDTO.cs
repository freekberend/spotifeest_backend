using DataLayer;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace Spotifeest.Classes
{
    public class UsersPartyDTO
    {
        public string partycode { get; set; }
        public ICollection <String> gebruikers { get; set; } = new List<String>();
public UsersPartyDTO(Party b)
        {
            partycode = b.FeestCode;
            foreach (User user in b.Users)
            {
                gebruikers.Add(user.Username);
            }
            Debug.WriteLine(b.Users.Count());
        }
    }
}
