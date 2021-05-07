using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XMarketPlace.Core.Service;
using XMarketPlace.Model.Entities;

namespace XMarketPlace.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICoreService<Product> _ps;
        private readonly ICoreService<Category> _cs;
        private readonly ICoreService<User> _us;


        public HomeController(ICoreService<Product> ps, ICoreService<Category> cs, ICoreService<User> us)
        {
            _ps = ps;
            _cs = cs;
            _us = us;

        }
        
        public IActionResult Index()
        {
            return View(_ps.GetAll());// content will be changed...
        }

        public IActionResult ShowAllProducts()
        {
            return View(_ps.GetAll());
        }

        // This method is to show products of one category
        public IActionResult Products(Guid id)
        {
            return View(_ps.GetDefault(x => x.CategoryID == id).ToList());
        }
        public IActionResult Product(Guid id)
        {
            var item = _ps.GetById(id);
            item.ViewCount++; // Görüntülemek istenen ürünün görüntülenme sayısı 1 arttırıyoruz
            _ps.Update(item);

            //şimdilik Tuple kullanmıyorum, gerekirse düzenlerim.
            //return View(Tuple.Create<Product, User>(blog, _us.GetById(blog.UserID)));
            return View(item);
        }

    }
}
