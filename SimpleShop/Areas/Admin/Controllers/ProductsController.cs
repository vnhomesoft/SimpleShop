using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SimpleShop.Models;
using SimpleShop.Models.ViewModels;

namespace SimpleShop.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private SimpleProjectModel db = new SimpleProjectModel();

        // GET: Admin/Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(long? id)
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

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName");
            ViewBag.ID = new SelectList(db.ProductFeatures, "ID", "Color");
            return View(new Product());
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductCode,ProductName,Description,Price,Quantity,Branch,CategoryID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName", product.CategoryID);
            ViewBag.ID = new SelectList(db.ProductFeatures, "ID", "Color", product.ID);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(long? id)
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
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName", product.CategoryID);
            ViewBag.ID = new SelectList(db.ProductFeatures, "ID", "Color", product.ID);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProductCode,ProductName,Description,Price,Quantity,Branch,CategoryID, UploadFile")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.ProductFeature.ID = product.ID;

                db.Entry(product).State = EntityState.Modified;
                SaveUploadedImage(product); // TODO: upload image, cần accept field ở phần Bind
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategoryName", product.CategoryID);
            ViewBag.ID = new SelectList(db.ProductFeatures, "ID", "Color", product.ID);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(long? id)
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

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Sử dụng view upload riêng
        public ActionResult Upload(ImageUpload product)
		{
            
            return View(product);
		}

        private void SaveUploadedImage(Product product)
		{
            string uploadDir = "/Uploads";
            string relativePath = product.UploadFile.FileName;
            string absolutePath = Server.MapPath(uploadDir + "/" + product.UploadFile.FileName);
            var featuredImage = new ProductImage
            {
                ImageUrl = relativePath,
                IsFeatured = true
            };
            product.UploadFile.SaveAs(absolutePath);
            product.ProductImages.Add(featuredImage);
           // db.SaveChanges();
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
