using SimpleShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace SimpleShop.Controllers
{  
    public class ProductsController : Controller
    {
        SimpleProjectModel db = new SimpleProjectModel();

        // GET: Products
        public ActionResult Index()
        {
            IEnumerable<Product> list = db.Products.Include("ProductFeature").ToList();
            return View(list);
        }
    }
}