using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using POS.ProductManager.Models.Domain;

namespace POS.ProductManager.DataServices
{
    public class ProductContext :DbContext
    {
        public ProductContext() : base("DefaultConnection") { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //remove auto pluralization of table names
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}