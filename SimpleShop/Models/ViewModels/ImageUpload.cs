using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleShop.Models.ViewModels
{
	// TODO: ADD - Upload image
	// Vì field UploadFile không phải là field trong DB -> không 
	public class ImageUpload
	{
		public int ProductID { get; set; }
		public HttpPostedFileBase UploadFile { get; set; }
	}
}