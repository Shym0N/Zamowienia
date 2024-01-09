﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zamowienia.Models;
using Zamowienia.ViewModels;
using System;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

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

        // AccountController.cs

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
                    string username = GenerateUsername(model.Imie, model.Nazwisko);
                    var user = new ApplicationUser
                    {
                        UserName = username,
                        Email = model.Email,
                        Imie = model.Imie,
                        Nazwisko = model.Nazwisko,
                        TypUzytkownika = string.IsNullOrEmpty(model.TypUzytkownika) ? "Użytkownik" : model.TypUzytkownika
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        //await _signInManager.SignInAsync(user, isPersistent: false);
                        //return RedirectToAction("Login", "Account");
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
                // Dodaj logowanie błędów
                Console.WriteLine($"Wystąpił błąd podczas rejestracji: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Wystąpił błąd podczas rejestracji.");
                return View("Registration", model);
            }
        }
        private string GenerateUsername(string firstName, string lastName)
        {
            string firstPart = firstName.Length > 2 ? firstName.Substring(0, 3) : firstName;
            string secondPart = lastName.Length > 2 ? lastName.Substring(0, 3) : lastName;
            return (firstPart + secondPart).ToLower();
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
            return RedirectToAction(nameof(HomeController.Index), "Home");
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
/*
    public class LoginViewModel
    {
        [Required]
        public string EmailOrUsername { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }*/
}