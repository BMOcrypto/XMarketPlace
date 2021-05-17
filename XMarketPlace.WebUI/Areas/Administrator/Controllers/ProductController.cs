using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using XMarketPlace.Core.Service;
using XMarketPlace.Model.Entities;
using XMarketPlace.WebUI.Areas.Administrator.Models;

namespace XMarketPlace.WebUI.Areas.Administrator.Controllers
{
    [Area("Administrator"), Authorize]
    public class ProductController : Controller
    {
        private IHostingEnvironment _env;
        private readonly ICoreService<Product> _ps;
        private readonly ICoreService<Category> _cs;

        public ProductController(ICoreService<Product> ps, ICoreService<Category> cs, IHostingEnvironment env)
        {
            _ps = ps;
            _cs = cs;
            _env = env;
        }

        public IActionResult Index()
        {
            return View(_ps.GetAll());
        }

        public IActionResult Insert()
        {
            ViewBag.Categories = new SelectList(_cs.GetActive(), "ID", "CategoryName");
            return View();
        }

        [HttpPost]
        public IActionResult Insert(Product prdct, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                // Since Product doesn't have UserID as foreign key, below line is unnecessary
                //prdct.UserID = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "ID").Value);

                bool imgResult;
                string imgPath = Upload.ImageUpload(files, _env, out imgResult);

                if (imgResult)
                {
                    prdct.ImagePath = imgPath;
                }
                else
                {
                    TempData["Message"] = "Resim yükleme esnasında hata Oluştu";
                }

                bool result = _ps.Add(prdct);
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
                TempData["Message"] = "Hata Oluştu";
            }

            return View(prdct);
        }

        public IActionResult Update(Guid id)
        {
            ViewBag.Categories = new SelectList(_cs.GetActive(), "ID", "CategoryName");
            return View(_ps.GetById(id));
        }

        [HttpPost]
        public IActionResult Update(Product prdct)
        {
            if (ModelState.IsValid)
            {
                var updated = _ps.GetById(prdct.ID);
                updated.ProductName = prdct.ProductName;
                updated.ProductDetail = prdct.ProductDetail;
                updated.ProductSummary = prdct.ProductSummary;
                updated.UnitPrice = prdct.UnitPrice;
                updated.UnitsInStock = prdct.UnitsInStock;

                bool result = _ps.Update(updated);
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
                TempData["Message"] = "Hata Oluştu";
            }

            return View(prdct);
        }

        public IActionResult Delete(Guid id)
        {
            bool result = _ps.Remove(_ps.GetById(id));
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
            bool result = _ps.Activate(id);
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
