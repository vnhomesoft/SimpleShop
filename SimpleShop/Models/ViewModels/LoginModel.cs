using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleShop.Models.ViewModels
{
	public class LoginModel
	{
		public string LoginName { get; set; }

		public string Password { get; set; }

		public bool RememberLogin { get; set; }
	}
}