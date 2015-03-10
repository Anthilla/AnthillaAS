using AnthillaCore.Logging;
using AnthillaCore.Mapper;
using AnthillaCore.Models;
using AnthillaCore.Security;
using AnthillaCore.TagEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnthillaCore.Mail {

    public class Anth_MailboxRepository {
        private Anth_Tagger tag = new Anth_Tagger();
        private Anth_Mapper map = new Anth_Mapper();
        public IEnumerable<Anth_MailboxModel> MailboxTable = DeNSo.Session.New.Get<Anth_MailboxModel>(model => model.IsDeleted == false).ToArray();

        public IEnumerable<Anth_Dump> GetAll() {
            var table = new List<Anth_Dump>();
            foreach (Anth_MailboxModel item in MailboxTable) {
                var i = map.MailboxToDump(item);
                table.Add(i);
            }
            Anth_Log.TraceEvent("Mailbox Management", "Information", "tmp", "Mailbox getalldec");
            return table;
        }

        public Anth_Dump GetById(string id) {
            var item = DeNSo.Session.New.Get<Anth_MailboxModel>(c => c.MailboxGuid == id).FirstOrDefault();
            var n = new Anth_Dump();
            if (item == null) {
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("Mailbox Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            var i = map.MailboxToDump(item);
            Anth_Log.TraceEvent("Mailbox Management", "Information", "tmp", "Mailbox getbyId");
            return i;
        }

        public Anth_Dump GetByAlias(string alias) {
            var table = GetAll();
            Anth_Dump item = (from c in table
                              where c.AnthillaAlias == alias
                              select c).FirstOrDefault();
            var n = new Anth_Dump();
            if (item == null) {
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("Mailbox Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            Anth_Log.TraceEvent("Mailbox Management", "Information", "tmp", "Mailbox getbyId");
            return item;
        }

        public Anth_MailboxModel Create(string alias, string tags) {
            var model = new Anth_MailboxModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.MailboxId = Guid.NewGuid().ToString();
            model.MailboxGuid = Guid.NewGuid().ToString();
            model.MailboxAlias = AnthillaSecurity.Encrypt(alias, model.StorIndexN2, model.StorIndexN1);
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
            Anth_Log.TraceEvent("Mailbox Management", "Information", "tmp", "Mailbox Created");
            return model;
        }

        public Anth_MailboxModel Edit(string id, string alias, string tags) {
            var oldItem = DeNSo.Session.New.Get<Anth_MailboxModel>(m => m.MailboxGuid == id && m.IsDeleted == false).FirstOrDefault();
            oldItem.IsDeleted = true;
            oldItem.Aned = "e";
            var model = new Anth_MailboxModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = oldItem.ARelGuid;
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.MailboxId = Guid.NewGuid().ToString();
            model.MailboxGuid = oldItem.MailboxGuid;
            model.MailboxAlias = AnthillaSecurity.Encrypt(alias, model.StorIndexN2, model.StorIndexN1);
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
            Anth_Log.TraceEvent("Mailbox Management", "Information", "tmp", "Mailbox Edited");
            return model;
        }

        public Anth_MailboxModel Delete(string id) {
            var deletedItem = DeNSo.Session.New.Get<Anth_MailboxModel>(model => model.MailboxGuid == id && model.IsDeleted == false).FirstOrDefault();
            if (deletedItem != null) {
                deletedItem.ADt = DateTime.Now.ToString();
                deletedItem.Aned = "d";
                deletedItem.IsDeleted = true;
                DeNSo.Session.New.Set(deletedItem);
                Anth_Log.TraceEvent("Mailbox Management", "Information", "tmp", "Mailbox Deleted");
            }
            return deletedItem;
        }
    }
}