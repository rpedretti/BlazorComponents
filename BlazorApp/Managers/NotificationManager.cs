using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Managers
{
    public class NotificationManager
    {
        public event EventHandler<NotificationEventArgs> ShowNotification;

        public void ShowNotificationMessage(string message, string title)
        {
            ShowNotification?.Invoke(this, new NotificationEventArgs
            {
                Message = message,
                Title = title
            });
        }

        public class NotificationEventArgs : EventArgs
        {
            public string Message { get; set; }
            public string Title { get; set; }
        }
    }
}
