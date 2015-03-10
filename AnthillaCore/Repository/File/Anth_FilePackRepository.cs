using AnthillaCore.FileManagement;
using AnthillaCore.Logging;
using AnthillaCore.Mapper;
using AnthillaCore.Models;
using AnthillaCore.Security;
using AnthillaCore.TagEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnthillaCore.Repository {

    public class Anth_FilePackRepository {
        private Anth_Tagger tag = new Anth_Tagger();
        private Anth_Mapper map = new Anth_Mapper();
        public IEnumerable<Anth_FilePackModel> FileTable = DeNSo.Session.New.Get<Anth_FilePackModel>(m => m.IsDeleted == false && m.isEmpty == false).ToArray();

        public IEnumerable<Anth_Dump> GetAll() {
            var table = new List<Anth_Dump>();
            foreach (Anth_FilePackModel item in FileTable) {
                var i = map.FilePackToDump(item);
                table.Add(i);
            }
            Anth_Log.TraceEvent("File-pack Management", "Information", "tmp", "File-pack getalldec");
            return table;
        }

        public List<Anth_Dump> GetAllShared() {
            var table = GetAll();
            var shsh = (from s in table
                        where s.AnthillaType == "sharing"
                        select s).ToList();
            Anth_Log.TraceEvent("File-pack Management", "Information", "tmp", "File-pack getalldec shared");
            return shsh;
        }

        public IEnumerable<Anth_Dump> GetAllArchived() {
            var table = GetAll();
            var arar = (from s in table
                        where s.AnthillaType == "archive"
                        select s).ToList();
            Anth_Log.TraceEvent("File-pack Management", "Information", "tmp", "File-pack getalldec archived");
            return arar;
        }

        public Anth_Dump GetById(string id) {
            var item = DeNSo.Session.New.Get<Anth_FilePackModel>(c => c.PackageGuid == id && c.IsDeleted == false).FirstOrDefault();
            var n = new Anth_Dump();
            if (item == null) {
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("File-pack Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            var i = map.FilePackToDump(item);
            Anth_Log.TraceEvent("File-pack Management", "Information", "tmp", "File-pack getbyId");
            return i;
        }

        public List<Anth_Dump> GetFileInPackage(string packGuid) {
            var pack = DeNSo.Session.New.Get<Anth_FilePackModel>(c => c.PackageGuid == packGuid).FirstOrDefault();
            var fileList = pack.Files;
            var newList = new List<Anth_Dump>() { };
            foreach (var f in fileList) {
                var file = fileRepo.GetById(f);
                newList.Add(file);
            }
            Anth_Log.TraceEvent("File-pack Management", "Information", "tmp", "Get files in this package.");
            return newList;
        }

        public Anth_FilePackModel SetupPackage(string guid, string owner) {
            var package = new Anth_FilePackModel();
            package.ADt = DateTime.Now.ToString("yyyyMMdd");
            package.Aned = "n";
            package.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            package.IsDeleted = false;
            package.StorIndexN2 = CoreSecurity.CreateRandomKey();
            package.StorIndexN1 = CoreSecurity.CreateRandomVector();
            package._Id = Guid.NewGuid().ToString();
            package.PackageId = Guid.NewGuid().ToString();
            package.PackageGuid = guid;
            package.PackageAlias = "";
            package.PackageType = "";
            package.PackageOwner = owner;
            package.PackageVersion = 0;
            package.PackageMessageDescription = "";
            package.PackageKey = AnthillaSecurity.Encrypt(KeyGenerator.Gen(), package.StorIndexN2, package.StorIndexN1);
            package.Tags = tag.Tagger("").ToList();
            package.PackageUserIds = new List<string>() { };
            package.isEmpty = true;
            DeNSo.Session.New.Set(package);
            return package;
        }

        public Anth_FilePackModel CompletePackage(string type, string guid, string alias, string description, string tags) {
            var package = DeNSo.Session.New.Get<Anth_FilePackModel>(pk => pk.PackageGuid == guid && pk.IsDeleted == false).FirstOrDefault();
            if (package != null) {
                package.PackageType = type;
                package.PackageAlias = alias;
                package.PackageMessageDescription = description;
                package.Tags = tag.Tagger(tags).ToList();
                package.isEmpty = false;
                DeNSo.Session.New.Set(package);
            }
            else {
                return null;
            }
            return package;
        }

        public Anth_FilePackModel Create(string type, string guid, string owner, string alias, string _msg, string tags) {
            var item = DeNSo.Session.New.Get<Anth_FilePackModel>(pk => pk.PackageGuid == guid && pk.IsDeleted == false).FirstOrDefault();
            var model = new Anth_FilePackModel();
            if (item == null) {
                model.ADt = DateTime.Now.ToString("yyyyMMdd");
                model.Aned = "n";
                model.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
                model.IsDeleted = false;
                model.StorIndexN2 = CoreSecurity.CreateRandomKey();
                model.StorIndexN1 = CoreSecurity.CreateRandomVector();
                model._Id = Guid.NewGuid().ToString();
                model.PackageId = Guid.NewGuid().ToString();
                model.PackageGuid = guid;
                model.PackageAlias = alias;
                model.PackageType = type;
                model.PackageOwner = owner;
                model.PackageVersion = 0;
                model.PackageMessageDescription = _msg;
                model.PackageKey = AnthillaSecurity.Encrypt(KeyGenerator.Gen(), model.StorIndexN2, model.StorIndexN1);
                model.Tags = tag.Tagger(tags).ToList();
                model.PackageUserIds = new List<string>() { };
                DeNSo.Session.New.Set(model);
            }
            else {
                model = item;
            }
            return model;
        }

        public Anth_FilePackModel Edit(string guid, string alias, string _msg, string tags) {
            var oldItem = DeNSo.Session.New.Get<Anth_FilePackModel>(pk => pk.PackageGuid == guid && pk.IsDeleted == false).FirstOrDefault();
            oldItem.IsDeleted = true;
            oldItem.Aned = "e";
            DeNSo.Session.New.Set(oldItem);

            var model = oldItem;
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = oldItem.ARelGuid;
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.PackageId = Guid.NewGuid().ToString();
            model.PackageGuid = oldItem.PackageGuid;
            model.PackageAlias = alias;
            model.PackageType = oldItem.PackageType;
            model.PackageOwner = oldItem.PackageOwner;
            model.PackageVersion = oldItem.PackageVersion + 1;
            model.PackageMessageDescription = _msg;
            model.Tags = tag.Tagger(tags).ToList();

            DeNSo.Session.New.Set(model);
            return model;
        }

        private Anth_FileRepository fileRepo = new Anth_FileRepository();

        public Anth_FilePackModel AddFileTo(string pack, string file) {
            var item = DeNSo.Session.New.Get<Anth_FilePackModel>(model => model.PackageGuid == pack && model.IsDeleted == false).FirstOrDefault();
            if (item.Files != null) {
                item.Files.Add(file);
            }
            else {
                var nlist = new List<string>();
                nlist.Add(file);
                item.Files = nlist;
            }

            fileRepo.AssignPackage(file, pack);
            item.isEmpty = false;

            DeNSo.Session.New.Set(item);
            Anth_Log.TraceEvent("File-pack Management", "Information", "tmp", "File add to package");
            return item;
        }

        public Anth_FilePackModel RemoveFile(string pack, string file) {
            var item = DeNSo.Session.New.Get<Anth_FilePackModel>(model => model.PackageGuid == pack && model.IsDeleted == false).FirstOrDefault();
            item.Files.Remove(file);

            fileRepo.RemovePackage(file, pack);

            DeNSo.Session.New.Set(item);
            Anth_Log.TraceEvent("File-pack Management", "Information", "tmp", "File removed from package");
            return item;
        }

        public Anth_FilePackModel Delete(string id) {
            var deletedItem = DeNSo.Session.New.Get<Anth_FilePackModel>(model => model.PackageGuid == id && model.IsDeleted == false).FirstOrDefault();
            if (deletedItem != null) {
                deletedItem.ADt = DateTime.Now.ToString();
                deletedItem.Aned = "d";
                deletedItem.IsDeleted = true;
                DeNSo.Session.New.Set(deletedItem);
                Anth_Log.TraceEvent("File-pack Management", "Information", "tmp", "File-pack Deleted");
            }
            return deletedItem;
        }

        public Anth_FilePackModel AssignProject(string fileGuid, string projectGuid) {
            var file = DeNSo.Session.New.Get<Anth_FilePackModel>(model => model.PackageGuid == fileGuid && model.IsDeleted == false).FirstOrDefault();
            file.PackageProjectId = projectGuid;
            DeNSo.Session.New.Set(file);
            return file;
        }

        public Anth_FilePackModel AssignCompany(string fileGuid, string companyGuid) {
            var file = DeNSo.Session.New.Get<Anth_FilePackModel>(model => model.PackageGuid == fileGuid && model.IsDeleted == false).FirstOrDefault();
            file.PackageCompanyId = companyGuid;
            DeNSo.Session.New.Set(file);
            return file;
        }

        public Anth_FilePackModel AssignUser(string fileGuid, string userGuid) {
            var file = DeNSo.Session.New.Get<Anth_FilePackModel>(model => model.PackageGuid == fileGuid && model.IsDeleted == false).FirstOrDefault();
            file.PackageUserIds.Add(userGuid);
            DeNSo.Session.New.Set(file);
            return file;
        }

        public Anth_FilePackModel IncreaseHierarchyIndex(string guid) {
            var oldItem = DeNSo.Session.New.Get<Anth_FilePackModel>(m => m.PackageGuid == guid && m.IsDeleted == false).FirstOrDefault();
            oldItem.IsDeleted = true;
            oldItem.Aned = "e";
            var model = oldItem;
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.IsDeleted = false;
            model.PackageId = Guid.NewGuid().ToString();

            var oldhi = oldItem.PackageVersion;
            var newhi = oldhi + 1;
            model.PackageVersion = newhi;

            DeNSo.Session.New.Set(oldItem);
            DeNSo.Session.New.Set(model);
            return model;
        }

        public Anth_FilePackModel DecreaseHierarchyIndex(string guid) {
            var oldItem = DeNSo.Session.New.Get<Anth_FilePackModel>(m => m.PackageGuid == guid && m.IsDeleted == false).FirstOrDefault();
            oldItem.IsDeleted = true;
            oldItem.Aned = "e";
            var model = oldItem;
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.IsDeleted = false;
            model.PackageId = Guid.NewGuid().ToString();

            var oldhi = oldItem.PackageVersion;
            var newhi = oldhi - 1;
            model.PackageVersion = newhi;

            DeNSo.Session.New.Set(oldItem);
            DeNSo.Session.New.Set(model);
            return model;
        }

        public string GetKey(string guid) {
            var item = GetById(guid);
            var k = item.AnthillaPackageKey;
            return k;
        }
    }
}