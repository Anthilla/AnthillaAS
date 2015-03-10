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

    public class Anth_ProjectRepository {
        private Anth_Tagger tag = new Anth_Tagger();
        private Anth_Mapper map = new Anth_Mapper();
        public IEnumerable<Anth_ProjectModel> ProjectTable = DeNSo.Session.New.Get<Anth_ProjectModel>(model => model.IsDeleted == false).ToArray();

        public IEnumerable<Anth_Dump> GetAll() {
            var table = new List<Anth_Dump>();
            foreach (Anth_ProjectModel item in ProjectTable) {
                var i = map.ProjectToDump(item);
                table.Add(i);
            }
            Anth_Log.TraceEvent("Project Management", "Information", "tmp", "Project getalldec");
            return table;
        }

        public Anth_Dump GetById(string id) {
            var item = DeNSo.Session.New.Get<Anth_ProjectModel>(c => c.ProjectGuid == id).FirstOrDefault();
            var n = new Anth_Dump();
            if (item == null) {
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("Project Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            var i = map.ProjectToDump(item);
            Anth_Log.TraceEvent("Project Management", "Information", "tmp", "Project getbyId");
            return i;
        }

        public Anth_Dump GetByAlias(string alias) {
            var table = GetAll();
            Anth_Dump project = (from c in table
                                 where c.AnthillaAlias == alias
                                 select c).FirstOrDefault();
            var n = new Anth_Dump();
            if (project == null) {
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("Project Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            Anth_Log.TraceEvent("Project Management", "Information", "tmp", "Project getbyId");
            return project;
        }

        public bool CheckAliasExist(string alias) {
            var table = GetAll();
            Anth_Dump proj = (from c in table
                              where c.AnthillaAlias == alias
                              select c).FirstOrDefault();
            if (proj == null) { return false; }
            else return true;
        }

        public Anth_ProjectModel Create(string guid, string alias, string tags) {
            var model = new Anth_ProjectModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.ProjectId = Guid.NewGuid().ToString();
            model.ProjectGuid = guid;
            var prcode = "";
            Random rnd = new Random(DateTime.Now.Millisecond);
            for (var i = 0; i < 5; i++) {
                prcode += rnd.Next(0, 9).ToString();
            }
            model.ProjectCode = AnthillaSecurity.Encrypt(prcode, model.StorIndexN2, model.StorIndexN1);
            var naming = Anth_Naming.GetByType("project");
            if (naming != null) {
                var na = Anth_NamingActions.SetProjectAlias(alias);
                model.ProjectAlias = AnthillaSecurity.Encrypt(na, model.StorIndexN2, model.StorIndexN1);
            }
            else {
                model.ProjectAlias = AnthillaSecurity.Encrypt(alias, model.StorIndexN2, model.StorIndexN1);
            }

            model.Tags = tag.Tagger(tags).ToList();
            model.IsActive = true;

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
            model.LeaderId = "";
            model.CompanyLeaderId = "";
            model.CompanyGuids = new List<string>() { };
            model.LicenseGuid = "";

            #endregion Nulls

            DeNSo.Session.New.Set(model);
            Anth_Log.TraceEvent("Project Management", "Information", "tmp", "Project Created");
            return model;
        }

        public Anth_ProjectModel Edit(string id, string alias, string tags) {
            var oldItem = DeNSo.Session.New.Get<Anth_ProjectModel>(m => m.ProjectGuid == id && m.IsDeleted == false).FirstOrDefault();
            oldItem.IsDeleted = true;
            oldItem.Aned = "e";
            var model = new Anth_ProjectModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = oldItem.ARelGuid;
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.ProjectId = Guid.NewGuid().ToString();
            model.ProjectGuid = oldItem.ProjectGuid;
            model.ProjectAlias = AnthillaSecurity.Encrypt(alias, model.StorIndexN2, model.StorIndexN1);
            model.Tags = tag.Tagger(tags).ToList();
            model.IsActive = model.IsActive;

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
            model.LeaderId = oldItem.LeaderId;
            model.CompanyLeaderId = oldItem.CompanyLeaderId;
            model.LicenseGuid = oldItem.LicenseGuid;

            #endregion Nulls

            DeNSo.Session.New.Set(oldItem);
            DeNSo.Session.New.Set(model);
            Anth_Log.TraceEvent("Project Management", "Information", "tmp", "Project Edited");
            return model;
        }

        public Anth_ProjectModel Delete(string id) {
            var deletedItem = DeNSo.Session.New.Get<Anth_ProjectModel>(model => model.ProjectGuid == id && model.IsDeleted == false).FirstOrDefault();
            if (deletedItem != null) {
                deletedItem.ADt = DateTime.Now.ToString();
                deletedItem.Aned = "d";
                deletedItem.IsDeleted = true;
                deletedItem.IsActive = false;
                DeNSo.Session.New.Set(deletedItem);
                Anth_Log.TraceEvent("Project Management", "Information", "tmp", "Project Deleted");
            }
            return deletedItem;
        }

        private Anth_UserRepository userrepo = new Anth_UserRepository();

        public Anth_ProjectModel AssignPLeader(string objId, string guid) {
            Anth_ProjectModel project = DeNSo.Session.New.Get<Anth_ProjectModel>(model => model.ProjectGuid == objId && model.IsDeleted == false).FirstOrDefault();
            project.LeaderId = guid;
            DeNSo.Session.New.Set(project);

            userrepo.AssignProject(guid, objId);

            return project;
        }

        public Anth_Dump GetProjectLeader(string projId) {
            Anth_ProjectModel project = DeNSo.Session.New.Get<Anth_ProjectModel>(model => model.ProjectGuid == projId && model.IsDeleted == false).FirstOrDefault();
            string plg = project.LeaderId;
            var u = userrepo.GetById(plg);
            return u;
        }

        public Anth_ProjectModel AssignCompanyLeader(string objId, string guid) {
            Anth_ProjectModel project = DeNSo.Session.New.Get<Anth_ProjectModel>(model => model.ProjectGuid == objId && model.IsDeleted == false).FirstOrDefault();
            project.CompanyLeaderId = guid;
            DeNSo.Session.New.Set(project);
            return project;
        }

        public Anth_ProjectModel AddCompany(string objId, string guid) {
            Anth_ProjectModel project = DeNSo.Session.New.Get<Anth_ProjectModel>(model => model.ProjectGuid == objId && model.IsDeleted == false).FirstOrDefault();
            project.CompanyGuids.Add(guid);
            DeNSo.Session.New.Set(project);
            return project;
        }

        public Anth_ProjectModel RemoveCompany(string objId, string guid) {
            Anth_ProjectModel project = DeNSo.Session.New.Get<Anth_ProjectModel>(model => model.ProjectGuid == objId && model.IsDeleted == false).FirstOrDefault();
            project.CompanyGuids.Remove(guid);
            DeNSo.Session.New.Set(project);
            return project;
        }

        public Anth_ProjectModel AssignLicense(string objId, string guid) {
            Anth_ProjectModel project = DeNSo.Session.New.Get<Anth_ProjectModel>(model => model.ProjectGuid == objId && model.IsDeleted == false).FirstOrDefault();
            project.LicenseGuid = guid;
            DeNSo.Session.New.Set(project);
            return project;
        }
    }
}