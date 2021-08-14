using SimpleShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace SimpleShop.Controllers
{  
    public class ProductsController : Controller
    {
        SimpleProjectModel db = new SimpleProjectModel();

        //// GET: Products
        //public ActionResult Index()
        //{
        //    IEnumerable<Product> list = db.Products.Include("ProductFeature").ToList();
        //    return View(list);
        //}

        public ActionResult Index()
        {
            int categoryId = 0;
            List<Product> products;
            // Dùng Request.Params.Get() để lấy tham số từ query string 
            if (int.TryParse(Request.Params.Get("category"), out categoryId))
            {
                // Hiển thị dữ liệu của category chỉ định
                products = db.Products.Include(p => p.Category)
                    .Where(p => p.CategoryID == categoryId).ToList();
            }
            else
            {
                // Hiển thị all sản phẩm
                products = db.Products.Include(p => p.Category)
                    .ToList();
            }
            return View(products);
        }


    }
}