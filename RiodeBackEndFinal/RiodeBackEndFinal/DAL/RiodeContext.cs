using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RiodeBackEndFinal.Models;

namespace RiodeBackEndFinal.DAL
{
    public class RiodeContext :IdentityDbContext<AppUser>
    {
        public RiodeContext(DbContextOptions<RiodeContext> opt):base(opt)
        {

        }
       public DbSet<Setting> Settings { get; set; }
       public DbSet<Slider> Sliders { get; set; }
       public DbSet<Badge> Badges { get; set; }
       public DbSet<Product> Products { get; set; }
       public DbSet<ProductBadges> ProductBadges { get; set; }
       public DbSet<ProductImages> ProductImages { get; set; }
       public DbSet<Category> Categories { get; set; }
       public DbSet<Color> Colors { get; set; }
       public DbSet<ProductColors> ProductColors { get; set; }
       public DbSet<ResetPasswordCode> ResetPasswordCodes { get; set; } 
    }
}
