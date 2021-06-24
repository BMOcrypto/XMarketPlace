using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XMarketPlace.Model.Context;


namespace XMarketPlace.Model.Entities
{
    public class ShoppingCart
    {
        
        private readonly XMarketPlaceContext _xMarketPlaceContext;
        public ShoppingCart(XMarketPlaceContext xMarketPlaceContext)
        {
            _xMarketPlaceContext = xMarketPlaceContext;
        }
        
        
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        //
        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<XMarketPlaceContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Product product)
        {
            var shoppingCartItem =_xMarketPlaceContext.ShoppingCartItems.SingleOrDefault(s => s.Product.ID == product.ID && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Amount = 1
                };
                
                _xMarketPlaceContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _xMarketPlaceContext.SaveChanges();
        }

        public int RemoveFromCart(Product product)
        {
            var shoppingCartItem =
                    _xMarketPlaceContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Product.ID == product.ID && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _xMarketPlaceContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _xMarketPlaceContext.SaveChanges();

            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??
                   (ShoppingCartItems =
                       _xMarketPlaceContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                           .Include(s => s.Product)
                           .ToList());

            
        }

        public void ClearCart()
        {
            var cartItems = _xMarketPlaceContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _xMarketPlaceContext.ShoppingCartItems.RemoveRange(cartItems);

            _xMarketPlaceContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _xMarketPlaceContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Product.UnitPrice * c.Amount).Sum();
            return (decimal)total;
        }

        //
    }
}
