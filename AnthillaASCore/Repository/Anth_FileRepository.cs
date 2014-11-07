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
    public class Anth_FileRepository
    {
        private Anth_Tagger tag = new Anth_Tagger();
        private Anth_Mapper map = new Anth_Mapper();
        public IEnumerable<Anth_FileModel> FileTable = DeNSo.Session.New.Get<Anth_FileModel>(m => m.IsDeleted == false).ToArray();

        public IEnumerable<Anth_Dump> GetAll()
        {
            var table = new List<Anth_Dump>();
            foreach (Anth_FileModel item in FileTable)
            {
                var i = map.FileToDump(item);
                table.Add(i);
            }
            Anth_Log.TraceEvent("File Management", "Information", "tmp", "File getalldec");
            return table;
        }

        public Anth_Dump GetById(string id)
        {
            var item = DeNSo.Session.New.Get<Anth_FileModel>(c => c.FileGuid == id).FirstOrDefault();
            var n = new Anth_Dump();
            if (item == null)
            {
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("File Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            var i = map.FileToDump(item);
            Anth_Log.TraceEvent("File Management", "Information", "tmp", "File getbyId");
            return i;
        }

        public Anth_Dump GetByAlias(string alias)
        {
            var list = GetAll();
            var item = (from i in list
                        where i.AnthillaAlias == alias
                        select i).FirstOrDefault();
            var n = new Anth_Dump();
            if (item == null)
            {
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("File Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            Anth_Log.TraceEvent("File Management", "Information", "tmp", "File getbyId");
            return item;
        }

        public string GetDbName(string guid)
        {
            var item = GetById(guid);
            var n = item.AnthillaAlias;
            return n;
        }

        public byte[] GetKey(string guid)
        {
            var item = DeNSo.Session.New.Get<Anth_FileModel>(c => c.FileGuid == guid && c.IsDeleted == false).FirstOrDefault();
            var k = item.StorIndexN2;
            return k;
        }

        public byte[] GetVector(string guid)
        {
            var item = DeNSo.Session.New.Get<Anth_FileModel>(c => c.FileGuid == guid && c.IsDeleted == false).FirstOrDefault();
            var v = item.StorIndexN1;
            return v;
        }

        public string GetSubPath(string guid)
        {
            Anth_FileModel item = DeNSo.Session.New.Get<Anth_FileModel>(c => c.FileGuid == guid && c.IsDeleted == false).FirstOrDefault();
            DateTime date = item.FileCreated;
            string y = date.ToString("yyyy");
            string m = date.ToString("MM");
            string d = date.ToString("dd");
            string subpath = y + @"\" + m + @"\" + d + @"\";
            return subpath;
        }

        public Anth_FileModel Create(string guid, string context, string userId, string alias, string fileType, string oalias, string length, string key, string vector)
        {
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
            model.FileOAlias = AnthillaSecurity.Encrypt(oalias, model.StorIndexN2, model.StorIndexN1);
            model.FilePath = AnthillaSecurity.Encrypt("  ", model.StorIndexN2, model.StorIndexN1);
            model.FileDimension = AnthillaSecurity.Encrypt(" ", model.StorIndexN2, model.StorIndexN1);
            model.FileLenght = AnthillaSecurity.Encrypt(length, model.StorIndexN2, model.StorIndexN1);
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

        public Anth_FileModel AddTags(string guid, string tags)
        {
            var item = DeNSo.Session.New.Get<Anth_FileModel>(model => model.FileGuid == guid && model.IsDeleted == false).FirstOrDefault();
            item.Tags = tag.Tagger(tags).ToList();
            DeNSo.Session.New.Set(item);
            return item;
        }

        public Anth_FileModel Edit(string id, string alias, string tags)
        {
            var oldItem = DeNSo.Session.New.Get<Anth_FileModel>(m => m.FileGuid == id && m.IsDeleted == false).FirstOrDefault();
            oldItem.IsDeleted = true;
            oldItem.Aned = "e";
            var model = new Anth_FileModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = oldItem.ARelGuid;
            model.IsDeleted = false;
            model.StorIndexN2 = oldItem.StorIndexN2;
            model.StorIndexN1 = oldItem.StorIndexN2;
            model._Id = Guid.NewGuid().ToString();
            model.FileId = Guid.NewGuid().ToString();
            model.FileGuid = oldItem.FileGuid;
            model.FileAlias = AnthillaSecurity.Encrypt(alias, model.StorIndexN2, model.StorIndexN1);
            model.FileDimension = AnthillaSecurity.Encrypt(" ", model.StorIndexN2, model.StorIndexN1);
            var tmpe = AnthillaSecurity.Decrypt(model.FilePath, model.StorIndexN2, model.StorIndexN1);
            var xt = System.IO.Path.GetExtension(tmpe).ToString();
            model.FileExtension = AnthillaSecurity.Encrypt(xt, model.StorIndexN2, model.StorIndexN1);
            model.FileCreated = DateTime.Now;
            model.FileLastModified = DateTime.Now;

            #region Nulls

            model.FileUserIds = oldItem.FileUserIds;
            model.FileCompanyIds = oldItem.FileUserIds;
            model.FileProjectIds = oldItem.FileUserIds;
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
            Anth_Log.TraceEvent("File Management", "Information", "tmp", "File Edited");
            return model;
        }

        public Anth_FileModel Delete(string id)
        {
            var deletedItem = DeNSo.Session.New.Get<Anth_FileModel>(model => model.FileGuid == id && model.IsDeleted == false).FirstOrDefault();
            if (deletedItem != null)
            {
                deletedItem.ADt = DateTime.Now.ToString();
                deletedItem.Aned = "d";
                deletedItem.IsDeleted = true;
                DeNSo.Session.New.Set(deletedItem);
                Anth_Log.TraceEvent("File Management", "Information", "tmp", "File Deleted");
            }
            return deletedItem;
        }

        public Anth_FileModel AssignCompany(string fileGuid, string companyGuid)
        {
            var file = DeNSo.Session.New.Get<Anth_FileModel>(model => model.FileGuid == fileGuid && model.IsDeleted == false).FirstOrDefault();
            file.FileCompanyIds.Add(companyGuid);
            DeNSo.Session.New.Set(file);
            return file;
        }

        public Anth_FileModel AssignProject(string fileGuid, string projectGuid)
        {
            var file = DeNSo.Session.New.Get<Anth_FileModel>(model => model.FileGuid == fileGuid && model.IsDeleted == false).FirstOrDefault();
            file.FileProjectIds.Add(projectGuid);
            DeNSo.Session.New.Set(file);
            return file;
        }

        public Anth_FileModel AssignUser(string fileGuid, string userGuid)
        {
            var file = DeNSo.Session.New.Get<Anth_FileModel>(model => model.FileGuid == fileGuid && model.IsDeleted == false).FirstOrDefault();
            file.FileUserIds.Add(userGuid);
            DeNSo.Session.New.Set(file);
            return file;
        }
    }
}