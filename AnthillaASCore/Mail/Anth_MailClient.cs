using System.Collections.Generic;
using System.Linq;
using AnthillaASCore.Models;
using ImapX;
using ImapX.Enums;

namespace AnthillaASCore.Mail
{
    public class Anth_MailClient
    {
        public void ConnectGmail()
        {
            var c = DeNSo.Session.New.Get<Anth_ClientModel>(m => m.ClientAlias == "gmail").FirstOrDefault();
            var address = c.ClientAddress;
            var client = new ImapClient(address, true);
            client.Connect();
            client.Login("fimbulsgone@gmail.com", "hagall17");
        }

        public void Login(string server, string account, string pwd)
        {
            var c = DeNSo.Session.New.Get<Anth_ClientModel>(m => m.ClientAlias == server).FirstOrDefault();
            var address = c.ClientAddress;
            var client = new ImapClient(address, true);
            client.Connect();
            client.Login(account, pwd);
        }

        public Message[] GetAllMail(string server, string account, string pwd)
        {
            var c = DeNSo.Session.New.Get<Anth_ClientModel>(m => m.ClientAlias == server).FirstOrDefault();
            var address = c.ClientAddress;
            var client = new ImapClient(address, true);
            client.Connect();
            client.Login(account, pwd);
            client.Behavior.MessageFetchMode = MessageFetchMode.Full;
            client.Behavior.AutoPopulateFolderMessages = true;
            var inbox = client.Folders.Inbox;
            var messages = inbox.Search();
            client.Logout();
            client.Disconnect();
            return messages;
        }

        public IEnumerable<Anth_MailModel> GetAllMailMapped(string server, string account, string pwd)
        {
            var c = DeNSo.Session.New.Get<Anth_ClientModel>(m => m.ClientAlias == server).FirstOrDefault();
            var address = c.ClientAddress;
            var client = new ImapClient(address, true);
            client.Connect();
            client.Login(account, pwd);
            client.Behavior.MessageFetchMode = MessageFetchMode.Basic;
            client.Behavior.AutoPopulateFolderMessages = true;
            var inbox = client.Folders.Inbox;
            var messages = inbox.Search();
            var table = new List<Anth_MailModel>();
            foreach (Message msg in messages)
            {
                Anth_MailModel ml = new Anth_MailModel();
                ml.MailTo = msg.To;
                ml.MailFrom = msg.From;
                ml.MailBody = msg.Body;
                ml.MailBodyParts = msg.BodyParts;
                ml.MailDate = msg.Date;
                ml.strMailSubject = msg.Subject;
                table.Add(ml);
            }
            client.Logout();
            client.Disconnect();
            return table;
        }

        public IEnumerable<Anth_MailboxModel> GetMailboxList(string server, string account, string pwd)
        {
            var c = DeNSo.Session.New.Get<Anth_ClientModel>(m => m.ClientAlias == server).FirstOrDefault();
            var address = c.ClientAddress;
            var client = new ImapClient(address, true);
            client.Connect();
            client.Login(account, pwd);
            client.Behavior.FolderTreeBrowseMode = FolderTreeBrowseMode.Full;
            var newList = new List<Anth_MailboxModel>();
            var list = client.Folders;
            foreach (Folder folder in list)
            {
                Anth_MailboxModel ml = new Anth_MailboxModel();
                ml.strMailboxAlias = folder.Name;
                newList.Add(ml);
            }
            client.Logout();
            client.Disconnect();
            return newList;
        }

        public IEnumerable<Anth_MailModel> GetMailInMailboxMapped(string server, string account, string pwd, string folder)
        {
            var c = DeNSo.Session.New.Get<Anth_ClientModel>(m => m.ClientAlias == server).FirstOrDefault();
            var address = c.ClientAddress;
            var client = new ImapClient(address, true);
            client.Connect();
            client.Login(account, pwd);
            client.Behavior.MessageFetchMode = MessageFetchMode.Basic;
            client.Behavior.FolderTreeBrowseMode = FolderTreeBrowseMode.Full;
            var messages = client.Folders[folder].Search();
            var table = new List<Anth_MailModel>();
            foreach (Message msg in messages)
            {
                Anth_MailModel ml = new Anth_MailModel();
                ml.MailTo = msg.To;
                ml.MailFrom = msg.From;
                ml.MailBody = msg.Body;
                ml.MailBodyParts = msg.BodyParts;
                ml.MailDate = msg.Date;
                ml.strMailSubject = msg.Subject;
                table.Add(ml);
            }
            client.Logout();
            client.Disconnect();
            return table;
        }

        /// <param name="folder">corrisponde al nome del folder/label</param>
        public IEnumerable<Anth_MailModel> GetMail(string server, string account, string pwd, string folder)
        {
            var c = DeNSo.Session.New.Get<Anth_ClientModel>(m => m.ClientAlias == server).FirstOrDefault();
            var address = c.ClientAddress;
            var client = new ImapClient(address, true);
            client.Connect();
            client.Login(account, pwd);
            client.Behavior.MessageFetchMode = MessageFetchMode.Basic;
            client.Behavior.FolderTreeBrowseMode = FolderTreeBrowseMode.Full;
            var messages = client.Folders[folder].Search();
            var table = new List<Anth_MailModel>();
            foreach (Message msg in messages)
            {
                Anth_MailModel ml = new Anth_MailModel();
                ml.MailTo = msg.To;
                ml.MailFrom = msg.From;
                ml.MailBody = msg.Body;
                ml.MailBodyParts = msg.BodyParts;
                ml.MailDate = msg.Date;
                ml.strMailSubject = msg.Subject;
                table.Add(ml);
            }
            return table;
        }
    }
}