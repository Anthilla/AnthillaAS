using AnthillaCore.Logging;
using AnthillaCore.Mapper;
using AnthillaCore.Models;
using AnthillaCore.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnthillaCore.Repository {

    public class Anth_LicenseRepository {
        private Anth_Mapper map = new Anth_Mapper();

        public IEnumerable<Anth_Dump> GetAll() {
            var LicenseTable = DeNSo.Session.New.Get<Anth_LicenseModel>(model => model.IsDeleted == false).ToList();
            var table = new List<Anth_Dump>();
            foreach (Anth_LicenseModel item in LicenseTable) {
                var i = map.LicenseToDump(item);
                table.Add(i);
            }
            Anth_Log.TraceEvent("License Management", "Information", "tmp", "License getalldec");
            return table;
        }

        public Anth_Dump GetById(string id) {
            var item = DeNSo.Session.New.Get<Anth_LicenseModel>(c => c.LicenseGuid == id).FirstOrDefault();
            var n = new Anth_Dump();
            if (item == null) {
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("License Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            var i = map.LicenseToDump(item);
            Anth_Log.TraceEvent("License Management", "Information", "tmp", "License getbyId");
            return i;
        }

        private Anth_ProjectRepository projRepo = new Anth_ProjectRepository();

        public Anth_Dump GetByProject(string projectGuid) {
            var proj = projRepo.GetById(projectGuid);
            var licenseGuid = proj.AnthillaLicenseGuid;
            var item = GetById(licenseGuid);
            var n = new Anth_Dump();
            if (item == null) {
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("License Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            Anth_Log.TraceEvent("License Management", "Information", "tmp", "License GetByProject");
            return item;
        }

        private Anth_FilePackRepository filepkgRepo = new Anth_FilePackRepository();

        public Anth_Dump GetByFilePackage(string filePkgGuid) {
            var filePkg = filepkgRepo.GetById(filePkgGuid);
            var projGuid = filePkg.AnthillaProjectId;
            var proj = projRepo.GetById(projGuid);
            var licenseGuid = proj.AnthillaLicenseGuid;
            var item = GetById(licenseGuid);
            var n = new Anth_Dump();
            if (item == null) {
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("License Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            Anth_Log.TraceEvent("License Management", "Information", "tmp", "License GetByFile");
            return item;
        }

        public Anth_LicenseModel Create(string guid, string title, string text) {
            var model = new Anth_LicenseModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.LicenseId = Guid.NewGuid().ToString();
            model.LicenseGuid = guid;
            model.LicenseTitle = title;
            model.LicenseText = text;

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
            Anth_Log.TraceEvent("License Management", "Information", "tmp", "License Created");
            return model;
        }

        public Anth_LicenseModel Edit(string id, string title, string text) {
            var oldItem = DeNSo.Session.New.Get<Anth_LicenseModel>(m => m.LicenseGuid == id && m.IsDeleted == false).FirstOrDefault();
            oldItem.IsDeleted = true;
            oldItem.Aned = "e";
            var model = new Anth_LicenseModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = oldItem.ARelGuid;
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.LicenseId = Guid.NewGuid().ToString();
            model.LicenseGuid = oldItem.LicenseGuid;
            model.LicenseTitle = title;
            model.LicenseText = text;

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
            Anth_Log.TraceEvent("License Management", "Information", "tmp", "License Edited");
            return model;
        }

        public Anth_LicenseModel Delete(string id) {
            var deletedItem = DeNSo.Session.New.Get<Anth_LicenseModel>(model => model.LicenseGuid == id && model.IsDeleted == false).FirstOrDefault();
            if (deletedItem != null) {
                deletedItem.ADt = DateTime.Now.ToString();
                deletedItem.Aned = "d";
                deletedItem.IsDeleted = true;
                DeNSo.Session.New.Set(deletedItem);
                Anth_Log.TraceEvent("License Management", "Information", "tmp", "License Deleted");
            }
            return deletedItem;
        }
    }
}