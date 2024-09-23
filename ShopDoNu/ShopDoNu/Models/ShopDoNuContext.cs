using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopDoNu.Identity;
using System.Data.Entity;
namespace ShopDoNu.Models
{
    public class ShopDoNuContext: DbContext
    {
        public ShopDoNuContext(): base("ShopDoNuDB") { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }

    }
}