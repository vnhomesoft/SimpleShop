using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleShop.Models.ViewModels
{
	public class NotificationMessage
	{
		public string Content { get; set; }
		/// <summary>
		/// success, error, warn
		/// </summary>
		public string MessageType { get; set; }

		public NotificationMessage() { }
		public NotificationMessage(string content, string messageType)
		{
			Content = content;
			MessageType = messageType;
		}
	}
}