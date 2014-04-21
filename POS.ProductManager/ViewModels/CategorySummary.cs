using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POS.ProductManager.ViewModels
{
    public class CategorySummary
    {
        public virtual string CategoryName { get; set; }
        public virtual int Quantity { get; set; }
    }
}