using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetShop.Models;

namespace PetShop.Data
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //one to many
            modelBuilder.Entity<Item>().HasOne<Order>(o => o.Order).WithMany(i => i.Items).HasForeignKey(o => o.OrderId);
            // one to one
          //  modelBuilder.Entity<Product>().HasOne<Item>(i=>i.Item).WithOne(pi => pi.Product).HasForeignKey<Item>(i=>i.ProductId);

            modelBuilder.Entity<Product>().HasKey(c => c.Id);

            modelBuilder.Entity<Product>().Property(c => c.Id).ValueGeneratedNever();


            base.OnModelCreating(modelBuilder); 
        }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Item> Items { get; set; }


    }
}
