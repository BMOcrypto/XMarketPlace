using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XMarketPlace.Core.Service;
using XMarketPlace.Model.Context;
using XMarketPlace.Model.Entities;

namespace XMarketPlace.WebUI.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ICoreService<Product> _ps;
        private readonly ICoreService<User> _us;
        private readonly ShoppingCart _shoppingCart;
        
        public ShoppingCartController(ICoreService<Product> ps, ShoppingCart shoppingCart,ICoreService<User> us)
        {
            _ps = ps;
            _us = us;
            _shoppingCart = shoppingCart;
        }
        
        [Authorize]
        public IActionResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            
            return View(_shoppingCart);
        }


        public IActionResult AddToShoppingCart(Guid id)
        {
            _shoppingCart.AddToCart(_ps.GetById(id));

            return RedirectToAction("Index");
        }
        //

        

        public IActionResult RemoveFromShoppingCart(Guid id)
        {
            //bool result = _ps.Remove(_ps.GetById(id));
            _shoppingCart.RemoveFromCart(_ps.GetById(id));

            return RedirectToAction("Index");
            
        }
        //

        public IActionResult SubmitOrder()
        {

            // Not used
            

            return RedirectToAction("Index");
        }
    }
}
