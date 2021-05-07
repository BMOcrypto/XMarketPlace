using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XMarketPlace.Core.Service;
using XMarketPlace.Model.Entities;

namespace XMarketPlace.WebUI.Models.ViewComponents
{
    public class TrendingViewComponent : ViewComponent
    {
        private readonly ICoreService<Product> _ps;

        public TrendingViewComponent(ICoreService<Product> ps)
        {
            _ps = ps;
        }

        public IViewComponentResult Invoke()
        {
            var products = _ps.GetActive().OrderByDescending(x => x.ViewCount).Take(4).ToList();
            return View(products);
        }
    }
}
