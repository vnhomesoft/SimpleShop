using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SimpleShop.Models
{
	// TODO:
	public class ProductImage
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

		//[MaxLength(200)]
		public string ImageUrl { get; set; }

		/// <summary>
		/// Set True để làm hình ảnh đại diện cho sản phẩm
		/// </summary>
		public bool IsFeatured { get; set; }
	}
}