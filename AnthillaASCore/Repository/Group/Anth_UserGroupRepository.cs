using System;
using System.Collections.Generic;
using System.Linq;

using AnthillaASCore.Logging;
using AnthillaASCore.Mapper;
using AnthillaASCore.Models;
using AnthillaASCore.Security;
using AnthillaASCore.TagEngine;

namespace AnthillaASCore.Repositories
{
    public class Anth_UserGroupRepository
    {
        private Anth_Tagger tag = new Anth_Tagger();
        private Anth_Mapper map = new Anth_Mapper();
        public IEnumerable<Anth_UserGroupModel> UsersGroupsTable = DeNSo.Session.New.Get<Anth_UserGroupModel>(model => model.IsDeleted == false).ToArray();

        public IEnumerable<Anth_Dump> GetAll()
        {
            var table = new List<Anth_Dump>();
            foreach (Anth_UserGroupModel item in UsersGroupsTable)
            {
                var i = map.UserGroupToDump(item);
                table.Add(i);
            }
            Anth_Log.TraceEvent("Users Groups Setting", "Information", "tmp", "Functions Group getalldec");
            return table;
        }

        public List<Anth_Dump> GetAllByHier(int index)
        {
            var table = GetAll();
            List<Anth_Dump> ui = (from ug in table
                                  where ug.AnthillaHierarchyIndex == index
                                  select ug).ToList();
            return ui;
        }

        public List<Anth_Dump> GetAllByNest(int index)
        {
            var table = GetAll();
            List<Anth_Dump> ui = (from ug in table
                                  where ug.AnthillaNestedIndex == index && ug.AnthillaNestedIndex > 0
                                  select ug).ToList();
            return ui;
        }

        public List<Anth_Dump> GetAllOrderedByHierIndex()
        {
            var table = GetAll();
            List<Anth_Dump> ui = (from ug in table
                                  orderby ug.AnthillaHierarchyIndex ascending
                                  select ug).ToList();
            return ui;
        }

        public List<Anth_Dump> GetAllOrderedByNestIndex()
        {
            var table = GetAll();
            List<Anth_Dump> ui = (from ug in table
                                  where ug.AnthillaNestedIndex > 0
                                  orderby ug.AnthillaNestedIndex ascending
                                  select ug).ToList();
            return ui;
        }

        public List<Anth_Dump> GetListOrderedByHierIndex(List<Anth_Dump> modelList)
        {
            List<Anth_Dump> ui = (from ug in modelList
                                  orderby ug.AnthillaHierarchyIndex ascending
                                  select ug).ToList();
            return ui;
        }

        public List<Anth_Dump> GetListOrderedByNestIndex(List<Anth_Dump> modelList)
        {
            List<Anth_Dump> ui = (from ug in modelList
                                  where ug.AnthillaNestedIndex > 0
                                  orderby ug.AnthillaNestedIndex ascending
                                  select ug).ToList();
            return ui;
        }

