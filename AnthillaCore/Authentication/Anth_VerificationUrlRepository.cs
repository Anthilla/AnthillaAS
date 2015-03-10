using AnthillaCore.Logging;
using AnthillaCore.Mapper;
using AnthillaCore.Models;
using AnthillaCore.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnthillaCore {

    public class Anth_VerificationUrlRepository {
        private Anth_Mapper map = new Anth_Mapper();

        public IEnumerable<Anth_Dump> GetAll() {
            var table = DeNSo.Session.New.Get<Anth_VerificationUrlModel>(model => model.IsDeleted == false).ToList();
            var newtable = new List<Anth_Dump>();
            foreach (var item in table) {
                var i = map.VerificationUrlToDump(item);
                newtable.Add(i);
            }
            Anth_Log.TraceEvent("VerificationUrl Management", "Information", "tmp", "VerificationUrl getalldec");
            return newtable;
        }

        public Anth_Dump GetById(string id) {
            var item = DeNSo.Session.New.Get<Anth_VerificationUrlModel>(model => model.VerificationUrlGuid == id && model.IsDeleted == false).FirstOrDefault();

            var n = new Anth_Dump();
            if (item == null) {
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("VerificationUrl Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            var i = map.VerificationUrlToDump(item);
            Anth_Log.TraceEvent("VerificationUrl Management", "Information", "tmp", "VerificationUrl getbyId");
            return i;
        }

        public bool CheckValidity(string guid) {
            bool isValid;
            DateTime dtnow = DateTime.Now;
            var item = DeNSo.Session.New.Get<Anth_VerificationUrlModel>(model => model.VerificationUrlGuid == guid && model.IsDeleted == false).FirstOrDefault();
            DateTime cr = item.UrlCreation;
            DateTime ex = cr.AddHours(item.UrlValidity);
            if (dtnow < ex) {
                isValid = true;
            }
            else {
                Delete(guid);
                isValid = false;
            }
            return isValid;
        }

        public string GetUrl(string guid) {
            var item = DeNSo.Session.New.Get<Anth_VerificationUrlModel>(model => model.VerificationUrlGuid == guid && model.IsDeleted == false).FirstOrDefault();
            var check = CheckValidity(guid);

            if (check == true) {
                return item.VerificationUrl;
            }

            return null;
        }

        public Anth_VerificationUrlModel Create(string guid, string userGuid, string packageGuid, string emailTo) {
            var model = new Anth_VerificationUrlModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.VerificationUrlGuid = guid;
            model.VerificationUrl = packageGuid;
            model.UrlValidity = 8;
            model.UrlCreation = DateTime.Now;
            model.EmailToAddress = emailTo;
            model.UserGuid = userGuid;

            DeNSo.Session.New.Set(model);
            Anth_Log.TraceEvent("VerificationUrl Management", "Information", "tmp", "VerificationUrl Created");
            return model;
        }

        public Anth_VerificationUrlModel Delete(string id) {
            var deletedItem = DeNSo.Session.New.Get<Anth_VerificationUrlModel>(model => model.VerificationUrlGuid == id && model.IsDeleted == false).FirstOrDefault();
            if (deletedItem != null) {
                deletedItem.ADt = DateTime.Now.ToString();
                deletedItem.Aned = "d";
                deletedItem.IsDeleted = true;
                DeNSo.Session.New.Set(deletedItem);
                Anth_Log.TraceEvent("VerificationUrl Management", "Information", "tmp", "VerificationUrl Deleted");
            }
            return deletedItem;
        }
    }
}