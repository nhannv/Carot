using System;

namespace Carot.ERP.Notifications
{
	public class NotificationHandlerAttribute : Attribute
	{
		public string Channel { get; set; }

		public NotificationHandlerAttribute(string channel = null)
		{
			Channel = channel;
		}
	}
}
