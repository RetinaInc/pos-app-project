using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS.ProductManager.Models.Domain
{
    public class Category
    {
        public Category()
        {
            // set timezone to bangladesh
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");
            DateTime bdTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi);
            Created = bdTime;
        }

        [Display(Name = "Category")]
        public virtual int CategoryId { get; set; }
        [HiddenInput]
        [Display(Name = "Category Id")]
        public virtual string CategoryStoreId { get; set; }
        [Required]
        [Display(Name = "Category Name")]
        public virtual string Name { get; set; }
        [Display(Name = "Purchase Price (BDT)")]
        public double PurchasePrice { get; set; }
        [Display(Name = "Selling Price (BDT)")]
        public virtual double SellingPrice { get; set; }
        public virtual DateTime Created { get; private set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}