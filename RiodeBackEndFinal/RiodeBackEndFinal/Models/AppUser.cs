using Microsoft.AspNetCore.Identity;

namespace RiodeBackEndFinal.Models
{
    public class AppUser :IdentityUser
    {
        public string FirstName { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<UserBasket> UserBaskets { get; set; }
    }
}

