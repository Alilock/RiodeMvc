using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RiodeBackEndFinal.Models;
using RiodeBackEndFinal.ViewModels;

namespace RiodeBackEndFinal.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<AppUser> UserManager { get; }

        public AccountController(UserManager<AppUser> userManager)
        {
            UserManager = userManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public  async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = new()
            {
                FirstName = register.FirstName,
                UserName = register.Username,
                Email = register.Email,

            };
            IdentityResult result = await UserManager.CreateAsync(user);

            return Json("ok");
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}
