using System;
using System.Collections.Generic;
using System.Text;
using XMarketPlace.Model.Context;

namespace XMarketPlace.Model.Entities
{
    public class OrderRepository : IOrderRepository
    {
        private readonly XMarketPlaceContext _xMarketPlaceContext;
        private readonly ShoppingCart _shoppingCart;
        public OrderRepository(XMarketPlaceContext xMarketPlaceContext, ShoppingCart shoppingCart)
        {
            _xMarketPlaceContext = xMarketPlaceContext;
            _shoppingCart = shoppingCart;

        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;
            _xMarketPlaceContext.Orders.Add(order);
            _xMarketPlaceContext.SaveChanges();

            var shoppingCartItems = _shoppingCart.ShoppingCartItems;

            foreach (var item in shoppingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Amount = item.Amount,
                    ProductId = item.Product.ID,
                    OrderId = order.OrderId,
                    Price = item.Product.UnitPrice
                };
                _xMarketPlaceContext.OrderDetails.Add(orderDetail);
            }
            _xMarketPlaceContext.SaveChanges();
            

        }
    }
}
