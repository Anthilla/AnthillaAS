using AnthillaCore.Logging;
using AnthillaCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnthillaCore.Repository {

    public class Anth_SessionRepository {
        public IEnumerable<Anth_SessionModel> SessionTable = DeNSo.Session.New.Get<Anth_SessionModel>(model => model.IsDeleted == false).ToArray();

        public IEnumerable<Anth_SessionModel> GetAll() {
            Anth_Log.TraceEvent("Session", "Information", "tmp", "Get All Sessions");
            return SessionTable;
        }

        public Anth_SessionModel GetById(string id) {
            Anth_Log.TraceEvent("Session", "Information", "tmp", "Get session by Id: " + id);
            return DeNSo.Session.New.Get<Anth_SessionModel>(c => c.SessionGuid == id).FirstOrDefault();
        }

        public Anth_SessionModel Create(string app, DateTime created, DateTime expires, DateTime lockDate, int lockId, int goTimeout, int locked, int flags, string uid) {
            var model = new Anth_SessionModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.IsDeleted = false;
            model.SessionGuid = Guid.NewGuid().ToString();
            model.ApplicationName = app;
            model.Created = created;
            model.Expires = expires;
            model.LockDate = lockDate;
            model.LockId = lockId;
            model.GoTimeout = goTimeout;
            model.Locked = locked; //si può mettere a 0 (0 == false)
            model.Flags = flags;
            model.UserUid = uid;
            DeNSo.Session.New.Set(model);
            Anth_Log.TraceEvent("Session", "Information", "tmp", "Session " + model.SessionGuid + " Created");
            return model;
        }

        public Anth_SessionModel Edit(string id, string app, DateTime created, DateTime expires, DateTime lockDate, int lockId, int goTimeout, int locked, int flags, string uid) {
            var oldItem = DeNSo.Session.New.Get<Anth_SessionModel>(m => m.SessionGuid == id && m.IsDeleted == false).FirstOrDefault();
            oldItem.IsDeleted = true;
            oldItem.Aned = "e";
            var model = new Anth_SessionModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = oldItem.ARelGuid;
            model.IsDeleted = false;
            model.SessionGuid = Guid.NewGuid().ToString();
            model.ApplicationName = app;
            model.Created = created;
            model.Expires = expires;
            model.LockDate = lockDate;
            model.LockId = lockId;
            model.GoTimeout = goTimeout;
            model.Locked = locked;
            model.Flags = flags;
            model.UserUid = uid;
            DeNSo.Session.New.Set(oldItem);
            DeNSo.Session.New.Set(model);
            Anth_Log.TraceEvent("Session", "Information", "tmp", "Session " + id + " Edited");
            return model;
        }

        public Anth_SessionModel Delete(string id) {
            var deletedItem = DeNSo.Session.New.Get<Anth_SessionModel>(model => model.SessionGuid == id && model.IsDeleted == false).FirstOrDefault();
            if (deletedItem != null) {
                deletedItem.ADt = DateTime.Now.ToString();
                deletedItem.Aned = "d";
                deletedItem.IsDeleted = true;
                DeNSo.Session.New.Set(deletedItem);
                Anth_Log.TraceEvent("Session", "Information", "tmp", "Session " + id + " Deleted");
            }
            return deletedItem;
        }

        public Anth_SessionModel Lock(string id) {
            var session = DeNSo.Session.New.Get<Anth_SessionModel>(model => model.SessionGuid == id && model.IsDeleted == false).FirstOrDefault();
            session.Locked = 1;
            DeNSo.Session.New.Set(session);
            Anth_Log.TraceEvent("Session", "Information", "tmp", "Session " + id + " Locked");
            return session;
        }

        public Anth_SessionModel GetByUser(string id) {
            var session = DeNSo.Session.New.Get<Anth_SessionModel>(model => model.UserId == id && model.IsDeleted == false).FirstOrDefault();
            Anth_Log.TraceEvent("Session", "Information", "tmp", "Get session by user: " + id);
            return session;
        }

        public int Check(string id, DateTime created, DateTime expires, DateTime lockDate, int locked, string uid) {
            var session = DeNSo.Session.New.Get<Anth_SessionModel>(model => model.SessionGuid == id && model.IsDeleted == false).FirstOrDefault();
            if (session != null && locked == 0 && created < DateTime.Now && expires < lockDate) {
                Anth_Log.TraceEvent("Session", "Information", "tmp", "Session " + id + " is valid");
                return 1;
            }
            else {
                Anth_Log.TraceEvent("Session", "Information", "tmp", "Session " + id + " is NO MORE valid");
                return 0;
            }
        }
    }
}