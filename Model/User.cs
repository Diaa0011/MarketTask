using Microsoft.AspNetCore.Identity;

namespace Market.Model
{
    public class User:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public string Role { get; set; }

        internal static object FindFirst(object nameIdentifier)
        {
            throw new NotImplementedException();
        }
    }
}
