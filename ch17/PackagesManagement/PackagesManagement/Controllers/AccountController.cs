using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PackagesManagement.Models;
using PackagesManagement.Models.Account;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PackagesManagement.Controllers
{
    public class AccountController : Controller
    {
        
        [HttpGet]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            // Clear the existing  cookie 
            //to ensure a clean login process
            await HttpContext
                .SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(
            LoginViewModel model,
           string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (User?.Identity?.IsAuthenticated??false)
            {
                await HttpContext.SignOutAsync();
            }
            if (ModelState.IsValid)
            {
                
                var result = model.UserName=="admin" && model.Password=="admin";
            if (result)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, model.UserName),
                    new Claim(ClaimTypes.Role, "Administrator")
                };
                var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddDays(30),
                    IsPersistent = model.RememberMe,
                    IssuedUtc = DateTime.UtcNow,
                    AllowRefresh = true
                };
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                if (!string.IsNullOrEmpty(returnUrl))
                    return LocalRedirect(returnUrl);
                else
                    return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "wrong user name or password");
                return View(model);
            }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext
                .SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
