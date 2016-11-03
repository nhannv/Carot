using System;

namespace Carot.ERP.Notifications
{
    public interface INotificationService
	{
		void Initialize(IServiceProvider serviceProvider);

		void SendNotification(Notification notification);
	}
}