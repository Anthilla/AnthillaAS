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

    public class Anth_FileRepository {
        private Anth_Tagger tag = new Anth_Tagger();
        private Anth_Mapper map = new Anth_Mapper();
        public IEnumerable<Anth_FileModel> FileTable = DeNSo.Session.New.Get<Anth_FileModel>(m => m.IsDeleted == false).ToArray();

        public IEnumerable<Anth_Dump> GetAll() {
            var table = new List<Anth_Dump>();
            foreach (Anth_FileModel item in FileTable) {
                var i = map.FileToDump(item);
                table.Add(i);
            }
            Anth_Log.TraceEvent("File Management", "Information", "tmp", "File getalldec");
            return table;
        }

        public Anth_Dump GetById(string id) {
            var item = DeNSo.Session.New.Get<Anth_FileModel>(c => c.FileGuid == id && c.IsDeleted == false).FirstOrDefault();
            var n = new Anth_Dump();
            if (item == null) {
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("File Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            var i = map.FileToDump(item);
            Anth_Log.TraceEvent("File Management", "Information", "tmp", "File getbyId");
            return i;
        }

        public Anth_Dump GetByAlias(string alias) {
            var list = GetAll();
            var item = (from i in list
                        where i.AnthillaAlias == alias
                        select i).FirstOrDefault();
            var n = new Anth_Dump();
            if (item == null) {
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("File Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            Anth_Log.TraceEvent("File Management", "Information", "tmp", "File getbyId");
            return item;
        }

        public string GetDbName(string guid) {
            var item = GetById(guid);
            var n = item.AnthillaAlias;
            return n;
        }

        public byte[] GetKey(string guid) {
            var item = DeNSo.Session.New.Get<Anth_FileModel>(c => c.FileGuid == guid && c.IsDeleted == false).FirstOrDefault();
            var k = item.StorIndexN2;
            return k;
        }

        public byte[] GetVector(string guid) {
            var item = DeNSo.Session.New.Get<Anth_FileModel>(c => c.FileGuid == guid && c.IsDeleted == false).FirstOrDefault();
            var v = item.StorIndexN1;
            return v;
        }

        public string GetFileExt(string guid) {
            var item = GetById(guid);
            var v = item.AnthillaExtension;
            return v;
        }

        public string GetSubPath(string guid) {
            Anth_FileModel item = DeNSo.Session.New.Get<Anth_FileModel>(c => c.FileGuid == guid && c.IsDeleted == false).FirstOrDefault();
            DateTime date = item.FileCreated;
            string y = date.ToString("yyyy");
            string m = date.ToString("MM");
            string d = date.ToString("dd");
            string subpath = y + "/" + m + "/" + d + "/";
            return subpath;
        }

        public DateTime GetDate(string guid) {
            Anth_FileModel item = DeNSo.Session.New.Get<Anth_FileModel>(c => c.FileGuid == guid && c.IsDeleted == false).FirstOrDefault();
            DateTime date = item.FileCreated;
            return date;
        }

        public Anth_FileModel Create(string guid, string context, string userId, string alias, string fileType, string oalias, string length, string key, string vector) {
            var model = new Anth_FileModel();
            model.ADt = DateTime.Now.ToString("yyyyMMdd");
            model.Aned = "n";
            model.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            model.IsDeleted = false;
            model.StorIndexN2 = AnthillaSecurity.AnthillaKey(key);
            model.StorIndexN1 = AnthillaSecurity.AnthillaVector(vector);
            model._Id = Guid.NewGuid().ToString();
            model.FileId = Guid.NewGuid().ToString();
            model.FileGuid = guid;
            model.FileContext = context;
            model.FileType = AnthillaSecurity.Encrypt(fileType, model.StorIndexN2, model.StorIndexN1);
            model.FileOwner = AnthillaSecurity.Encrypt(userId, model.StorIndexN2, model.StorIndexN1);
            model.FileAlias = AnthillaSecurity.Encrypt(alias, model.StorIndexN2, model.StorIndexN1);
            model.FileTrackAlias = AnthillaSecurity.Encrypt(alias, model.StorIndexN2, model.StorIndexN1);
            model.FileVersion = 0;
            model.FilePackage = "";

            var naming = Anth_Naming.GetByType("file");
            if (naming != null) {
                var na = Anth_NamingActions.SetFileAlias(oalias);
                model.FileOAlias = AnthillaSecurity.Encrypt(na, model.StorIndexN2, model.StorIndexN1);
            }
            else {
                model.FileOAlias = AnthillaSecurity.Encrypt(oalias, model.StorIndexN2, model.StorIndexN1);
            }

            model.FilePath = AnthillaSecurity.Encrypt("  ", model.StorIndexN2, model.StorIndexN1);
            model.FileLenght = AnthillaSecurity.Encrypt(length, model.StorIndexN2, model.StorIndexN1);
            model.FileDimension = AnthillaSecurity.Encrypt(length, model.StorIndexN2, model.StorIndexN1);
            var tmpe = AnthillaSecurity.Decrypt(model.FilePath, model.StorIndexN2, model.StorIndexN1);
            var xt = System.IO.Path.GetExtension(tmpe).ToString();
            model.FileExtension = AnthillaSecurity.Encrypt(xt, model.StorIndexN2, model.StorIndexN1);
            model.FileCreated = DateTime.Now;
            model.FileLastModified = DateTime.Now;

            #region Nulls

            model.FileUserIds = new List<string>() { };
            model.FileCompanyIds = new List<string>() { };
            model.FileProjectIds = new List<string>() { };
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
            model.Tags = new List<string>() { };

            #endregion Nulls

            DeNSo.Session.New.Set(model);
            return model;
        }

        public Anth_FileModel Edit(string guid, string newalias, string tags) {
            var oldItem = DeNSo.Session.New.Get<Anth_FileModel>(m => m.FileGuid == guid && m.IsDeleted == false).FirstOrDefault();
            oldItem.IsDeleted = true;
            oldItem.Aned = "e";
            var model = oldItem;
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = oldItem.ARelGuid;
            model.IsDeleted = false;
            model.StorIndexN2 = oldItem.StorIndexN2;
            model.StorIndexN1 = oldItem.StorIndexN1;
            model.FileId = Guid.NewGuid().ToString();
            model.FileGuid = oldItem.FileGuid;
            model.FileType = oldItem.FileType;
            model.FileAlias = oldItem.FileAlias;
            model.FileTrackAlias = oldItem.FileTrackAlias;
            model.FileVersion = oldItem.FileVersion + 1;
            model.Tags = tag.Tagger(tags).ToList();

            var naming = Anth_Naming.GetByType("file");
            if (naming != null) {
                var na = Anth_NamingActions.SetFileAlias(newalias);
                model.FileOAlias = AnthillaSecurity.Encrypt(na, model.StorIndexN2, model.StorIndexN1);
            }
            else {
                model.FileOAlias = AnthillaSecurity.Encrypt(newalias, model.StorIndexN2, model.StorIndexN1);
            }

            model.FileLenght = oldItem.FileLenght;
            model.FileDimension = oldItem.FileDimension;
            model.FileExtension = oldItem.FileExtension;
            model.FileLastModified = DateTime.Now;

            DeNSo.Session.New.Set(oldItem);
            DeNSo.Session.New.Set(model);
            return model;
        }

        public void AssignPackage(string file, string package) {
            var item = DeNSo.Session.New.Get<Anth_FileModel>(model => model.FileGuid == file && model.IsDeleted == false).FirstOrDefault();
            item.FilePackage = package;
            DeNSo.Session.New.Set(item);
        }

        public void RemovePackage(string file, string package) {
            var item = DeNSo.Session.New.Get<Anth_FileModel>(model => model.FileGuid == file && model.IsDeleted == false).FirstOrDefault();
            item.FilePackage = "";
            DeNSo.Session.New.Set(item);
        }

        private Anth_NamingActions namin = new Anth_NamingActions();

        public Anth_FileModel SetFileAliasPost(string fileGuid, string projGuid) {
            var item = GetById(fileGuid);
            var _dateTime = DateTime.Now;
            var _projectGuid = projGuid;
            var _fileName = item.AnthillaAlias;
            var version = "";
            var newalias = namin.SetFileAliasPost(_dateTime, _projectGuid, _fileName, version);
            var newmod = DeNSo.Session.New.Get<Anth_FileModel>(model => model.FileGuid == fileGuid && model.IsDeleted == false).FirstOrDefault();
            newmod.FileTrackAlias = AnthillaSecurity.Encrypt(newalias, newmod.StorIndexN2, newmod.StorIndexN1);
            DeNSo.Session.New.Set(newmod);
            return newmod;
        }

        public Anth_FileModel AddTags(string guid, string tags) {
            var item = DeNSo.Session.New.Get<Anth_FileModel>(model => model.FileGuid == guid && model.IsDeleted == false).FirstOrDefault();
            item.Tags = tag.Tagger(tags).ToList();
            DeNSo.Session.New.Set(item);
            return item;
        }

        public Anth_FileModel Delete(string id) {
            var deletedItem = DeNSo.Session.New.Get<Anth_FileModel>(model => model.FileGuid == id && model.IsDeleted == false).FirstOrDefault();
            if (deletedItem != null) {
                deletedItem.ADt = DateTime.Now.ToString();
                deletedItem.Aned = "d";
                deletedItem.IsDeleted = true;
                DeNSo.Session.New.Set(deletedItem);
                Anth_Log.TraceEvent("File Management", "Information", "tmp", "File Deleted");
            }
            return deletedItem;
        }

        public Anth_FileModel AssignCompany(string fileGuid, string companyGuid) {
            var file = DeNSo.Session.New.Get<Anth_FileModel>(model => model.FileGuid == fileGuid && model.IsDeleted == false).FirstOrDefault();
            file.FileCompanyIds.Add(companyGuid);
            DeNSo.Session.New.Set(file);
            return file;
        }

        public Anth_FileModel AssignProject(string fileGuid, string projectGuid) {
            var file = DeNSo.Session.New.Get<Anth_FileModel>(model => model.FileGuid == fileGuid && model.IsDeleted == false).FirstOrDefault();
            file.FileProjectIds.Add(projectGuid);
            DeNSo.Session.New.Set(file);
            return file;
        }

        public Anth_FileModel AssignUser(string fileGuid, string userGuid) {
            var file = DeNSo.Session.New.Get<Anth_FileModel>(model => model.FileGuid == fileGuid && model.IsDeleted == false).FirstOrDefault();
            file.FileUserIds.Add(userGuid);
            DeNSo.Session.New.Set(file);
            return file;
        }

        public Anth_FileModel IncreaseHierarchyIndex(string guid) {
            var oldItem = DeNSo.Session.New.Get<Anth_FileModel>(m => m.FileGuid == guid && m.IsDeleted == false).FirstOrDefault();
            oldItem.IsDeleted = true;
            oldItem.Aned = "e";
            var model = oldItem;
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.IsDeleted = false;
            model.FileId = Guid.NewGuid().ToString();

            var oldhi = oldItem.FileVersion;
            var newhi = oldhi + 1;
            model.FileVersion = newhi;

            DeNSo.Session.New.Set(oldItem);
            DeNSo.Session.New.Set(model);
            return model;
        }

        public Anth_FileModel DecreaseHierarchyIndex(string guid) {
            var oldItem = DeNSo.Session.New.Get<Anth_FileModel>(m => m.FileGuid == guid && m.IsDeleted == false).FirstOrDefault();
            oldItem.IsDeleted = true;
            oldItem.Aned = "e";
            var model = oldItem;
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.IsDeleted = false;
            model.FileId = Guid.NewGuid().ToString();

            var oldhi = oldItem.FileVersion;
            var newhi = oldhi - 1;
            model.FileVersion = newhi;

            DeNSo.Session.New.Set(oldItem);
            DeNSo.Session.New.Set(model);
            return model;
        }

        //public Anth_FileModel AssignPackage(string fileGuid, string packGuid)
        //{
        //    var item = DeNSo.Session.New.Get<Anth_FileModel>(model => model.FileGuid == fileGuid && model.IsDeleted == false).FirstOrDefault();
        //    item.FilePackage = packGuid;
        //    DeNSo.Session.New.Set(item);
        //    Anth_Log.TraceEvent("File Management", "Information", "tmp", "File add to package");
        //    return item;
        //}

        //public List<Anth_Dump> GetByPackage(string packGuid)
        //{
        //    var list = GetAll();
        //    var item = (from i in list
        //                where i.AnthillaFilePackageId == packGuid
        //                select i).ToList();
        //    Anth_Log.TraceEvent("File Management", "Information", "tmp", "File GetByPackage");
        //    return item;
        //}
    }
}