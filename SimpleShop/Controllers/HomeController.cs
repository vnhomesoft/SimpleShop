using SimpleShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleShop.Controllers
{
	public class HomeController : Controller
	{
		SimpleProjectModel db = new SimpleProjectModel();
		public ActionResult Index()
		{
			//return View();
			return RedirectToAction("Index", new { controller = "Products" });
		}

		public ActionResult About()
		{
			var about = db.Posts.Where(post => post.Name == "about_page").FirstOrDefault();
			if (about != null)
			{
				ViewBag.Content = about.Content;
			}
			else
			{
				ViewBag.Content = "<h3>Nội dung này chưa được cập nhật</h3>";
			}

			return View();
		}

		public ActionResult Contact()
		{
			var about = db.Posts.Where(post => post.Name == "contact_page").FirstOrDefault();
			if (about != null)
			{
				ViewBag.Content = about.Content;
			}
			else
			{
				ViewBag.Content = "<h3>Nội dung này chưa được cập nhật</h3>";
			}

			return View();
		}
	}
}