using System;
using System.Collections.Generic;
using System.Text;
using XMarketPlace.Core.Entity;

namespace XMarketPlace.Model.Entities
{
    public class Product : CoreEntity
    {
        public string ProductName { get; set; }
        public string ProductDetail { get; set; }
        public string ProductSummary { get; set; }
        public string ImagePath { get; set; }
        public double UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public int ViewCount { get; set; }
        public int AddToCartCount { get; set; }

        public Guid CategoryID { get; set; } // FOREIGN KEY
        public virtual Category Category { get; set; }


        ///Product tablosu ile User tablosunu ilişkilendirmeye gerek duymadım...
        //public Guid UserID { get; set; } // FOREGIN KEY
        //public virtual User User { get; set; } // Product sınıfı için User sınıfını bağladık
    }
}
