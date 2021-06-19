using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XMarketPlace.Core.Entity;
using XMarketPlace.Model.Entities;
using XMarketPlace.Model.Maps;

namespace XMarketPlace.Model.Context
{
    public class XMarketPlaceContext:DbContext
    {
        public XMarketPlaceContext(DbContextOptions<XMarketPlaceContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new ProductMap());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public override int SaveChanges()
        {
            // ChangeTracker.Entries() ifadesi o an çalıştığımız tabloyu(modeli) temsil eder.

            var collection = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified || x.State == EntityState.Added).ToList();

            string computerName = Environment.MachineName;
            string ipAddress = "127.0.0.1"; // localhost
            DateTime date = DateTime.Now;

            foreach (var item in collection)
            {
                CoreEntity entity = item.Entity as CoreEntity; // item.Entity ifadesi model bilgisini alırız.

                if (item != null)
                {
                    if (entity != null)
                    {
                        switch (item.State) // item.State kaydın durumunu verir
                        {
                            case EntityState.Added:
                                entity.CreatedComputerName = computerName;
                                entity.CreatedIP = ipAddress;
                                entity.CreatedDate = date;
                                break;
                            case EntityState.Modified:
                                entity.ModifiedComputerName = computerName;
                                entity.ModifiedIP = ipAddress;
                                entity.ModifiedDate = date;
                                break;
                        }
                    }
                    
                }
            }

            return base.SaveChanges();
        }
    }
}
