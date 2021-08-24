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
using System.IO;

namespace SimpleShop.Areas.Admin.Controllers
{
    [Authorize]
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

        /// <summary>
        /// Hiển thị gallery view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Gallery(int id)
		{
            Product product = db.Products.Include(p => p.ProductImages)
                .Where(p => p.ID == id).First();
            ImageUpload viewModel = new ImageUpload { Product = product };
            return View(viewModel);
		}

        /// <summary>
        /// Upload 1 hoặc nhiều file (dùng để upload image gallery)
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadGallery(ImageUpload viewModel)
		{
            var product = db.Products.Find(viewModel.Product.ID);
            var fileList = new List<HttpPostedFileBase>();
            if(viewModel.UploadFile1 != null)
			{
                fileList.Add(viewModel.UploadFile1);
			}
            if (viewModel.UploadFile2 != null)
            {
                fileList.Add(viewModel.UploadFile2);
            }
            if (viewModel.UploadFile3 != null)
            {
                fileList.Add(viewModel.UploadFile3);
            }
			//if (ModelState.IsValid)       // bởi vì thông tin product gửi lên không đầy đủ (thiếu required fields) -> không check điều kiện này (nếu không sẽ luôn false)
			//{
                db.Entry(product).State = EntityState.Modified;
                db.Entry(product.ProductFeature).State = EntityState.Unchanged;
                SaveUploadedImages(product, fileList);
                db.SaveChanges();
			//}

            return RedirectToAction("Gallery", "Products", new { id = product.ID});      // Sử dụng redirect để tránh tình trạng postback khiến upload lại
		}

        /// <summary>
        /// Lưu hình ảnh đại diện (featured image) của product
        /// </summary>
        /// <param name="product"></param>
        private void SaveUploadedImage(Product product)
		{
            // Bỏ qua xử lí nếu không có file được upload
            if(product.UploadFile == null) { return; }

            // Lấy đường dẫn để lưu
            string uploadDir = "/Uploads";
            string relativePath = Common.Utils.PrependUniqueString(product.UploadFile.FileName);
            string absolutePath = Server.MapPath(uploadDir + "/" + relativePath);
           
            var featuredImage = new ProductImage
            {
                ImageUrl = relativePath,
                IsFeatured = true
            };

            // Cơ bản để lưu file về
            product.UploadFile.SaveAs(absolutePath);      
           
            // Gắn thông tin imgage vào sản phẩm (lưu dữ liệu vào bảng ProductImage)
            product.ProductImages.Add(featuredImage);
        }

        private void SaveUploadedImages(Product product, List<HttpPostedFileBase> uploadFiles)
        {
            string uploadDir = "/Uploads";
            foreach (var file in uploadFiles)
            {
                string relativePath = Common.Utils.PrependUniqueString(file.FileName);
                string absolutePath = Server.MapPath(uploadDir + "/" + relativePath);
                var galleryImage = new ProductImage
                {
                    ImageUrl = relativePath,
                    IsFeatured = false      // Image gallery thì set giá trị này là False
                };
                file.SaveAs(absolutePath);
                product.ProductImages.Add(galleryImage);
            }
        }


        /// <summary>
        /// Xóa image của product sau đó return về trang Edit.
        /// Cần cung cấu tham số RedirectUrl ở dạng query string
        /// </summary>
        /// <param name="id">Image ID</param>
        public ActionResult RemoveImage(int id)
		{
            string redirectUrl = Request.Params.Get("RedirectUrl"); // Ví dụ: /Admin/Products/Edit/12
            ProductImage image = db.ProductImages.Find(id);
            Product product = db.Products.Find(image.Product.ID);
            product.ProductImages.Remove(image);
            db.Entry(product).State = EntityState.Modified;
            db.Entry(product.ProductFeature).State = EntityState.Unchanged; // Fix vấn đề quan hệ 1-1 (không cần care trong xử lí upload)
            db.SaveChanges();

            string uploadDir = "/Uploads";
            string absolutePath = Server.MapPath(uploadDir + "/" + image.ImageUrl);
			if (System.IO.File.Exists(absolutePath))
			{
                System.IO.File.Delete(absolutePath);
			}
            return Redirect(redirectUrl);
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
