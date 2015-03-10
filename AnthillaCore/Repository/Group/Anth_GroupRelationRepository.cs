using AnthillaCore.Logging;
using AnthillaCore.Mapper;
using AnthillaCore.Models;
using AnthillaCore.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnthillaCore.Repository {

    public class Anth_GroupRelationRepository {
        private Anth_Mapper map = new Anth_Mapper();
        public IEnumerable<Anth_GroupRelationModel> Relations = DeNSo.Session.New.Get<Anth_GroupRelationModel>(model => model.IsDeleted == false).ToArray();

        public IEnumerable<Anth_Dump> GetAll() {
            var table = new List<Anth_Dump>();
            foreach (Anth_GroupRelationModel item in Relations) {
                var i = map.GroupRelationToDump(item);
                table.Add(i);
            }
            Anth_Log.TraceEvent("Groups Relation Setting", "Information", "tmp", "Groups Relation getalldec");
            return table;
        }

        public Anth_Dump GetById(string id) {
            var item = DeNSo.Session.New.Get<Anth_GroupRelationModel>(c => c.GroupRelationGuid == id).FirstOrDefault();
            var n = new Anth_Dump();
            if (item == null) {
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("Groups Relation Setting", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            var i = map.GroupRelationToDump(item);
            Anth_Log.TraceEvent("Groups Relation Setting", "Information", "tmp", "Groups Relation getbyId");
            return i;
        }

        public List<Anth_Dump> GetByUserGroup(string userGroupGuid) {
            var items = GetAll();
            List<Anth_Dump> relList = (from g in items
                                       where g.AnthillaUserGroupId == userGroupGuid
                                       select g).ToList();
            Anth_Log.TraceEvent("Groups Relation Setting", "Information", "tmp", "Groups Relation get by user group");
            return relList;
        }

        public Anth_GroupRelationModel Create(string ugroup, string fgroup) {
            var model = new Anth_GroupRelationModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.GroupRelationId = Guid.NewGuid().ToString();
            model.GroupRelationGuid = Guid.NewGuid().ToString();
            model.UsersRelGuid = ugroup;
            model.FunctionsRelGuid = fgroup;

            DeNSo.Session.New.Set(model);
            Anth_Log.TraceEvent("Groups Relation Setting", "Information", "tmp", "Groups Relation Created");
            return model;
        }

        public void CreateV(string ugroup, string fgroup) {
            var model = new Anth_GroupRelationModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.GroupRelationId = Guid.NewGuid().ToString();
            model.GroupRelationGuid = Guid.NewGuid().ToString();
            model.UsersRelGuid = ugroup;
            model.FunctionsRelGuid = fgroup;

            DeNSo.Session.New.Set(model);
            Anth_Log.TraceEvent("Groups Relation Setting", "Information", "tmp", "Groups Relation Created");
        }

        public Anth_GroupRelationModel Edit(string guid, string ugroup, string fgroup) {
            var oldItem = DeNSo.Session.New.Get<Anth_GroupRelationModel>(m => m.GroupRelationGuid == guid && m.IsDeleted == false).FirstOrDefault();
            oldItem.IsDeleted = true;
            oldItem.Aned = "e";
            var model = new Anth_GroupRelationModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = oldItem.ARelGuid;
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.GroupRelationId = Guid.NewGuid().ToString();
            model.GroupRelationGuid = oldItem.GroupRelationGuid;
            model.UsersRelGuid = ugroup;
            model.FunctionsRelGuid = fgroup;

            DeNSo.Session.New.Set(oldItem);
            DeNSo.Session.New.Set(model);
            Anth_Log.TraceEvent("Groups Relation Setting", "Information", "tmp", "Groups Relation Edited");
            return model;
        }

        public Anth_GroupRelationModel Delete(string guid) {
            var deletedItem = DeNSo.Session.New.Get<Anth_GroupRelationModel>(model => model.GroupRelationGuid == guid && model.IsDeleted == false).FirstOrDefault();
            if (deletedItem != null) {
                deletedItem.ADt = DateTime.Now.ToString();
                deletedItem.Aned = "d";
                deletedItem.IsDeleted = true;
                DeNSo.Session.New.Set(deletedItem);
                Anth_Log.TraceEvent("Groups Relation Setting", "Information", "tmp", "tmp");
            }
            return deletedItem;
        }
    }
}