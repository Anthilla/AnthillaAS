using AnthillaCore.Models;
using ImapX;
using System.Collections.Generic;

namespace AnthillaCore.Mail {

    public class Anth_MailAction {
        private Anth_MailClient client = new Anth_MailClient();
        private Anth_MailRepository repo = new Anth_MailRepository();

        public IEnumerable<Anth_MailModel> Download(string server, string account, string pwd) {
            var table = client.GetAllMailDump(server, account, pwd);
            var mails = new List<Anth_MailModel>();
            foreach (Message msg in table) {
                var m = repo.Create(msg.From.ToString(), msg.To.ToString(), msg.Body.ToString(), msg.Subject.ToString(), "", "");
                mails.Add(m);
            }
            return mails;
        }
    }
}