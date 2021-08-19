using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleShop.Models;
using SimpleShop.Models.ViewModels;

namespace SimpleShop.Controllers
{
	public class CartController : Controller
	{
		SimpleProjectModel db = new SimpleProjectModel();
		// GET: Cart
		public ActionResult Index()
		{
			return View(GetCartItems());
		}

		public ActionResult AddToCart(int id) // parameter must named "id" to match name on route
		{
			var product = db.Products.Find(id);
			if (product == null)
			{
				return HttpNotFound();
			}

			// Kiểm tra xem sản phẩm đã được thêm vào cart trước đó .
			//   Nếu đã có rồi thì báo là đã tồn tại. (cũng có thể xử lí để tăng số lượng đặt hàng)
			if (GetCartItems().Any(item => item.ProductId == product.ID))
			{
				TempData["Message"] = new NotificationMessage(
					string.Format("Product \"{0}\" has already been in cart.", product.ProductName),
					"error");
			}
			//   Nếu chưa thì thêm vào cart
			else
			{
				int quantity = int.Parse(Request.Params.Get("quantity"));
				if(quantity <= 0) { quantity = 1; }

				GetCartItems().Add(new CartItem(product, quantity));
				TempData["Message"] = new NotificationMessage(
					string.Format("Product \"{0}\" is added to cart.", product.ProductName),
					"success");
			}
			// Kiểm tra nếu tồn tại UrlReferrer  thì redirect về trang trước đó.
			//   Có trường hợp không tồn tại UrlReferrer (do trình duyệt chặn, không gửi lên) -> cần xử lí kiểu khác
			if (Request.UrlReferrer != null)
			{
				return Redirect(Request.UrlReferrer.AbsoluteUri);
			}
			return RedirectToAction("Index");
		}


		/// <summary>
		/// Cập nhật giá trị sản phẩm trong giỏ hàng.
		/// </summary>
		/// <param name="productIds"></param>
		/// <param name="quantities"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult UpdateCart(long[] productIds, int[] quantities)
		{
			var items = GetCartItems();
			for (int i = 0; i < productIds.Length; i++)
			{
				var cartItem = items.Where(product => product.ProductId == productIds[i]).First();
				cartItem.Quantity = quantities[i];
			}
			return RedirectToAction("Index");
		}

		/// <summary>
		/// Bỏ 1 sản phẩm khỏi giỏ hàng
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult RemoveFromCart(int id)
		{
			var product = db.Products.Find(id);
			if (product == null)
			{
				return HttpNotFound();
			}
			var cartItem = GetCartItems().Where(item => item.ProductId == id).First();
			GetCartItems().Remove(cartItem);
			return RedirectToAction("Index");
		}

		// Display checkout form
		public ActionResult CheckOut()
		{
			return View();
		}

		// Submit check out information
		[HttpPost]
		public ActionResult CheckedOut()
		{
			return View();
		}

		// Display success message after checked out
		public ActionResult CompletedOrder()
		{
			return View();
		}

		private List<CartItem> GetCartItems()
		{
			if (Session["Cart"] == null)
			{
				Session["Cart"] = new List<CartItem>();

			}
			return Session["Cart"] as List<CartItem>;
		}
	}
}