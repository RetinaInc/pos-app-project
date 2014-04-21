using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POS.ProductManager.Models.Domain;
using POS.ProductManager.ViewModels;

namespace POS.ProductManager.DataServices
{
    public class ProductService
    {
        public static string GenerateId(int itemId, string itemName, string itemCategory = "")
        {
            string idPart = itemId.ToString("D3");
            string namePart = GetName(itemName);
            string genId = idPart + "-" + namePart;
            if (itemCategory != "") genId = itemCategory + "-" + idPart + "-" + namePart;
            return genId;
        }

        private static string GetName(string name)
        {
            //string[] words = name.Split(' ');
            int range = 3;
            if (name.Length < 3) range = name.Length;
            string namePart = name.Substring(0, range).ToUpper();
            //if (words.Count() <= 1) return namePart;
            //namePart = "";
            //if (name.Length < 3) range = name.Length;
            //namePart = (words.Aggregate(namePart, (current, word) => current + word.Substring(0, range))).ToUpper();
            return namePart;
        }

        public List<CategorySummary> GetSummary()
        {
            var db = new ProductContext();
            var categories = db.Categories;

            // linq expressin to get list of summaries
            var summaries = (from category in categories
                             let quantity = db.Products.Count(p => p.CategoryId == category.CategoryId)
                             select new CategorySummary { CategoryName = category.Name, Quantity = quantity }
                            ).ToList();

            return summaries;
        }

        internal static void CreateProduct(CreateProduct productInfo)
        {
            var db = new ProductContext();
            string catStoreId = db.Categories.Find(productInfo.CategoryId).CategoryStoreId;
            for (int i = 1; i <= productInfo.Quantity; i++)
            {
                var product = new Product {Name = productInfo.Name, CategoryId = productInfo.CategoryId};
                db.Products.Add(product);
                db.SaveChanges();
                product.ProductStoreId = GenerateId(product.ProductId, product.Name, catStoreId);
                db.SaveChanges();
            }
            db.Dispose();
        }
    }
}