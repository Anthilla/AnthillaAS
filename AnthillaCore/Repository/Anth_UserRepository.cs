using AnthillaCore.Logging;
using AnthillaCore.Mail;
using AnthillaCore.Mapper;
using AnthillaCore.Models;
using AnthillaCore.Naming;
using AnthillaCore.Security;
using AnthillaCore.TagEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnthillaCore.Repository {

    public class Anth_UserRepository {
        private Anth_Tagger tag = new Anth_Tagger();
        private Anth_Mapper map = new Anth_Mapper();

        public IEnumerable<Anth_Dump> GetAll() {
            var newTable = new List<Anth_Dump>();
            List<Anth_UserModel> oldTable = DeNSo.Session.New.Get<Anth_UserModel>().ToList();
            List<Anth_UserModel> filter = (from c in oldTable
                                           where c.IsDeleted == false
                                           select c).ToList();
            foreach (Anth_UserModel item in filter) {
                if (item != null) {
                    var i = map.UserToDump(item);
                    newTable.Add(i);
                }
            }
            Anth_Log.TraceEvent("Users Management", "Information", "tmp", "Users getalldec");
            return newTable;
        }

        public Anth_Dump GetById(string id) {
            var item = DeNSo.Session.New.Get<Anth_UserModel>(c => c.UserGuid == id).FirstOrDefault();
            var n = new Anth_Dump();
            if (item == null) {
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("Users Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            var i = map.UserToDump(item);
            Anth_Log.TraceEvent("Users Management", "Information", "tmp", "User getbyId");
            return i;
        }

        public Anth_Dump GetByName(string fName, string lName) {
            var table = GetAll();
            Anth_Dump user = (from c in table
                              where c.AnthillaFirstName == fName && c.AnthillaLastName == lName
                              select c).FirstOrDefault();
            if (user == null) {
                var n = new Anth_Dump();
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("Users Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            Anth_Log.TraceEvent("Users Management", "Information", "tmp", "User getbyId");
            return user;
        }

        public Anth_Dump GetByAlias(string alias) {
            var table = GetAll();
            Anth_Dump user = (from c in table
                              where c.AnthillaAlias == alias
                              select c).FirstOrDefault();
            if (user == null) {
                var n = new Anth_Dump();
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("Users Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            Anth_Log.TraceEvent("Users Management", "Information", "tmp", "User getbyId");
            return user;
        }

        public Anth_Dump GetByEmail(string email) {
            var table = GetAll();
            Anth_Dump user = (from c in table
                              where c.AnthillaEmail == email
                              select c).FirstOrDefault();
            if (user == null) {
                var n = new Anth_Dump();
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("Users Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            Anth_Log.TraceEvent("Users Management", "Information", "tmp", "User getbyId");
            return user;
        }

        public List<Anth_Dump> GetByProject(string guid) {
            var newTab = new List<Anth_Dump>();
            var table = GetAll();
            foreach (var u in table) {
                string pid = (from p in u.AnthillaProjectIds
                              where p == guid
                              select p).FirstOrDefault();
                if (pid != null) {
                    newTab.Add(u);
                }
            }
            Anth_Log.TraceEvent("Users Management", "Information", "tmp", "User get by project");
            return newTab;
        }

        public List<Anth_Dump> GetByCompany(string companyGuid) {
            var table = GetAll();
            var user = (from c in table
                        where c.AnthillaCompanyId == companyGuid
                        select c).ToList();
            if (user == null) {
                var ln = new List<Anth_Dump>();
                var n = new Anth_Dump();
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("Users Management", "Error", "tmp", "Item not found, id not exist");
                ln.Add(n);
                return ln;
            }
            Anth_Log.TraceEvent("Users Management", "Information", "tmp", "User get by Company");
            return user;
        }

        public List<Anth_Dump> GetInsiders() {
            var table = GetAll();
            var user = (from c in table
                        where c.AnthillaInsider == true
                        select c).ToList();
            if (user == null) {
                var ln = new List<Anth_Dump>();
                var n = new Anth_Dump();
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("Users Management", "Error", "tmp", "Item not found, id not exist");
                ln.Add(n);
                return ln;
            }
            Anth_Log.TraceEvent("Users Management", "Information", "tmp", "User get insiders");
            return user;
        }

        public bool CheckAliasExist(string fName, string lName) {
            var table = GetAll();
            Anth_Dump user = (
                from c in table
                where c.AnthillaFirstName == fName && c.AnthillaLastName == lName
                select c
            ).FirstOrDefault();

            if (user == null) { return false; }
            else return true;
        }

        private Anth_AddressBookRepository abRepo = new Anth_AddressBookRepository();
        private KeyIdentity_Repository kiRepo = new KeyIdentity_Repository();

        public Anth_UserModel Create(string guid, string fName, string lName, string pwd, string email, string tags) {
            var model = new Anth_UserModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.UserId = Guid.NewGuid().ToString();
            model.UserGuid = guid;
            var alias = SetAlias(fName, lName);
            model.UserAlias = AnthillaSecurity.Encrypt(alias, model.StorIndexN2, model.StorIndexN1);
            var naming = Anth_Naming.GetByType("user");
            if (naming != null) {
                var nfn = Anth_NamingActions.SetUserAlias(fName);
                var nln = Anth_NamingActions.SetUserAlias(lName);
                model.UserFirstName = AnthillaSecurity.Encrypt(nfn, model.StorIndexN2, model.StorIndexN1);
                model.UserLastName = AnthillaSecurity.Encrypt(nln, model.StorIndexN2, model.StorIndexN1);
            }
            else {
                model.UserFirstName = AnthillaSecurity.Encrypt(fName, model.StorIndexN2, model.StorIndexN1);
                model.UserLastName = AnthillaSecurity.Encrypt(lName, model.StorIndexN2, model.StorIndexN1);
            }

            string pwdHashed = CoreSecurity.AnthillaHash(pwd);
            model.UserPassword = AnthillaSecurity.Encrypt(pwdHashed, model.StorIndexN2, model.StorIndexN1);
            model.UserEmail = AnthillaSecurity.Encrypt(email, model.StorIndexN2, model.StorIndexN1);
            model.Tags = tag.Tagger(tags).ToList();
            var ab = abRepo.GetCheck(guid);
            if (ab == false) {
                abRepo.CreateBook(guid);
            }

            kiRepo.Create(guid);

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

        public Anth_UserModel Edit(string guid, string fName, string lName, string pwd, string email, string tags) {
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

        public void MakeInsider(string id) {
            var user = DeNSo.Session.New.Get<Anth_UserModel>(model => model.UserGuid == id).FirstOrDefault();
            if (user != null) {
                user.Insider = true;
                DeNSo.Session.New.Set(user);
                Anth_Log.TraceEvent("Users Management", "Information", "tmp", "User made insider");
            }
        }

        public void MakeOutsider(string id) {
            var user = DeNSo.Session.New.Get<Anth_UserModel>(model => model.UserGuid == id).FirstOrDefault();
            if (user != null) {
                user.Insider = false;
                DeNSo.Session.New.Set(user);
                Anth_Log.TraceEvent("Users Management", "Information", "tmp", "User made outsider");
            }
        }

        public Anth_UserModel Delete(string id) {
            var deletedItem = DeNSo.Session.New.Get<Anth_UserModel>(model => model.UserGuid == id).FirstOrDefault();
            if (deletedItem != null) {
                deletedItem.ADt = DateTime.Now.ToString();
                deletedItem.Aned = "d";
                deletedItem.IsDeleted = true;
                DeNSo.Session.New.Set(deletedItem);
                Anth_Log.TraceEvent("Users Management", "Information", "tmp", "User Deleted");
            }
            return deletedItem;
        }

        public Anth_UserModel AssignCompany(string objId, string compguid) {
            Anth_UserModel user = DeNSo.Session.New.Get<Anth_UserModel>(model => model.UserGuid == objId && model.IsDeleted == false).FirstOrDefault();
            user.CompanyId = compguid;
            DeNSo.Session.New.Set(user);
            MakeInsider(objId);
            return user;
        }

        public Anth_UserModel AssignProject(string objId, string projGuid) {
            Anth_UserModel user = DeNSo.Session.New.Get<Anth_UserModel>(model => model.UserGuid == objId && model.IsDeleted == false).FirstOrDefault();
            if (!user.ProjectIds.Contains(projGuid)) {
                user.ProjectIds.Add(projGuid);
            }
            DeNSo.Session.New.Set(user);
            return user;
        }

        public Anth_UserModel AssignGroup(string objId, string groupGuid) {
            Anth_UserModel user = DeNSo.Session.New.Get<Anth_UserModel>(model => model.UserGuid == objId && model.IsDeleted == false).FirstOrDefault();
            if (!user.UGroupIds.Contains(groupGuid)) {
                user.UGroupIds.Add(groupGuid);
            }
            DeNSo.Session.New.Set(user);
            return user;
        }

        //<<<<<<<<<
        public Anth_UserModel AssignCompanyByName(string objId, string compAlias) {
            Anth_CompanyRepository compRepo = new Anth_CompanyRepository();
            Anth_UserModel user = DeNSo.Session.New.Get<Anth_UserModel>(model => model.UserGuid == objId && model.IsDeleted == false).FirstOrDefault();
            var company = compRepo.GetByAlias(compAlias);
            user.CompanyId = company.AnthillaGuid;
            DeNSo.Session.New.Set(user);
            MakeInsider(objId);
            return user;
        }

        public Anth_UserModel AssignProjectByName(string objId, string projAlias) {
            Anth_ProjectRepository projRepo = new Anth_ProjectRepository();
            Anth_UserModel user = DeNSo.Session.New.Get<Anth_UserModel>(model => model.UserGuid == objId && model.IsDeleted == false).FirstOrDefault();
            var project = projRepo.GetByAlias(projAlias);
            user.ProjectIds.Add(project.AnthillaGuid);
            DeNSo.Session.New.Set(user);
            return user;
        }

        public Anth_UserModel AssignGroupByName(string objId, string groupAlias) {
            Anth_UserGroupRepository groupRepo = new Anth_UserGroupRepository();
            Anth_UserModel user = DeNSo.Session.New.Get<Anth_UserModel>(model => model.UserGuid == objId && model.IsDeleted == false).FirstOrDefault();
            var ugroup = groupRepo.GetByAlias(groupAlias);
            user.UGroupIds.Add(ugroup.AnthillaGuid);
            DeNSo.Session.New.Set(user);
            return user;
        }

        //<<<<<<<<<

        public Anth_UserModel ResetPassword(string objId, string password) {
            Anth_UserModel user = DeNSo.Session.New.Get<Anth_UserModel>(model => model.UserGuid == objId && model.IsDeleted == false).FirstOrDefault();
            user.UserPassword = null;
            string pwdHashed = CoreSecurity.AnthillaHash(password);
            user.UserPassword = AnthillaSecurity.Encrypt(pwdHashed, user.StorIndexN2, user.StorIndexN1);
            DeNSo.Session.New.Set(user);
            return user;
        }

        private string SetAlias(string firstName, string lastName) {
            string fn = firstName.Substring(0, 3);
            string ln = lastName.Replace(" ", "").Substring(0, 3);
            string alias = ln + fn;
            return alias.ToLower();
        }

        public Anth_UserModel ConfigImap(string userGuid, string type, string url, string account, string password) {
            Anth_UserModel user = DeNSo.Session.New.Get<Anth_UserModel>(model => model.UserGuid == userGuid && model.IsDeleted == false).FirstOrDefault();

            var key = user.StorIndexN2;
            var vec = user.StorIndexN1;

            List<MailSetting> setList = new List<MailSetting>();
            if (user.ImapMailSetting != null) {
                setList = user.ImapMailSetting;
            }
            var set = new MailSetting();
            set.Type = AnthillaSecurity.Encrypt(type, key, vec);
            set.ImapUrl = AnthillaSecurity.Encrypt(url, key, vec);
            set.Account = AnthillaSecurity.Encrypt(account, key, vec);
            set.Password = AnthillaSecurity.Encrypt(password, key, vec);
            setList.Add(set);
            user.ImapMailSetting = setList;
            Anth_Log.TraceEvent("User Management", "Information", "tmp", "Set user imap setting");
            DeNSo.Session.New.Set(user);
            return user;
        }

        public List<string[]> GetImapSettingById(string userGuid) {
            var empty = new List<string[]>() { };
            if (userGuid == "00000000-0000-0000-0000-000000000500") {
                return empty;
            }
            var list = new List<string[]>();
            Anth_UserModel user = DeNSo.Session.New.Get<Anth_UserModel>(model => model.UserGuid == userGuid && model.IsDeleted == false).FirstOrDefault();
            var key = user.StorIndexN2;
            var vec = user.StorIndexN1;
            List<MailSetting> settings = user.ImapMailSetting;
            if (settings == null) {
                return empty;
            }
            foreach (var s in settings) {
                var mappedSetting = map.MailSettingToDumpArray(s, key, vec);
                list.Add(mappedSetting);
            }
            Anth_Log.TraceEvent("User Management", "Information", "tmp", "Get user imap setting");
            return list;
        }
    }
}