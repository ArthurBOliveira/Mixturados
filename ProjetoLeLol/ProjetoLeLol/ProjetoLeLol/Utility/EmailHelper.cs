using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mail;

namespace ProjetoLeLol.Utility
{
    public class EmailHelper
    {
        private string _sender = "noreply@mixturadoslol.com.br";
        private string _password = "MixTurados@15";

        public bool SendEmail(string receiver, string subject, string body)
        {
            bool result = true;

            try
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage(_sender, receiver);
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = "smtp.mixturadoslol.com.br";
                client.Credentials = new System.Net.NetworkCredential(_sender, _password);
                mail.Subject = subject;
                mail.Body = body;
                client.Send(mail);
            }
            catch(Exception e)
            {
                result = false;
            }

            return result;
        }
    }
}