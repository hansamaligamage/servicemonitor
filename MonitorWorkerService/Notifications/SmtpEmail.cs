using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorWorkerService.Notifications
{
    class SmtpEmail : Email
    {
        public override void Send()
        {
            base.Send();
            //new implementation
        }
    }
}
