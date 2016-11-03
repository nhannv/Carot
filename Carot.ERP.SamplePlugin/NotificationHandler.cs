using Carot.ERP.Notifications;

namespace Carot.ERP.SamplePlugin
{
    public class NotificationHandler
    {
		[NotificationHandler(channel:"*")]
		public void HandleNotification(Notification notification)
		{
		}
    }
}
