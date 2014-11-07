using System;
using System.Collections.Generic;
using System.Linq;
using AnthillaASCore.Logging;
using AnthillaASCore.Mapper;
using AnthillaASCore.Models;
using AnthillaASCore.Security;

namespace AnthillaASCore.Repositories
{
    public class Anth_NamingRepository
    {
        private Anth_Mapper map = new Anth_Mapper();
        public IEnumerable<Anth_NamingModel> NomenTable = DeNSo.Session.New.Get<Anth_NamingModel>(model => model.IsDeleted == false).ToArray();

        public IEnumerable<Anth_Dump> GetAll()
        {
            var table = new List<Anth_Dump>();
            foreach (Anth_NamingModel item in NomenTable)
            {
                var i = map.NamingToDump(item);
                table.Add(i);
            }
            Anth_Log.TraceEvent("Naming Setting", "Information", "tmp", "Naming get all");
            return table;
        }

        public Anth_Dump GetById(string id)
        {
            var item = DeNSo.Session.New.Get<Anth_NamingModel>(c => c.NamingGuid == id).FirstOrDefault();
            var n = new Anth_Dump();
            if (item == null)
            {
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("Naming Setting", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            var i = map.NamingToDump(item);
            Anth_Log.TraceEvent("Naming Setting", "Information", "tmp", "Company getbyId");
            return i;
        }

        public Anth_NamingModel Create(string type, string alias, string prefix, string suffix)
        {
            var model = new Anth_NamingModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.NamingGuid = Guid.NewGuid().ToString();
            model.NamingAlias = AnthillaSecurity.Encrypt(alias, model.StorIndexN2, model.StorIndexN1);
            model.NamingType = type;
            model.Prefix = AnthillaSecurity.Encrypt(prefix, model.StorIndexN2, model.StorIndexN1);
            model.Suffix = AnthillaSecurity.Encrypt(suffix, model.StorIndexN2, model.StorIndexN1);
            Anth_Log.TraceEvent("Naming Setting", "Information", "tmp", "Naming set created");
            DeNSo.Session.New.Set(model);
            return model;
        }

        public Anth_NamingModel Edit(string id, string type, string alias, string prefix, string suffix)
        {
            var oldItem = DeNSo.Session.New.Get<Anth_NamingModel>(m => m.NamingGuid == id && m.IsDeleted == false).FirstOrDefault();
            oldItem.IsDeleted = true;
            oldItem.Aned = "e";
            var model = new Anth_NamingModel();
            model.NamingGuid = oldItem.NamingGuid;
            model.IsDeleted = false;
            model.ARelGuid = oldItem.ARelGuid;
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.NamingAlias = AnthillaSecurity.Encrypt(alias, model.StorIndexN2, model.StorIndexN1);
            model.NamingType = type;
            model.Prefix = AnthillaSecurity.Encrypt(prefix, model.StorIndexN2, model.StorIndexN1);
            model.Suffix = AnthillaSecurity.Encrypt(suffix, model.StorIndexN2, model.StorIndexN1);
            DeNSo.Session.New.Set(oldItem);
            DeNSo.Session.New.Set(model);
            Anth_Log.TraceEvent("Naming Setting", "Information", "tmp", "Naming set edited");
            return model;
        }

        public Anth_NamingModel Delete(string id)
        {
            var deletedItem = DeNSo.Session.New.Get<Anth_NamingModel>(model => model.NamingGuid == id && model.IsDeleted == false).FirstOrDefault();
            if (deletedItem != null)
            {
                deletedItem.ADt = DateTime.Now.ToString();
                deletedItem.Aned = "d";
                deletedItem.IsDeleted = true;
                DeNSo.Session.New.Set(deletedItem);
                Anth_Log.TraceEvent("Naming Setting", "Information", "tmp", "Naming set deleted");
            }
            return deletedItem;
        }

        public string CreateUserAlias(string name, string surname)
        {
            string subnam = name.Substring(0, 3);
            string subsur = name.Substring(0, 3);
            string alias = subnam + subsur;
            return alias;
        }
    }
}