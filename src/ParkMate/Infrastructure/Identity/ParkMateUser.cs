using Microsoft.AspNetCore.Identity;

namespace ParkMate.Infrastructure.Identity
{
    public class ParkMateUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
