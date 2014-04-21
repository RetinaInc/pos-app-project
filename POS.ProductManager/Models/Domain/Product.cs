using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POS.ProductManager.Models.Domain
{
    public class Product
    {
        public Product()
        {
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");
            DateTime bdTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi);
            Created = bdTime;
        }

        public virtual int ProductId { get; set; }
        [HiddenInput]
        [Display(Name = "Product Id")]
        public virtual string ProductStoreId { get; set; }
        [Required]
        [Display(Name = "Product Name")]
        public virtual string Name { get; set; }
        public virtual DateTime Created { get; private set; }
        public virtual int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}