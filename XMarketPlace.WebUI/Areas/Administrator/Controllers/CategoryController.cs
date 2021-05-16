using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XMarketPlace.Core.Service;
using XMarketPlace.Model.Entities;

namespace XMarketPlace.WebUI.Areas.Administrator.Controllers
{
    [Area("Administrator"), Authorize]
    public class CategoryController : Controller
    {
        private readonly ICoreService<Category> _cs;

        public CategoryController(ICoreService<Category> cs)
        {
            _cs = cs;
        }
        public IActionResult Index()
        {
            return View(_cs.GetAll());
        }

        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insert(Category category)
        {
            if (ModelState.IsValid)
            {
                bool result = _cs.Add(category);
                if (result)
                {
                    // true ise
                    return RedirectToAction("Index");
                }
                else
                {
                    // false ise
                    TempData["Message"] = "Kayıt esnasında bilinmeyen bir hata oluştu";
                }
            }
            else
            {
                TempData["Message"] = "İşlem başarısız oldu. Lütfen daha sonra tekrar deneyin";
            }

            return View(category);
        }

        public IActionResult Update(Guid id)
        {
            return View(_cs.GetById(id));
        }

        [HttpPost]
        public IActionResult Update(Category category)
        {
            if (ModelState.IsValid)
            {
                var updated = _cs.GetById(category.ID);
                updated.CategoryName = category.CategoryName;
                updated.Description = category.Description;

                bool result = _cs.Update(updated);
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
                TempData["Message"] = "İşlem başarısız oldu. Lütfen daha sonra tekrar deneyin";
            }

            return View();
        }

        public IActionResult Delete(Guid id)
        {
            bool result = _cs.Remove(_cs.GetById(id));
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
            bool result = _cs.Activate(id);
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
