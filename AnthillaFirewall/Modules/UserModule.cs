using AnthillaCore.Compression;
using AnthillaCore.Models;
using AnthillaCore.Repository;
using Nancy;
using System;

namespace AnthillaFirewall {

    public class UserModule : NancyModule {
        private Anth_UserRepository repo = new Anth_UserRepository();
        private Anth_EventRepository eventRepo = new Anth_EventRepository();

        public UserModule()
            : base("/user") {
            Get["/"] = x => {
                var companies = repo.GetAll();
                string json = JsonCompression.Set(companies);
                return json;
            };

            Get["/{id}"] = x => {
                string id = x.id;
                var user = repo.GetById(id);
                string json = JsonCompression.Set(user);
                return json;
            };

            Get["/byproject/{projectGuid}"] = x => {
                string projectGuid = x.projectGuid;
                var user = repo.GetByProject(projectGuid);
                string json = JsonCompression.Set(user);
                return json;
            };

            Get["/bycompany/{companyGuid}"] = x => {
                string companyGuid = x.companyGuid;
                var user = repo.GetByCompany(companyGuid);
                string json = JsonCompression.Set(user);
                return json;
            };

            Get["/{guid}/{firstname}/{lastname}/{password}/{email}/{tags}"] = x => {
                string guid = x.guid;
                string f = x.firstname;
                string l = x.lastname;
                string p = x.password;
                string t = x.tags;
                string e = x.email;
                dynamic user;
                user = new Anth_Dump();
                bool aliasExists = repo.CheckAliasExist(f, l);
                if (aliasExists == false) {
                    user = repo.Create(guid, f, l, p, e, t);
                }
                else if (aliasExists == true) {
                    user = repo.GetByName(f, l);
                }

                string gu = Guid.NewGuid().ToString();
                string al = f + " " + l;
                string eventtype = "usercreation";
                string detail = f + " " + l + " joined Anthilla";
                string day = DateTime.Now.ToString("dd");
                string month = DateTime.Now.ToString("MM");
                string year = DateTime.Now.ToString("yyyy");
                string date = day + "/" + month + "/" + year;
                string s = DateTime.Now.ToString("hh") + "00";
                string eventtags = x.tags + ", user-creation";
                eventRepo.Create("", gu, al, eventtype, detail, date, s, eventtags);

                string json = JsonCompression.Set(user);
                return json;
            };

            Get["/{guid}/{firstname}/{lastname}/{password}/{email}/{tags}/e"] = x => {
                string g = x.guid;
                string f = x.firstname;
                string l = x.lastname;
                string p = x.password;
                string t = x.tags;
                string e = x.email;
                var user = repo.Edit(g, f, l, p, e, t);
                string json = JsonCompression.Set(user);
                return json;
            };

            Get["/{id}/x"] = x => {
                string id = x.id;
                var user = repo.Delete(id);
                string json = JsonCompression.Set(user);
                return json;
            };

            Get["/{guid}/{company}/company"] = x => {
                string guid = x.guid;
                string i = x.company;
                var user = repo.AssignCompany(guid, i);
                string json = JsonCompression.Set(user);
                return json;
            };

            Get["/{guid}/{project}/project"] = x => {
                string guid = x.guid;
                string i = x.project;
                var user = repo.AssignProject(guid, i);
                string json = JsonCompression.Set(user);
                return json;
            };

            Get["/{guid}/{grp}/ugroup"] = x => {
                string guid = x.guid;
                string i = x.grp;
                var user = repo.AssignGroup(guid, i);
                string json = JsonCompression.Set(user);
                return json;
            };

            //-------------
            Get["/snd/{guid}/{company}/company"] = x => {
                string guid = x.guid;
                string i = x.company;
                var user = repo.AssignCompanyByName(guid, i);
                string json = JsonCompression.Set(user);
                return json;
            };

            Get["/snd/{guid}/{project}/project"] = x => {
                string guid = x.guid;
                string i = x.project;
                var user = repo.AssignProjectByName(guid, i);
                string json = JsonCompression.Set(user);
                return json;
            };

            Get["/snd/{guid}/{grp}/ugroup"] = x => {
                string guid = x.guid;
                string i = x.grp;
                var user = repo.AssignGroupByName(guid, i);
                string json = JsonCompression.Set(user);
                return json;
            };
            //-------------

            Get["/{user}/{password}/reset"] = x => {
                string u = x.user;
                string p = x.password;
                var user = repo.ResetPassword(u, p);
                string json = JsonCompression.Set(user);
                return json;
            };

            Get["/imapsetting/{userguid}/{type}/{url}/{account}/{password}"] = x => {
                string userguid = x.userguid;
                string type = x.type;
                string url = x.url;
                string account = x.account;
                string password = x.password;
                var user = repo.ConfigImap(userguid, type, url, account, password);
                string json = JsonCompression.Set(user);
                return json;
            };

            Get["/imapsetting/getfor/{userguid}"] = x => {
                string userguid = x.userguid;
                var settings = repo.GetImapSettingById(userguid);
                string json = JsonCompression.Set(settings);
                return json;
            };
        }
    }
}