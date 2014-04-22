using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using POS.ProductManager.Models.Domain;

namespace POS.ProductManager.ViewModels
{
    public sealed class ProductViewModel
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int Quantity { get; set; }
    }
}