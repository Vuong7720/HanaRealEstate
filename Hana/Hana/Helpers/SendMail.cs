using System.Net;
using System.Net.Mail;

namespace Hana.Helpers
{
    public class SendMail
    {
        public static bool SenMail(string to, string subject, string body, string attachFile)
        {
            try
            {
                MailMessage msg = new MailMessage(AddSendMail.emailSender, to, subject, body);

                using(var client = new SmtpClient(AddSendMail.hostEmail, AddSendMail.postEmail))
                {
                    client.EnableSsl = true;

                    NetworkCredential credential = new NetworkCredential(AddSendMail.emailSender, AddSendMail.passwordSender);
                    client.UseDefaultCredentials = false;
                    client.Credentials = credential;
                    client.Send(msg);
                }

            }
            catch (Exception ex)
            {
                return false;
            }


            return true;
        }
    }
}
