using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RiodeBackEndFinal.DAL;
using RiodeBackEndFinal.Models;
using RiodeBackEndFinal.Utlis.Constant;
using RiodeBackEndFinal.ViewModels;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WebMatrix.WebData;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace RiodeBackEndFinal.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<AppUser> UserManager { get; }
        public SignInManager<AppUser> SignInManager { get; }
        public RiodeContext RiodeContext { get; }

        public AccountController(UserManager<AppUser> userManager,  
                                 SignInManager<AppUser> signInManager,
                                 RiodeContext riodeContext)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RiodeContext = riodeContext;
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
                var confirmLink = Url.Action("ConfirmEmail", "Account", new { token, email = user.Email }, HttpContext.Request.Scheme);
                string body = "Thanks for join our family</br>" + $"please <a href='{confirmLink}'>confirm</a> your account";
                EmailHelper emailHelper = new();
                emailHelper.SendEmail(user.Email, body,"Confirm Your Email");
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
                    return View();
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> LogOut()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }

        //send confirm email
        public async void SendConfirmEmail(AppUser user)
        {
            var token = await UserManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmLink = Url.Action("ConfirmEmail", "Account", new { token, email = user.Email }, HttpContext.Request.Scheme);
            EmailHelper emailHelper = new();
            emailHelper.SendEmail(user.Email, confirmLink, "Confirm Your Email");
        }
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await UserManager.FindByEmailAsync(email);
            if (user is null) return NotFound();
            await SignInManager.SignInAsync(user, true);
            await UserManager.ConfirmEmailAsync(user, token);
            return View();
        }
        public IActionResult ForgotPass()
        {
            return  View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPass(ForgotPassVM forgotPassVM)
        {
            if (!ModelState.IsValid) return View();
            var user= await UserManager.FindByNameAsync(forgotPassVM.UserName);
            
            if (user is null)
            {
                ModelState.AddModelError("UserName", "This user can't be found");
                return View();
            }
            ResetPasswordCode passwordToken = new ResetPasswordCode(user.UserName);
            await RiodeContext.ResetPasswordCodes.AddAsync(passwordToken);
            await RiodeContext.SaveChangesAsync();
            var emailcontent = "Reset your password with this link: " +passwordToken.Code;
            EmailHelper email = new();
            email.SendEmail(user.Email, emailcontent,"Reset Your Password");
            return RedirectToAction("ConfirmPassword", "Account");
        }
        public IActionResult ConfirmPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmPassword(string resetToken)
        {
            if (String.IsNullOrWhiteSpace(resetToken))
            {
                ModelState.AddModelError("resetToken", "Write the confirm code");
                return View();
            }
            var resetPassword = RiodeContext.ResetPasswordCodes.Where(r => r.Code == resetToken && r.ExpireTime > TimeSpan.Zero).FirstOrDefault();
            if (resetPassword is null)
            {
                ModelState.AddModelError("resetToken", "code is not correct");
                return View();
            }
            var user = RiodeContext.Users.Where(u => u.UserName == resetPassword.UserName).SingleOrDefault();
           await SignInManager.SignInAsync(user, true);
            return RedirectToAction("ResetPassword", "Account");   
        }
        public IActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM reset)
        {
            if (!ModelState.IsValid) return View();

            var username = User.Identity.Name;
            var user = await UserManager.FindByNameAsync(username);
            var token = await UserManager.GeneratePasswordResetTokenAsync(user);
            await UserManager.ResetPasswordAsync(user, token, reset.NewPassword);
            return RedirectToAction("Login", "Account");
        }
    }

}
