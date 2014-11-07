using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using AnthillaASCore.Logging;
using AnthillaASCore.Mapper;
using AnthillaASCore.Models;
using AnthillaASCore.Security;
using AnthillaASCore.TagEngine;

namespace AnthillaASCore.Mail
{
    public class Anth_MailRepository
    {
        private Anth_Tagger tag = new Anth_Tagger();
        private Anth_Mapper map = new Anth_Mapper();
        public IEnumerable<Anth_MailModel> MailTable = DeNSo.Session.New.Get<Anth_MailModel>(model => model.IsDeleted == false).ToArray();

        public IEnumerable<Anth_Dump> GetAll()
        {
            var table = new List<Anth_Dump>();
            foreach (Anth_MailModel item in MailTable)
            {
                var i = map.MailToDump(item);
                table.Add(i);
            }
            Anth_Log.TraceEvent("Mail Management", "Information", "tmp", "Mail getalldec");
            return table;
        }

        public Anth_Dump GetById(string id)
        {
            var item = DeNSo.Session.New.Get<Anth_MailModel>(c => c.MailGuid == id).FirstOrDefault();
            var n = new Anth_Dump();
            if (item == null)
            {
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("Mail Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            var i = map.MailToDump(item);
            Anth_Log.TraceEvent("Mail Management", "Information", "tmp", "Mail getbyId");
            return i;
        }

        public Anth_Dump GetByMailbox(string guid)
        {
            var item = DeNSo.Session.New.Get<Anth_MailModel>(c => c.MailboxId == guid).FirstOrDefault();
            var n = new Anth_Dump();
            if (item == null)
            {
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("Mail Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            var i = map.MailToDump(item);
            Anth_Log.TraceEvent("Mail Management", "Information", "tmp", "Mail getbyId");
            return i;
        }

        public Anth_MailModel Create(string from, string to, string body, string sub, string box, string tags)
        {
            var model = new Anth_MailModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.MailId = Guid.NewGuid().ToString();
            model.MailGuid = Guid.NewGuid().ToString();
            model.strMailFrom = AnthillaSecurity.Encrypt(from, model.StorIndexN2, model.StorIndexN1);
            var tod = AnthillaSecurity.Encrypt(to, model.StorIndexN2, model.StorIndexN1);
            var toTab = new List<byte[]>();
            toTab.Add(tod);
            model.strMailTo = toTab;
            model.strMailBody = AnthillaSecurity.Encrypt(body, model.StorIndexN2, model.StorIndexN1);
            model.MailSubject = AnthillaSecurity.Encrypt(sub, model.StorIndexN2, model.StorIndexN1);
            model.MailboxId = box;
            model.Tags = tag.Tagger(tags).ToList();

            #region Nulls

            model.Attribute001 = "";
            model.Attribute002 = "";
            model.Attribute003 = "";
            model.Attribute004 = "";
            model.Attribute005 = "";
            model.Attribute006 = "";
            model.Attribute007 = "";
            model.Attribute008 = "";
            model.Attribute009 = "";
            model.Attribute010 = "";
            model.TagInput = "";

            #endregion Nulls

            DeNSo.Session.New.Set(model);
            Anth_Log.TraceEvent("Mail Management", "Information", "tmp", "Mail Created");
            return model;
        }

        public Anth_MailModel Import(MailMessage mail, string tags)
        {
            var model = new Anth_MailModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.MailId = Guid.NewGuid().ToString();
            model.MailGuid = Guid.NewGuid().ToString();

            model.strMailFrom = AnthillaSecurity.Encrypt(mail.From.Address, model.StorIndexN2, model.StorIndexN1);

            var toTab = new List<byte[]>();
            foreach (var t in mail.To)
            {
                var tod = AnthillaSecurity.Encrypt(t.Address, model.StorIndexN2, model.StorIndexN1);
                toTab.Add(tod);
            }
            model.strMailTo = toTab;

            model.strMailBody = AnthillaSecurity.Encrypt(mail.Body, model.StorIndexN2, model.StorIndexN1);
            model.MailSubject = AnthillaSecurity.Encrypt(mail.Subject, model.StorIndexN2, model.StorIndexN1);
            model.Tags = tag.Tagger(tags).ToList();

            DeNSo.Session.New.Set(model);
            Anth_Log.TraceEvent("Mail Management", "Information", "tmp", "Mail Imported");
            return model;
        }

        public Anth_MailModel Edit(string id, string from, string to, string body, string sub, string box, string tags)
        {
            var oldItem = DeNSo.Session.New.Get<Anth_MailModel>(m => m.MailGuid == id && m.IsDeleted == false).FirstOrDefault();
            oldItem.IsDeleted = true;
            oldItem.Aned = "e";
            var model = new Anth_MailModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = oldItem.ARelGuid;
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.MailId = Guid.NewGuid().ToString();
            model.MailGuid = oldItem.MailGuid;
            model.strMailFrom = AnthillaSecurity.Encrypt(from, model.StorIndexN2, model.StorIndexN1);
            var tod = AnthillaSecurity.Encrypt(to, model.StorIndexN2, model.StorIndexN1);
            var toTab = new List<byte[]>();
            toTab.Add(tod);
            model.strMailTo = toTab;
            model.strMailBody = AnthillaSecurity.Encrypt(body, model.StorIndexN2, model.StorIndexN1);
            model.MailSubject = AnthillaSecurity.Encrypt(sub, model.StorIndexN2, model.StorIndexN1);
            model.MailboxId = box;
            model.Tags = tag.Tagger(tags).ToList();

            #region Nulls

            model.Attribute001 = oldItem.Attribute001;
            model.Attribute002 = oldItem.Attribute002;
            model.Attribute003 = oldItem.Attribute003;
            model.Attribute004 = oldItem.Attribute004;
            model.Attribute005 = oldItem.Attribute005;
            model.Attribute006 = oldItem.Attribute006;
            model.Attribute007 = oldItem.Attribute007;
            model.Attribute008 = oldItem.Attribute008;
            model.Attribute009 = oldItem.Attribute009;
            model.Attribute010 = oldItem.Attribute010;
            model.TagInput = oldItem.TagInput;

            #endregion Nulls

            DeNSo.Session.New.Set(oldItem);
            DeNSo.Session.New.Set(model);
            Anth_Log.TraceEvent("Mail Management", "Information", "tmp", "Mail Edited");
            return model;
        }

        public Anth_MailModel Delete(string id)
        {
            var deletedItem = DeNSo.Session.New.Get<Anth_MailModel>(model => model.MailGuid == id && model.IsDeleted == false).FirstOrDefault();
            if (deletedItem != null)
            {
                deletedItem.ADt = DateTime.Now.ToString();
                deletedItem.Aned = "d";
                deletedItem.IsDeleted = true;
                DeNSo.Session.New.Set(deletedItem);
                Anth_Log.TraceEvent("Mail Management", "Information", "tmp", "Mail Deleted");
            }
            return deletedItem;
        }
    }
}