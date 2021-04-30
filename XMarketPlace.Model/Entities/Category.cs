using System;
using System.Collections.Generic;
using System.Text;
using XMarketPlace.Core.Entity;

namespace XMarketPlace.Model.Entities
{
    public class Category : CoreEntity
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public virtual List<Product> Products { get; set; } // Category sınıfı için Post sınıfını bağladık
    }
}
