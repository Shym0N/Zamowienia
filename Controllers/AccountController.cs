using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zamowienia.Models;
using Zamowienia.ViewModels;
using System;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Zamowienia.Attributes;

namespace Zamowienia.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        
       

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Rejestracja()
        {
            return View("Registration", new RegistrationViewModel());
        }

     
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Rejestracja(RegistrationViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var user = new ApplicationUser
                    {
                        UserName = model.UserName,
                        Email = model.Email,
                        Imie = model.Imie,
                        Nazwisko = model.Nazwisko,
                        TypUzytkownika = string.IsNullOrEmpty(model.TypUzytkownika) ? "Użytkownik" : model.TypUzytkownika
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                return View("Registration", model);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd podczas rejestracji: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Wystąpił błąd podczas rejestracji.");
                return View("Registration", model);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = null;
                if (model.EmailOrUsername.Contains("@"))
                {
                    user = await _userManager.FindByEmailAsync(model.EmailOrUsername);
                }
                else
                {
                    user = await _userManager.FindByNameAsync(model.EmailOrUsername);
                }

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        return RedirectToLocal(returnUrl);
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}