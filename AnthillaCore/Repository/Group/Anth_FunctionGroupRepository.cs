using AnthillaCore.Logging;
using AnthillaCore.Mapper;
using AnthillaCore.Models;
using AnthillaCore.Naming;
using AnthillaCore.Security;
using AnthillaCore.TagEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnthillaCore.Repository {

    public class Anth_FunctionGroupRepository {
        private Anth_Tagger tag = new Anth_Tagger();
        private Anth_Mapper map = new Anth_Mapper();
        public IEnumerable<Anth_FunctionGroupModel> FunctionGroups = DeNSo.Session.New.Get<Anth_FunctionGroupModel>(model => model.IsDeleted == false).ToArray();

        public IEnumerable<Anth_Dump> GetAll() {
            var table = new List<Anth_Dump>();
            foreach (Anth_FunctionGroupModel item in FunctionGroups) {
                var i = map.FunctionGroupToDump(item);
                table.Add(i);
            }
            Anth_Log.TraceEvent("Functions Group Setting", "Information", "tmp", "Functions Group getalldec");
            return table;
        }

        public Anth_Dump GetById(string id) {
            var item = DeNSo.Session.New.Get<Anth_FunctionGroupModel>(c => c.FunctionsGroupGuid == id).FirstOrDefault();
            var n = new Anth_Dump();
            if (item == null) {
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("Functions Group Setting", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            var i = map.FunctionGroupToDump(item);
            Anth_Log.TraceEvent("Functions Group Setting", "Information", "tmp", "Functions Group getbyId");
            return i;
        }

        private Anth_FunctionRepository funcRepo = new Anth_FunctionRepository();

        public List<Anth_Dump> GetFunctionListByGroup(string guid) {
            var item = GetById(guid);
            var functionGuidList = item.AnthillaFunctionGuids;
            var nList = new List<Anth_Dump>() { };
            foreach (var fGuid in functionGuidList) {
                var function = funcRepo.GetById(fGuid);
                nList.Add(function);
            }
            Anth_Log.TraceEvent("Functions Group Setting", "Information", "tmp", "Functions Group get function list");
            return nList;
        }

        public List<string> GetFunctionAliasListByGroup(string guid) {
            var item = GetById(guid);
            var functionGuidList = item.AnthillaFunctionGuids;
            var nList = new List<string>() { };
            foreach (var fGuid in functionGuidList) {
                var function = funcRepo.GetById(fGuid);
                nList.Add(function.AnthillaAlias);
            }
            Anth_Log.TraceEvent("Functions Group Setting", "Information", "tmp", "Functions Group get function alias list");
            return nList;
        }

        public Anth_FunctionGroupModel Create(string guid, string alias, string tags) {
            var model = new Anth_FunctionGroupModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.FunctionsGroupId = Guid.NewGuid().ToString();
            model.FunctionsGroupGuid = guid;

            var naming = Anth_Naming.GetByType("group");
            if (naming != null) {
                var na = Anth_NamingActions.SetProjectAlias(alias);
                model.FunctionsGroupAlias = AnthillaSecurity.Encrypt(na, model.StorIndexN2, model.StorIndexN1);
            }
            else {
                model.FunctionsGroupAlias = AnthillaSecurity.Encrypt(alias, model.StorIndexN2, model.StorIndexN1);
            }

            model.Tags = tag.Tagger(tags).ToList();

            DeNSo.Session.New.Set(model);
            Anth_Log.TraceEvent("Functions Group Setting", "Information", "tmp", "Functions Group Created");
            return model;
        }

        public Anth_FunctionGroupModel Edit(string guid, string alias, string tags) {
            var oldItem = DeNSo.Session.New.Get<Anth_FunctionGroupModel>(m => m.FunctionsGroupGuid == guid && m.IsDeleted == false).FirstOrDefault();
            oldItem.IsDeleted = true;
            oldItem.Aned = "e";
            var model = new Anth_FunctionGroupModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = oldItem.ARelGuid;
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.FunctionsGroupId = Guid.NewGuid().ToString();
            model.FunctionsGroupGuid = oldItem.FunctionsGroupGuid;
            model.FunctionsGroupAlias = AnthillaSecurity.Encrypt(alias, model.StorIndexN2, model.StorIndexN1);
            model.Tags = tag.Tagger(tags).ToList();

            model.FunctionList = new List<string>() { };

            DeNSo.Session.New.Set(oldItem);
            DeNSo.Session.New.Set(model);
            Anth_Log.TraceEvent("Functions Group Setting", "Information", "tmp", "Functions Group Edited");
            return model;
        }

        public Anth_FunctionGroupModel Delete(string id) {
            var deletedItem = DeNSo.Session.New.Get<Anth_FunctionGroupModel>(model => model.FunctionsGroupGuid == id && model.IsDeleted == false).FirstOrDefault();
            if (deletedItem != null) {
                deletedItem.ADt = DateTime.Now.ToString();
                deletedItem.Aned = "d";
                deletedItem.IsDeleted = true;
                DeNSo.Session.New.Set(deletedItem);
                Anth_Log.TraceEvent("Functions Group Setting", "Information", "tmp", "tmp");
            }
            return deletedItem;
        }

        public Anth_FunctionGroupModel AssignFunction(string id, string guid) {
            var group = DeNSo.Session.New.Get<Anth_FunctionGroupModel>(model => model.FunctionsGroupGuid == id && model.IsDeleted == false).FirstOrDefault();
            if (group.FunctionList != null) {
                group.FunctionList.Add(guid);
            }
            else {
                var nlist = new List<string>();
                nlist.Add(guid);
                group.FunctionList = nlist;
            }
            DeNSo.Session.New.Set(group);
            return group;
        }
    }
}