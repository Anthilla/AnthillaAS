using AnthillaCore.Configuration;
using AnthillaCore.Repository;
using AnthillaCore.Security;
using System.Net;
using System.Net.Mail;

namespace AnthillaCore.Mail {

    public class Anth_MailSmtp {
        private Anth_MailRepository repo = new Anth_MailRepository();

        private MailMessage BasicSendMail(string guid, string from, string pwd, string to, string subject, string body, string tags) {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(from, pwd);
            smtp.Host = "smtp.gmail.com";
            smtp.Timeout = 30000;
            mail.To.Add(new MailAddress(to));
            mail.Headers.Add("X-AnthillaGuid", guid);
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;
            smtp.Send(mail);
            repo.Import(mail, guid, tags);
            return mail;
        }

        private Anth_UserRepository userRepo = new Anth_UserRepository();
        private Anth_SmtpConfig smptconf = new Anth_SmtpConfig();
        private KeyIdentity_Repository kiRepo = new KeyIdentity_Repository();

        public MailMessage SendMail(string mailGuid, string userGuid, string efrom, string to, string subject, string body, string tags) {
            string host;
            string account;
            string credentialAccount;
            string credentialPassword;
            account = efrom;
            string[] settings = smptconf.GetSmtpSetting();
            host = settings[0];
            credentialAccount = settings[1];
            credentialPassword = settings[2];
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(account);
            var userTO = userRepo.GetByEmail(to);
            var guid = userTO.AnthillaGuid;
            AnthillaPublicKey key = kiRepo.GetPublicKey(guid);
            var encryptedBody = AnthillaRsa.Encrypt(body, key);
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(credentialAccount, credentialPassword);
            smtp.Host = host;
            smtp.Timeout = 30000;
            mail.To.Add(new MailAddress(to));
            mail.Headers.Add("X-AnthillaGuid", guid);
            mail.Headers.Add("X-RSAEnabled", (true).ToString());
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = encryptedBody;
            smtp.Send(mail);
            repo.Import(mail, mailGuid, tags);
            return mail;
        }
    }
}