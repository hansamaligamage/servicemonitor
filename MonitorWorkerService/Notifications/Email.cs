using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace MonitorWorkerService.Notifications
{
    class Email : INotification
    {
        public virtual void Send()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("hansamaligamages@gmail.com");
                mail.To.Add("ham@tiqri.com");
                mail.Subject = "Test Mail";
                mail.Body = "This is for testing SMTP mail from GMAIL";

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("hansamaligamages@gmail.com", "MVP@2020");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
