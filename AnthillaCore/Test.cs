using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace AnthillaCore {

    public class Test {

        public static void Write(string _content) {
            string tmpRoot = FileTools.GetRoot() + "/.tmp";
            string localdirectory = tmpRoot + "/.test";
            Directory.CreateDirectory(localdirectory);
            string recapFile = "stuff.json";
            string path = Path.Combine(localdirectory, recapFile);

            if (File.Exists(path)) {
                File.Delete(path);
            }
            using (StreamWriter sw = File.CreateText(path)) {
                sw.Write(_content);
            }
        }

        public static void CustomHeaders() {
            MailMessage mail = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("fimbulsgone@gmail.com", "hagall17");
            smtp.Host = "smtp.gmail.com";
            smtp.Timeout = 30000;

            mail.From = new System.Net.Mail.MailAddress("fimbulsgone@gmail.com");
            mail.To.Add("damiano.zanardi@anthilla.com");

            mail.Headers.Add("X-Uno", "uno");
            mail.Headers.Add("X-Due", "due");

            mail.IsBodyHtml = true;
            mail.Subject = "This is an email";
            mail.Body = "this is the body content of the email.";
            smtp.Send(mail);
        }

        public static dynamic GetMail() {
            var client = new ImapX.ImapClient("imap.gmail.com", true);
            client.Connect();
            client.Login("damiano.zanardi@anthilla.com", "AnthillaDev2014");
            client.Behavior.MessageFetchMode = ImapX.Enums.MessageFetchMode.Full;
            client.Behavior.AutoPopulateFolderMessages = true;
            var c = client.Behavior.RequestedHeaders;
            ImapX.Message[] messages = client.Folders["testfolder"].Search();

            var m = messages.First();

            client.Logout();
            client.Disconnect();

            //var ms = JsonConvert.SerializeObject(m);
            return m.Headers;
        }
    }
}