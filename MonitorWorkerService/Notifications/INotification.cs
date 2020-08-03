using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorWorkerService.Notifications
{
    interface INotification
    {
        void Send();
    }
}
