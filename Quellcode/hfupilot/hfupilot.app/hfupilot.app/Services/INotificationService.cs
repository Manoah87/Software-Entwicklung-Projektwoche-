using System;
using System.Collections.Generic;
using System.Text;

namespace hfupilot.app.Services
{
    public interface INotificationService
    {
        void ShowNotification(string title, string description);
    }
}
