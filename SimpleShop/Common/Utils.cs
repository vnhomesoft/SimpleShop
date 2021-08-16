using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleShop.Common
{
	public class Utils
	{
		static string uploadDirPath = "http://localhost:65499/" + "Uploads";
		public static string GetImageUrl(string relativePath)
		{
			return uploadDirPath + "/" + relativePath;
		}
	}
}