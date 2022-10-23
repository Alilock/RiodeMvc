using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RiodeBackEndFinal.Models;
using RiodeBackEndFinal.Utlis.Constant;
using RiodeBackEndFinal.ViewModels;
using System.Net.Mail;
using System.Runtime.InteropServices;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace RiodeBackEndFinal.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<AppUser> UserManager { get; }
        public SignInManager<AppUser> SignInManager { get; }

        public AccountController(UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");

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
            IdentityResult result = await UserManager.CreateAsync(user,register.Password);
            if (result.Succeeded)
            {
                var token = await UserManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmLink = Url.Action("ConfirmEmail","Account", new { token, email = user.Email }, HttpContext.Request.Scheme);
                EmailHelper emailHelper = new ();
                emailHelper.SendEmail(user.Email, confirmLink);
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    return View();
                }
            }

            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View();  

            AppUser user = await UserManager.FindByNameAsync(loginVM.UserName);
            if (user ==null)
            {
                ModelState.AddModelError("", "Username or password is wrong");
                return View();
            }
            if (user.EmailConfirmed is false)
            {
                ModelState.AddModelError("", "please, Confirm Your Email");
                return View();
            }
            SignInResult result = await SignInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, true);
            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "You trying many times. Please wait about" + $"{user.LockoutEnd} minutes");
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "Username or password is wrong");
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> LogOut()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await UserManager.FindByEmailAsync(email);
            if (user is null) return NotFound();
            await SignInManager.SignInAsync(user, true);
            await UserManager.ConfirmEmailAsync(user, token);
            return View();
        }

    }
}
