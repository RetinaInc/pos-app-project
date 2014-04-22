using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using POS.ProductManager.Models.Domain;
using POS.ProductManager.DataServices;
using POS.ProductManager.ViewModels;

namespace POS.ProductManager.Controllers
{
    // make sure that the user is logged in before accessing this controller
    [Authorize]
    public class ProductController : Controller
    {
        private ProductContext db = new ProductContext();

        // GET: /Product/

        public ViewResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            IQueryable<Product> products = db.Products;

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper())
                                       || s.Category.Name.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    products = products.OrderBy(s => s.Created);
                    break;
                case "date_desc":
                    products = products.OrderByDescending(s => s.Created);
                    break;
                default:
                    products = products.OrderBy(s => s.Name);
                    break;
            }

            return View(products);
        }

        // GET: /Product/Details/5

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: /Product/Create

        public ActionResult Create()
        {
            ViewBag.CategoryList = db.Categories.ToList();
            return View();
        }

        // POST: /Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "ProductId,ProductStoreId,Name,Created,CategoryId")]
        public ActionResult Create(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                ProductService.CreateProduct(product);
                //db.Products.Add(product);
                //db.SaveChanges();
                //string catName = db.Categories.Find(product.CategoryId).CategoryStoreId;
                //product.ProductStoreId = ProductService.GenerateId(product.ProductId, product.Name, catName);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryStoreId", product.CategoryId);
            return View(product);
        }

        //// GET: /Product/Edit/5

        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Product product = db.Products.Find(id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryStoreId", product.CategoryId);
        //    return View(product);
        //}

        //// POST: /Product/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]

        //public ActionResult Edit([Bind(Include = "ProductId,ProductStoreId,Name,Created,CategoryId")] Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(product).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryStoreId", product.CategoryId);
        //    return View(product);
        //}

        // GET: /Product/Delete/5

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
