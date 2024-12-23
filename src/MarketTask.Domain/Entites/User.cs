using Microsoft.AspNetCore.Identity;

namespace MarketTask.Domain.Entites{
    public class User:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public string Role { get; set; }

        internal static object FindFirst(string nameIdentifier)
        {
            throw new NotImplementedException();
        }
    }
}
