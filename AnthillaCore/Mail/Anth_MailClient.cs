using AnthillaCore.Mapper;
using AnthillaCore.Models;
using ImapX;
using ImapX.Enums;
using System.Collections.Generic;
using System.Linq;

namespace AnthillaCore.Mail {

    public class Anth_MailClient {
        private Anth_Mapper map = new Anth_Mapper();

        public void TestConnection() {
            var client = new ImapClient("imap.gmail.com", true);
            client.Connect();
            client.Login("fimbulsgone@gmail.com", "hagall17");
        }

        public void Login(string server, string account, string pwd) {
            var client = new ImapClient(server, true);
            client.Connect();
            client.Login(account, pwd);
        }

        public Message[] GetAllMailDump(string server, string account, string pwd) {
            var client = new ImapClient(server, true);
            client.Connect();
            client.Login(account, pwd);
            client.Behavior.MessageFetchMode = MessageFetchMode.Full;
            client.Behavior.AutoPopulateFolderMessages = true;
            var inbox = client.Folders.All;
            var messages = inbox.Search();
            client.Logout();
            client.Disconnect();
            return messages;
        }

        public IEnumerable<Anth_MailModel> GetMailList(string server, string account, string pwd) {
            var client = new ImapClient(server, true);
            client.Connect();
            client.Login(account, pwd);
            client.Behavior.MessageFetchMode = MessageFetchMode.Basic;
            client.Behavior.AutoPopulateFolderMessages = true;
            var inbox = client.Folders.All;
            var messages = inbox.Search();
            var table = new List<Anth_MailModel>();
            foreach (Message msg in messages) {
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

        public IEnumerable<Anth_MailboxModel> GetMailboxList(string server, string account, string pwd) {
            var client = new ImapClient(server, true);
            client.Connect();
            client.Login(account, pwd);
            client.Behavior.FolderTreeBrowseMode = FolderTreeBrowseMode.Full;
            var newList = new List<Anth_MailboxModel>();
            var list = client.Folders;
            foreach (Folder folder in list) {
                Anth_MailboxModel ml = new Anth_MailboxModel();
                ml.strMailboxAlias = folder.Name;
                newList.Add(ml);
            }
            client.Logout();
            client.Disconnect();
            return newList;
        }

        public string[] GetMailboxArray(string server, string account, string pwd) {
            var client = new ImapClient(server, true);
            client.Connect();
            client.Login(account, pwd);
            client.Behavior.FolderTreeBrowseMode = FolderTreeBrowseMode.Full;
            Folder[] list = client.Folders.ToArray();
            List<string> listStr = new List<string>();
            foreach (Folder folder in list) {
                listStr.Add(folder.Name);
            }
            string[] strArr = listStr.ToArray();
            client.Logout();
            client.Disconnect();
            return strArr;
        }

        public IEnumerable<Anth_MailModel> GetMailListInMailbox(string server, string account, string pwd, string folder) {
            var client = new ImapClient(server, true);
            client.Connect();
            client.Login(account, pwd);
            client.Behavior.MessageFetchMode = MessageFetchMode.Basic;
            client.Behavior.FolderTreeBrowseMode = FolderTreeBrowseMode.Full;
            var messages = client.Folders[folder].Search();
            var table = new List<Anth_MailModel>();
            foreach (Message msg in messages) {
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

        public List<Anth_Dump> GetMailListInMailboxDump(string server, string account, string pwd, string folder) {
            var client = new ImapClient(server, true);
            client.Connect();
            client.Login(account, pwd);
            client.Behavior.MessageFetchMode = MessageFetchMode.Basic;
            client.Behavior.FolderTreeBrowseMode = FolderTreeBrowseMode.Full;
            var messages = client.Folders[folder].Search();
            var table = new List<Anth_Dump>();
            foreach (Message msg in messages) {
                var d = map.ImapXMailToDump(msg);
                table.Add(d);
            }
            client.Logout();
            client.Disconnect();
            return table;
        }

        public void MoveMessage(string server, string account, string pwd, long messageUID, string fromFolder, string toFolder) {
            var client = new ImapClient(server, true);
            client.Connect();
            client.Login(account, pwd);
            client.Behavior.MessageFetchMode = MessageFetchMode.Basic;
            client.Behavior.FolderTreeBrowseMode = FolderTreeBrowseMode.Full;
            var messages = client.Folders[fromFolder].Search();
            ImapX.Message message = (from m in messages
                                     where m.UId == messageUID
                                     select m).FirstOrDefault();
            var folders = client.Folders.ToList();
            ImapX.Folder folder = (from f in folders
                                   where f.Name == toFolder
                                   select f).FirstOrDefault();
            message.MoveTo(folder);
        }

        public void RemoveMessage(string server, string account, string pwd, string folder, long messageUID) {
            var client = new ImapClient(server, true);
            client.Connect();
            client.Login(account, pwd);
            client.Behavior.MessageFetchMode = MessageFetchMode.Basic;
            client.Behavior.FolderTreeBrowseMode = FolderTreeBrowseMode.Full;
            var messages = client.Folders[folder].Search();
            ImapX.Message message = (from m in messages
                                     where m.UId == messageUID
                                     select m).FirstOrDefault();
            message.Remove();
        }

        public void MarkMessageAsReadUnread(string server, string account, string pwd, string messageGuid) {
            var client = new ImapClient(server, true);
            client.Connect();
            client.Login(account, pwd);
            client.Behavior.MessageFetchMode = MessageFetchMode.Basic;
            client.Behavior.FolderTreeBrowseMode = FolderTreeBrowseMode.Full;
            var inbox = client.Folders.All;
            var messages = inbox.Search();
            var message = (from m in messages
                           where m.MessageId == messageGuid
                           select m).FirstOrDefault();
            if (message.Seen) {
                message.Seen = false;
            }
            else {
                message.Seen = true;
            }
        }

        public List<Anth_Dump> TestFolder(string server, string account, string pwd) {
            var client = new ImapClient(server, true);
            client.Connect();
            client.Login(account, pwd);
            var folders = client.Folders.ToArray();
            var list = new List<Anth_Dump>() { };
            foreach (var f in folders) {
                var c = map.GmailMailboxToDump(f);
                list.Add(c);
            }
            client.Logout();
            client.Disconnect();
            return list;
        }
    }
}