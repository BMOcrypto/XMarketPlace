using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XMarketPlace.Core.Service;
using XMarketPlace.Model.Entities;
using XMarketPlace.WebUI.Areas.Administrator.Models;

namespace XMarketPlace.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IHostingEnvironment _env;
        private readonly ICoreService<User> _us;

        public AccountController(ICoreService<User> us, IHostingEnvironment env)
        {
            _us = us;
            _env = env;
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
                    new Claim("Image", logged.ImageUrl),
                    new Claim(ClaimTypes.Role,logged.Title) // yetki kısıtlaması yapabilmek için
                };

                var userIdentity = new ClaimsIdentity(claims, "login"); 

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity); 

                await HttpContext.SignInAsync(principal);

                if (logged.Title!="standard")
                {
                    return RedirectToAction("Index", "Home", new { area = "Administrator" });
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

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
        public IActionResult SignUp(User user, List<IFormFile> files)
        {

            if (ModelState.IsValid)
            {
                bool imgResult;
                string imgPath = Upload.ImageUpload(files, _env, out imgResult);

                if (imgResult)
                {
                    user.ImageUrl = imgPath;
                }
                else
                {
                    TempData["Message"] = "Resim yükleme esnasında hata oluştu";
                }
                _us.Add(user);
                _us.Save();
            }
            else
            {
                TempData["Message"] = "Hata oluştu. Lütfen daha sonra tekrar deneyin";
            }



            return RedirectToAction("Login", "Account");
        }
    }
}
