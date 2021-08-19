using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleShop.Models.ViewModels
{
	public class CartItem
	{
		public long ProductId { get; set; }
		public string ProductName { get; set; }
		public string ImageUrl { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }

		public CartItem(Product product, int quantity)
		{
			ProductId = product.ID;
			ProductName = product.ProductName;
			ImageUrl = product.FeaturedImage;
			Quantity = quantity;
			Price = product.Price.GetValueOrDefault();
		}
	}
}