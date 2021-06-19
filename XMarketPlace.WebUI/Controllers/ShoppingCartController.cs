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

        //ADD TO CART FUNCTION (DENEME)
        //[Authorize]
        //public RedirectToActionResult AddToShoppingCart(int drinkId)
        //{
        //    var selectedDrink = _drinkRepository.Drinks.FirstOrDefault(p => p.DrinkId == drinkId);
        //    if (selectedDrink != null)
        //    {
        //        _shoppingCart.AddToCart(selectedDrink, 1);
        //    }
        //    return RedirectToAction("Index");
        //}

        public IActionResult AddToShoppingCart(Guid id)
        {
            _shoppingCart.AddToCart(_ps.GetById(id));

            //return View("Index");
            //return View(_shoppingCart);(hatalı)
            return RedirectToAction("Index");
        }
        //

        //REMOVE FROM CART FUNCTION (DENEME)
        //public RedirectToActionResult RemoveFromShoppingCart(int drinkId)
        //{
        //    var selectedDrink = _drinkRepository.Drinks.FirstOrDefault(p => p.DrinkId == drinkId);
        //    if (selectedDrink != null)
        //    {
        //        _shoppingCart.RemoveFromCart(selectedDrink);
        //    }
        //    return RedirectToAction("Index");
        //}

        public IActionResult RemoveFromShoppingCart(Guid id)
        {
            //bool result = _ps.Remove(_ps.GetById(id));
            _shoppingCart.RemoveFromCart(_ps.GetById(id));

            return RedirectToAction("Index");
            
        }
        //

        public IActionResult SubmitOrder()
        {

            // Orders will be submitted here
            

            return RedirectToAction("Index");
        }
    }
}
