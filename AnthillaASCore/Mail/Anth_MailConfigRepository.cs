using System;
using System.Collections.Generic;
using System.Linq;
using AnthillaASCore.Logging;
using AnthillaASCore.Mapper;
using AnthillaASCore.Models;
using AnthillaASCore.Security;

namespace AnthillaASCore.Mail
{
    public class Anth_MailConfigRepository
    {
        private Anth_Mapper map = new Anth_Mapper();
        public IEnumerable<Anth_MailConfig> MailConfigTable = DeNSo.Session.New.Get<Anth_MailConfig>(model => model.IsDeleted == false).ToArray();

        public IEnumerable<Anth_Dump> GetAll()
        {
            var table = new List<Anth_Dump>();
            foreach (Anth_MailConfig item in MailConfigTable)
            {
                var i = map.MailConfigToDump(item);
                table.Add(i);
            }
            Anth_Log.TraceEvent("MailConfig Management", "Information", "tmp", "MailConfig getalldec");
            return table;
        }

        public Anth_Dump GetById(string userGuid)
        {
            var item = (from i in GetAll()
                        where i.AnthillaUserGuid == userGuid
                        select i).FirstOrDefault();
            var n = new Anth_Dump();
            if (item == null)
            {
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("MailConfig Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            Anth_Log.TraceEvent("MailConfig Management", "Information", "tmp", "MailConfig getbyId");
            return item;
        }

        public Tuple<string, string, string, string> GetSettingByTypeTuple(string userGuid, string type)
        {
            var item = (from i in GetAll()
                        where i.AnthillaUserGuid == userGuid
                        select i).FirstOrDefault();
            var settings = item.AnthillaMailSettingsTuple;
            var setting = (from s in settings
                           where s.Item1 == type
                           select s).FirstOrDefault();
            return setting;
        }

        public string[] GetSettingByTypeArray(string userGuid, string type)
        {
            var item = (from i in GetAll()
                        where i.AnthillaUserGuid == userGuid
                        select i).FirstOrDefault();
            var settings = item.AnthillaMailSettingsArray;
            var setting = (from s in settings
                           where s[0] == type
                           select s).FirstOrDefault();
            return setting;
        }

        public Anth_MailConfig Create(string userGuid)
        {
            var model = new Anth_MailConfig();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.MailConfigId = Guid.NewGuid().ToString();
            model.MailConfigGuid = Guid.NewGuid().ToString();
            model.UserGuid = userGuid;

            DeNSo.Session.New.Set(model);
            Anth_Log.TraceEvent("MailConfig Management", "Information", "tmp", "MailConfig Created");
            return model;
        }

        public Anth_MailConfig CreateSetting(string userGuid, string type, string url, string account, string password)
        {
            var item = DeNSo.Session.New.Get<Anth_MailConfig>(i => i.UserGuid == userGuid && i.IsDeleted == false).FirstOrDefault();
            if (item == null)
            {
                var a = Create(userGuid);
            }
            var k = item.StorIndexN2;
            var v = item.StorIndexN1;

            var set = new MailSetting();
            set.Type = AnthillaSecurity.Encrypt(type, k, v);
            set.ImapUrl = AnthillaSecurity.Encrypt(url, k, v);
            set.Account = AnthillaSecurity.Encrypt(account, k, v);
            set.Password = AnthillaSecurity.Encrypt(password, k, v);

            item.Setting.Add(set);

            DeNSo.Session.New.Set(item);
            return item;
        }

        public Anth_MailConfig Delete(string id)
        {
            var deletedItem = DeNSo.Session.New.Get<Anth_MailConfig>(model => model.MailConfigGuid == id && model.IsDeleted == false).FirstOrDefault();
            if (deletedItem != null)
            {
                deletedItem.ADt = DateTime.Now.ToString();
                deletedItem.Aned = "d";
                deletedItem.IsDeleted = true;
                DeNSo.Session.New.Set(deletedItem);
                Anth_Log.TraceEvent("MailConfig Management", "Information", "tmp", "MailConfig Deleted");
            }
            return deletedItem;
        }
    }
}