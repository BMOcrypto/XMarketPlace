using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XMarketPlace.Core.Service;
using XMarketPlace.Model.Entities;

namespace XMarketPlace.WebUI.Models.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly ICoreService<Category> _cs;

        public CategoryViewComponent(ICoreService<Category> cs)
        {
            _cs = cs;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _cs.GetActive();
            return View(categories);
        }
    }
}
