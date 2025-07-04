using Basics.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace Basics.Controllers
{
    
    public class AdminController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var claims = new List<Claim>() { new Claim("Damo", "Value") };
            var claimsIdentity = new ClaimsIdentity(claims, "Cookie");
            var claimPrincipal = new ClaimsPrincipal(claimsIdentity);
            HttpContext.SignInAsync("Cookie", claimPrincipal);
            return Redirect(model.ReturnUrl);
        }

        public IActionResult LogOff()
        {
            HttpContext.SignOutAsync("Cookie");
            return Redirect("/Home/Index");
        }
    }
}
