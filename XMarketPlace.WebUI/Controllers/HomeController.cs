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

        public HomeController(ICoreService<Product> ps)
        {
            _ps = ps;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        // This method is to show products of one category
        public IActionResult Products(Guid id)
        {
            return View(_ps.GetDefault(x => x.CategoryID == id).ToList());
        }
    }
}
