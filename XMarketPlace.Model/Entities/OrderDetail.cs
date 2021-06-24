using System;
using System.Collections.Generic;
using System.Text;

namespace XMarketPlace.Model.Entities
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public Guid ProductId { get; set; } 
        //public Product Product { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}
