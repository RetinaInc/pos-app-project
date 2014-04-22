using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POS.ProductManager.Models.Domain;

namespace POS.ProductManager.ViewModels
{
    public class CategoryViewModel
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}