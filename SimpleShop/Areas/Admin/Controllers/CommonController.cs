using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleShop.Models;

namespace SimpleShop.Areas.Admin.Controllers
{
    public class CommonController : Controller
    {
        SimpleProjectModel db = new SimpleProjectModel();
        public PartialViewResult GetMenuView()
		{
            var categories = db.Categories.ToList();
            ViewBag.Categories = categories;
            return PartialView("_MenuView");
		}
    }
}