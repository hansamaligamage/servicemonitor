using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorWorkerService.Notifications
{
    class Sms : INotification
    {
        public virtual void Send()
        {
            Console.WriteLine("Message From SMS");

        }
    }
}
