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
    public class Anth_UserRepository
    {
        private Anth_Tagger tag = new Anth_Tagger();
        private Anth_Mapper map = new Anth_Mapper();

        public IEnumerable<Anth_Dump> GetAll()
        {
            var newTable = new List<Anth_Dump>();
            List<Anth_UserModel> oldTable = DeNSo.Session.New.Get<Anth_UserModel>().ToList();
            List<Anth_UserModel> filter = (from c in oldTable
                                           where c.IsDeleted == false
                                           select c).ToList();
            foreach (Anth_UserModel item in filter)
            {
                if (item != null)
                {
                    var i = map.UserToDump(item);
                    newTable.Add(i);
                }
            }
            Anth_Log.TraceEvent("Users Management", "Information", "tmp", "Users getalldec");
            return newTable;
        }

        public Anth_Dump GetById(string id)
        {
            var item = DeNSo.Session.New.Get<Anth_UserModel>(c => c.UserGuid == id).FirstOrDefault();
            var n = new Anth_Dump();
            if (item == null)
            {
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("Users Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            var i = map.UserToDump(item);
            Anth_Log.TraceEvent("Users Management", "Information", "tmp", "User getbyId");
            return i;
        }

        public Anth_Dump GetByName(string fName, string lName)
        {
            var table = GetAll();
            Anth_Dump user = (from c in table
                              where c.AnthillaFirstName == fName && c.AnthillaLastName == lName
                              select c).FirstOrDefault();
            if (user == null)
            {
                var n = new Anth_Dump();
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("Users Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            Anth_Log.TraceEvent("Users Management", "Information", "tmp", "User getbyId");
            return user;
        }

        public Anth_Dump GetByAlias(string alias)
        {
            var table = GetAll();
            Anth_Dump user = (from c in table
                              where c.AnthillaAlias == alias
                              select c).FirstOrDefault();
            if (user == null)
            {
                var n = new Anth_Dump();
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("Users Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            Anth_Log.TraceEvent("Users Management", "Information", "tmp", "User getbyId");
            return user;
        }

        public List<Anth_Dump> GetByProject(string guid)
        {
            var newTab = new List<Anth_Dump>();
            var table = GetAll();
            foreach (var u in table)
            {
                string pid = (from p in u.AnthillaProjectIds
                              where p == guid
                              select p).FirstOrDefault();
                if (pid != null)
                {
                    newTab.Add(u);
                }
            }
            Anth_Log.TraceEvent("Users Management", "Information", "tmp", "User get by project");
            return newTab;
        }

        public bool CheckAliasExist(string fName, string lName)
        {
            var table = GetAll();
            Anth_Dump user = (
                from c in table
                where c.AnthillaFirstName == fName && c.AnthillaLastName == lName
                select c
            ).FirstOrDefault();

            if (user == null)
            { return false; }
            else return true;
        }

        public Anth_UserModel Create(string guid, string fName, string lName, string pwd, string email, string tags)
        {
            var model = new Anth_UserModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            model.IsDeleted = false;
            model.StorIndexN2 = AnthillaSecurity.CreateRandomKey();
            model.StorIndexN1 = AnthillaSecurity.CreateRandomVector();
            model.UserId = Guid.NewGuid().ToString();
            model.UserGuid = guid;
            model.UserFirstName = AnthillaSecurity.Encrypt(fName, model.StorIndexN2, model.StorIndexN1);
            model.UserLastName = AnthillaSecurity.Encrypt(lName, model.StorIndexN2, model.StorIndexN1);
            var alias = SetAlias(fName, lName);
            model.UserAlias = AnthillaSecurity.Encrypt(alias, model.StorIndexN2, model.StorIndexN1);

            string pwdHashed = CoreSecurity.AnthillaHash(pwd);
            model.UserPassword = AnthillaSecurity.Encrypt(pwdHashed, model.StorIndexN2, model.StorIndexN1);

            model.UserEmail = AnthillaSecurity.Encrypt(email, model.StorIndexN2, model.StorIndexN1);
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
            model.UserMap = new byte[] { };
            model.UserLanguage = new byte[] { };
            model.CompanyId = "";
            model.ProjectIds = new List<string>() { };
            model.UGroupIds = new List<string>() { };

            #endregion Nulls

            DeNSo.Session.New.Set(model);
            Anth_Log.TraceEvent("Users Management", "Information", "tmp", "User Created");
            return model;
        }

        public Anth_UserModel Edit(string guid, string fName, string lName, string pwd, string email, string tags)
        {
            var oldItem = DeNSo.Session.New.Get<Anth_UserModel>(m => m.UserGuid == guid && m.IsDeleted == false).FirstOrDefault();
            var model = new Anth_UserModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = oldItem.ARelGuid;
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.UserId = Guid.NewGuid().ToString();
            model.UserGuid = oldItem.UserGuid;
            model.UserFirstName = AnthillaSecurity.Encrypt(fName, model.StorIndexN2, model.StorIndexN1);
            model.UserLastName = AnthillaSecurity.Encrypt(lName, model.StorIndexN2, model.StorIndexN1);
            var alias = SetAlias(fName, lName);
            model.UserAlias = AnthillaSecurity.Encrypt(alias, model.StorIndexN2, model.StorIndexN1);

            model.UserEmail = AnthillaSecurity.Encrypt(email, model.StorIndexN2, model.StorIndexN1);
            model.Tags = tag.Tagger(tags).ToList();

            oldItem.IsDeleted = true;
            oldItem.Aned = "e";

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
            model.UserLanguage = oldItem.UserLanguage;
            model.UserMap = oldItem.UserMap;

            #endregion Nulls

            DeNSo.Session.New.Set(oldItem);
            DeNSo.Session.New.Set(model);
            Anth_Log.TraceEvent("Users Management", "Information", "tmp", "User Edited");
            return model;
        }

        public Anth_UserModel Delete(string id)
        {
            var deletedItem = DeNSo.Session.New.Get<Anth_UserModel>(model => model.UserGuid == id).FirstOrDefault();
            if (deletedItem != null)
            {
                deletedItem.ADt = DateTime.Now.ToString();
                deletedItem.Aned = "d";
                deletedItem.IsDeleted = true;
                DeNSo.Session.New.Set(deletedItem);
                Anth_Log.TraceEvent("Users Management", "Information", "tmp", "User Deleted");
            }
            return deletedItem;
        }

        public Anth_UserModel AssignCompany(string objId, string guid)
        {
            Anth_UserModel user = DeNSo.Session.New.Get<Anth_UserModel>(model => model.UserGuid == objId && model.IsDeleted == false).FirstOrDefault();
            user.CompanyId = guid;
            DeNSo.Session.New.Set(user);
            return user;
        }

        public Anth_UserModel AssignProject(string objId, string projGuid)
        {
            Anth_UserModel user = DeNSo.Session.New.Get<Anth_UserModel>(model => model.UserGuid == objId && model.IsDeleted == false).FirstOrDefault();
            user.ProjectIds.Add(projGuid);
            DeNSo.Session.New.Set(user);
            return user;
        }

        public Anth_UserModel AssignGroup(string objId, string groupGuid)
        {
            Anth_UserModel user = DeNSo.Session.New.Get<Anth_UserModel>(model => model.UserGuid == objId && model.IsDeleted == false).FirstOrDefault();
            user.UGroupIds.Add(groupGuid);
            DeNSo.Session.New.Set(user);
            return user;
        }

        public Anth_UserModel ResetPassword(string objId, string password)
        {
            Anth_UserModel user = DeNSo.Session.New.Get<Anth_UserModel>(model => model.UserGuid == objId && model.IsDeleted == false).FirstOrDefault();
            string pwdHashed = CoreSecurity.AnthillaHash(password);
            user.UserPassword = AnthillaSecurity.Encrypt(pwdHashed, user.StorIndexN2, user.StorIndexN1);
            DeNSo.Session.New.Set(user);
            return user;
        }

        private string SetAlias(string firstName, string lastName)
        {
            string fn = firstName.Substring(0, 3);
            string ln = lastName.Substring(0, 3);
            string alias = ln + fn;
            return alias.ToLower();
        }
    }
}