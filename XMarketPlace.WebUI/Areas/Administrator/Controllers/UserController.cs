using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XMarketPlace.Core.Service;
using XMarketPlace.Model.Entities;
using XMarketPlace.WebUI.Areas.Administrator.Models;

namespace XMarketPlace.WebUI.Areas.Administrator.Controllers
{
    [Area("Administrator"), Authorize]
    public class UserController : Controller
    {
        private IHostingEnvironment _env;
        private readonly ICoreService<User> _us;

        public UserController(ICoreService<User> us, IHostingEnvironment env)
        {
            _us = us;
            _env = env;
        }

        public IActionResult Index()
        {
            return View(_us.GetAll());
        }

        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insert(User user, List<IFormFile> files)
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

                bool result = _us.Add(user);
                if (result)
                {
                    // true
                    return RedirectToAction("Index");
                }
                else
                {
                    // false
                    TempData["Message"] = "Kayıt esnasında bilinmeyen bir hata oluştu";
                }
            }
            else
            {
                TempData["Message"] = "Hata oluştu. Lütfen daha sonra tekrar deneyin";
            }

            return View(user);
        }

        public IActionResult Update(Guid id)
        {
            return View(_us.GetById(id));
        }

        [HttpPost]
        public IActionResult Update(User user)
        {
            if (ModelState.IsValid)
            {
                var updated = _us.GetById(user.ID);
                updated.FirstName = user.FirstName;
                updated.LastName = user.LastName;
                updated.Title = user.Title;
                updated.EmailAddress = user.EmailAddress;

                updated.ImageUrl = user.ImageUrl;
                updated.Address = user.Address;
                updated.PhoneNumber = user.PhoneNumber;
                updated.Password = user.Password;

                bool result = _us.Update(updated);
                if (result)
                {
                    // true ise
                    return RedirectToAction("Index");
                }
                else
                {
                    // false ise
                    TempData["Message"] = "Güncelleme esnasında bilinmeyen bir hata oluştu";
                }
            }
            else
            {
                TempData["Message"] = "Hata oluştu. Lütfen daha sonra tekrar deneyin";
            }


            return View(user);
        }

        public IActionResult Delete(Guid id)
        {
            bool result = _us.Remove(_us.GetById(id));
            if (result)
            {
                // true ise
                return RedirectToAction("Index");
            }
            else
            {
                // false ise
                TempData["Message"] = "Silme esnasında bilinmeyen bir hata oluştu";
            }

            return View();
        }

        public IActionResult Activate(Guid id)
        {
            bool result = _us.Activate(id);
            if (result)
            {
                // true ise
                return RedirectToAction("Index");
            }
            else
            {
                // false ise
                TempData["Message"] = "Aktivasyon esnasında bilinmeyen bir hata oluştu";
            }

            return View();
        }
    }
}
