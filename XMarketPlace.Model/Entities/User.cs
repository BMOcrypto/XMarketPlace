using System;
using System.Collections.Generic;
using System.Text;
using XMarketPlace.Core.Entity;

namespace XMarketPlace.Model.Entities
{
    public class User : CoreEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public DateTime? LastLogin { get; set; }

        ///Product tablosu ile User tablosunu ilişkilendirmeye gerek duymadım...
        //public virtual List<Product> Products { get; set; } // User sınıfı için Product sınıfını bağladık

    }
}
