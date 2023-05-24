using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectMVC.PL.Helpers;
using ProjectMVC.PL.ViewModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ProjectMVC.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
            if(ModelState.IsValid)
            {
                var MappedUser = new ApplicationUser()
                {
                    Fname = model.Fname,
                    Lname = model.Lname,
                    Email = model.Email,
                    UserName = model.Email.Split('@')[0],
                    IsAgree = model.IsAgree
                };
               var Result = await _userManager.CreateAsync(MappedUser,model.Password);
                if (Result.Succeeded)
                    return RedirectToAction(nameof(Login));
                foreach (var error in Result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

            }
			return View(model);
		}

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                    if(user is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (flag)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                        if (result.Succeeded)
                            return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError(string.Empty, "Passwrod is not correct");
                }
                ModelState.AddModelError(string.Empty, "Email Is n't found");
            }
            return View(model);
        }

        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user is not null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var PasswordResetLink = Url.Action("ResetPassword","Account", new { email= user.Email , token},Request.Scheme);
                    var email = new Email()
                    {
                        Subject = "Reset Password",
                        Body = "Password Reset Link",
                        To = user.Email
                    };
                    EmailSend.SendEmail(email);
                    return RedirectToAction(nameof(CheckurInbox));
                }
                ModelState.AddModelError(string.Empty, "Email not found");
            }
            return View();
        }
        public IActionResult CheckurInbox()
        {
            return View();
        }
	}
}
