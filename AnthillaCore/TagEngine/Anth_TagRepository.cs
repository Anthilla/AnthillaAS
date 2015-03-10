using AnthillaCore.Logging;
using AnthillaCore.Mapper;
using AnthillaCore.Models;
using AnthillaCore.Naming;
using AnthillaCore.Security;
using DeNSo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnthillaCore.TagEngine {

    public class Anth_TagRepository {
        private Anth_Mapper map = new Anth_Mapper();

        private List<Anth_TagModel> TagTable2() {
            var table = Session.New.Get<Anth_TagModel>().ToList();
            List<Anth_TagModel> newTab = (from c in table
                                          where c.IsDeleted == false
                                          select c).ToList();
            return newTab;
        }

        public List<Anth_Dump> GetAll() {
            var tagTable = TagTable2();
            var table = new List<Anth_Dump>();
            foreach (Anth_TagModel item in tagTable) {
                var i = map.TagToDump(item);
                table.Add(i);
            }
            Anth_Log.TraceEvent("Tag Management", "Information", "tmp", "Tag getalldec");
            return table;
        }

        public List<string> GetList() {
            var tagTable = TagTable2();
            var list = new List<string>();
            foreach (Anth_TagModel item in tagTable) {
                var i = map.TagToDump(item);
                list.Add(i.AnthillaAlias);
            }
            Anth_Log.TraceEvent("Tag Management", "Information", "tmp", "Tag get list");
            return list;
        }

        public Anth_Dump GetById(string id) {
            var item = DeNSo.Session.New.Get<Anth_TagModel>(c => c.TagGuid == id).FirstOrDefault();
            var n = new Anth_Dump();
            if (item == null) {
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("Tag Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            var i = map.TagToDump(item);
            Anth_Log.TraceEvent("Tag Management", "Information", "tmp", "Tag getbyId");
            return i;
        }

        public Anth_Dump GetByAlias(string alias) {
            var table = GetAll();
            Anth_Dump company = (from c in table
                                 where c.AnthillaAlias == alias
                                 select c).FirstOrDefault();
            if (company == null) {
                var n = new Anth_Dump();
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("Tag Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            Anth_Log.TraceEvent("Tag Management", "Information", "tmp", "Tag getbyAlias");
            return company;
        }

        public bool CheckAliasExist(string alias) {
            var table = GetAll();
            Anth_Dump company = (from c in table
                                 where c.AnthillaAlias == alias
                                 select c).FirstOrDefault();
            if (company == null) { return false; }
            else return true;
        }

        public Anth_TagModel Create(string alias) {
            var model = new Anth_TagModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.TagId = Guid.NewGuid().ToString();
            model.TagGuid = Guid.NewGuid().ToString();

            var naming = Anth_Naming.GetByType("tag");
            if (naming != null) {
                var na = Anth_NamingActions.SetTagAlias(alias);
                model.TagAlias = AnthillaSecurity.Encrypt(na, model.StorIndexN2, model.StorIndexN1);
            }
            else {
                model.TagAlias = AnthillaSecurity.Encrypt(alias, model.StorIndexN2, model.StorIndexN1);
            }

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
            Anth_Log.TraceEvent("Tag Management", "Information", "tmp", "Tag Created");
            return model;
        }

        public Anth_TagModel Edit(string id, string alias) {
            var oldItem = DeNSo.Session.New.Get<Anth_TagModel>(m => m.TagGuid == id && m.IsDeleted == false).FirstOrDefault();
            oldItem.IsDeleted = true;
            oldItem.Aned = "e";
            var model = new Anth_TagModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = oldItem.ARelGuid;
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.TagId = Guid.NewGuid().ToString();
            model.TagGuid = oldItem.TagGuid;
            model.TagAlias = AnthillaSecurity.Encrypt(alias, model.StorIndexN2, model.StorIndexN1);

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
            Anth_Log.TraceEvent("Tag Management", "Information", "tmp", "Tag Edited");
            return model;
        }

        public Anth_TagModel Delete(string id) {
            var deletedItem = DeNSo.Session.New.Get<Anth_TagModel>(model => model.TagGuid == id && model.IsDeleted == false).FirstOrDefault();
            if (deletedItem != null) {
                deletedItem.ADt = DateTime.Now.ToString();
                deletedItem.Aned = "d";
                deletedItem.IsDeleted = true;
                DeNSo.Session.New.Set(deletedItem);
                Anth_Log.TraceEvent("Tag Management", "Information", "tmp", "Tag Deleted");
            }
            return deletedItem;
        }
    }
}