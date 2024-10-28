using Microsoft.AspNetCore.Identity;

namespace Market.Model
{
    public class User:IdentityUser
    {
        public string Role { get; set; }
    }
}