        public Anth_Dump GetById(string id)
        {
            var item = DeNSo.Session.New.Get<Anth_UserGroupModel>(c => c.UsersGroupGuid == id).FirstOrDefault();
            var n = new Anth_Dump();
            if (item == null)
            {
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("Users Groups Setting", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            var i = map.UserGroupToDump(item);
            Anth_Log.TraceEvent("Users Groups Setting", "Information", "tmp", "Company getbyId");
            return i;
        }

        public Anth_Dump GetByAlias(string alias)
        {
            var table = GetAll();
            Anth_Dump ug = (from c in table
                            where c.AnthillaAlias == alias
                            select c).FirstOrDefault();
            if (ug == null)
            {
                var n = new Anth_Dump();
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("Users Groups Setting", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            Anth_Log.TraceEvent("Users Groups Setting", "Information", "tmp", "Company getbyId");
            return ug;
        }

        public Anth_Dump GetByHierIndex(int val)
        {
            var table = GetAll();
            Anth_Dump ug = (from c in table
                            where c.AnthillaHierarchyIndex == val
                            select c).FirstOrDefault();
            Anth_Log.TraceEvent("Users Groups Setting", "Information", "tmp", "Company getby hierarchy index");
            return ug;
        }

        public Anth_Dump GetByNestIndex(int val)
        {
            var table = GetAll();
            Anth_Dump ug = (from c in table
                            where c.AnthillaNestedIndex == val
                            select c).FirstOrDefault();
            Anth_Log.TraceEvent("Users Groups Setting", "Information", "tmp", "Company getby nesting index");
            return ug;
        }

        public bool CheckAliasExist(string alias)
        {
            var table = GetAll();
            Anth_Dump ug = (from c in table
                            where c.AnthillaAlias == alias
                            select c).FirstOrDefault();
            if (ug == null)
            { return false; }
            else return true;
        }

        public Anth_UserGroupModel Create(string guid, string alias, string tags)
        {
            var model = new Anth_UserGroupModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.UsersGroupId = Guid.NewGuid().ToString();
            model.UsersGroupGuid = guid;
            model.UsersGroupAlias = AnthillaSecurity.Encrypt(alias, model.StorIndexN2, model.StorIndexN1);
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
            model.UsersGroupEvent = "";
            model.UsersGroupTrigger = "";
            model.UsersGroupHierarchyIndex = 0;
            model.UsersGroupNestedIndex = 0;

            #endregion Nulls

            DeNSo.Session.New.Set(model);
            Anth_Log.TraceEvent("Users Groups Setting", "Information", "tmp", "Functions Group Created");
            return model;
        }

        public Anth_UserGroupModel SetHierarchyIndex(string guid, int val)
        {
            var ug = DeNSo.Session.New.Get<Anth_UserGroupModel>(model => model.UsersGroupGuid == guid && model.IsDeleted == false).FirstOrDefault();
            ug.UsersGroupHierarchyIndex = val;
            DeNSo.Session.New.Set(ug);
            return ug;
        }

        public Anth_UserGroupModel SetNestedIndex(string guid, int val)
        {
            var ug = DeNSo.Session.New.Get<Anth_UserGroupModel>(model => model.UsersGroupGuid == guid && model.IsDeleted == false).FirstOrDefault();
            ug.UsersGroupNestedIndex = val;
            DeNSo.Session.New.Set(ug);
            return ug;
        }

        public Anth_UserGroupModel Edit(string id, string alias, string tags)
        {
            var oldItem = DeNSo.Session.New.Get<Anth_UserGroupModel>(m => m.UsersGroupGuid == id && m.IsDeleted == false).FirstOrDefault();
            oldItem.IsDeleted = true;
            oldItem.Aned = "e";
            var model = new Anth_UserGroupModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = oldItem.ARelGuid;
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.UsersGroupId = Guid.NewGuid().ToString();
            model.UsersGroupGuid = oldItem.UsersGroupGuid;
            model.UsersGroupAlias = AnthillaSecurity.Encrypt(alias, model.StorIndexN2, model.StorIndexN1);
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
            model.UsersGroupEvent = oldItem.UsersGroupEvent;
            model.UsersGroupTrigger = oldItem.UsersGroupTrigger;
            model.UsersGroupHierarchyIndex = oldItem.UsersGroupHierarchyIndex;
            model.UsersGroupNestedIndex = oldItem.UsersGroupNestedIndex;

            #endregion Nulls

            DeNSo.Session.New.Set(oldItem);
            DeNSo.Session.New.Set(model);
            Anth_Log.TraceEvent("Users Groups Setting", "Information", "tmp", "Functions Group Edited");
            return model;
        }

        public Anth_UserGroupModel Delete(string id)
        {
            var deletedItem = DeNSo.Session.New.Get<Anth_UserGroupModel>(model => model.UsersGroupGuid == id && model.IsDeleted == false).FirstOrDefault();
            if (deletedItem != null)
            {
                deletedItem.ADt = DateTime.Now.ToString();
                deletedItem.Aned = "d";
                deletedItem.IsDeleted = true;
                DeNSo.Session.New.Set(deletedItem);
                Anth_Log.TraceEvent("Users Groups Setting", "Information", "tmp", "tmp");
            }
            return deletedItem;
        }
    }
}