using System;
using System.Collections.Generic;
using System.Text;

namespace XMarketPlace.Model.Entities
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
    }
}
