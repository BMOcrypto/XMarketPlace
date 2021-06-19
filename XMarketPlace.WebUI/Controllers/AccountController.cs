using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using XMarketPlace.Core.Service;
using XMarketPlace.Model.Entities;

namespace XMarketPlace.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICoreService<User> _us;

        public AccountController(ICoreService<User> us)
        {
            _us = us;
        }
        
        public IActionResult Login() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User item)
        {
            if (_us.Any(x => x.EmailAddress == item.EmailAddress && x.Password == item.Password))
            {
                var logged = _us.GetByDefault(x => x.EmailAddress == item.EmailAddress && x.Password == item.Password);

                var claims = new List<Claim>()
                {
                    new Claim("ID", logged.ID.ToString()),
                    new Claim(ClaimTypes.Name, logged.FirstName),
                    new Claim(ClaimTypes.Surname, logged.LastName),
                    new Claim(ClaimTypes.Email, logged.EmailAddress),
                    new Claim("Image", logged.ImageUrl)
                };

                var userIdentity = new ClaimsIdentity(claims, "login"); 

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity); 

                await HttpContext.SignInAsync(principal); 

                return RedirectToAction("Index", "Home", new { area = "Administrator" }); 
            }

            return View(item);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(); 

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(User user)
        {

            _us.Add(user);
            _us.Save();


            return RedirectToAction("Login", "Account");
        }
    }
}
