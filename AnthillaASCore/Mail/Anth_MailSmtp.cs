using System.Net;
using System.Net.Mail;

namespace AnthillaASCore.Mail
{
    public class Anth_MailSmtp
    {
        private Anth_MailRepository repo = new Anth_MailRepository();

        public MailMessage SendMail(string from, string pwd, string to, string subject, string body, string tags)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);

            SmtpClient smtp = new SmtpClient();
            //smtp.Port = 465; non va?
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(from, pwd);
            smtp.Host = "smtp.gmail.com";
            smtp.Timeout = 30000;

            mail.To.Add(new MailAddress(to));

            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;
            smtp.Send(mail);

            var mappedMail = repo.Import(mail, tags);

            return mail;
        }
    }
}