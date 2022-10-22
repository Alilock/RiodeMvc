using Microsoft.AspNetCore.Identity;

namespace RiodeBackEndFinal.Models
{
    public class AppUser :IdentityUser
    {
        public string FirstName { get; set; }
    }
}
