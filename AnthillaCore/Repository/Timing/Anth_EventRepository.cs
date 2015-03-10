using AnthillaCore.Logging;
using AnthillaCore.Mapper;
using AnthillaCore.Models;
using AnthillaCore.Security;
using AnthillaCore.TagEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnthillaCore.Repository {

    public class Anth_EventRepository {
        private Anth_Tagger tag = new Anth_Tagger();
        private Anth_Mapper map = new Anth_Mapper();

        public Anth_DayModel SetDay(DateTime date) {
            var day = new Anth_DayModel();
            day.ADt = DateTime.Now.ToString();
            day.Aned = "n";
            day.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            day.IsDeleted = false;
            day.DayId = Guid.NewGuid().ToString();
            day.DayGuid = Guid.NewGuid().ToString();

            day.DayName = date.Day.ToString("dddd");
            day.DayInt = date.Day.ToString("dd");
            day.MonthName = date.Month.ToString("MMMM");
            day.MonthInt = date.Month.ToString("MM");
            day.Year = date.Year.ToString("yyyy");

            return day;
        }

        public Anth_DayModel ConvertDay(string date) {
            var day = new Anth_DayModel();
            return day;
        }

        public IEnumerable<Anth_EventModel> EventTable = DeNSo.Session.New.Get<Anth_EventModel>(model => model.IsDeleted == false).ToArray();

        public IEnumerable<Anth_Dump> GetAll() {
            var table = new List<Anth_Dump>();
            foreach (Anth_EventModel item in EventTable) {
                var i = map.EventToDump(item);
                table.Add(i);
            }
            Anth_Log.TraceEvent("Event Management", "Information", "tmp", "Event getalldec");
            return table;
        }

        public IEnumerable<Anth_Dump> GetAllByUser(string userguid) {
            var table = new List<Anth_Dump>();
            var etab = DeNSo.Session.New.Get<Anth_EventModel>(model => model.EventUserGuid == userguid && model.IsDeleted == false).ToArray();
            foreach (Anth_EventModel item in etab) {
                var i = map.EventToDump(item);
                table.Add(i);
            }
            Anth_Log.TraceEvent("Event Management", "Information", "tmp", "Event getallbyuser");
            return table;
        }

        public Anth_Dump GetById(string id) {
            var item = DeNSo.Session.New.Get<Anth_EventModel>(c => c.EventGuid == id).FirstOrDefault();
            var n = new Anth_Dump();
            if (item == null) {
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("Event Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            var i = map.EventToDump(item);
            Anth_Log.TraceEvent("Event Management", "Information", "tmp", "Event getbyId");
            return i;
        }

        public Anth_Dump GetByAlias(string alias) {
            var table = GetAll();
            Anth_Dump item = (from c in table
                              where c.AnthillaAlias == alias
                              select c).FirstOrDefault();
            var n = new Anth_Dump();
            if (item == null) {
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("Event Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            Anth_Log.TraceEvent("Event Management", "Information", "tmp", "Event getbyId");
            return item;
        }

        public Anth_EventModel Create(string userGuid, string guid, string alias, string type, string det, string date, string s, string tags) {
            var model = new Anth_EventModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.EventId = Guid.NewGuid().ToString();
            model.EventGuid = guid;
            model.EventUserGuid = userGuid;
            model.EventAlias = AnthillaSecurity.Encrypt(alias, model.StorIndexN2, model.StorIndexN1);
            model.EventType = AnthillaSecurity.Encrypt(type, model.StorIndexN2, model.StorIndexN1);
            model.EventDetail = AnthillaSecurity.Encrypt(det, model.StorIndexN2, model.StorIndexN1);
            model.EventDate = date;
            model.EventLenght = AnthillaSecurity.Encrypt("l", model.StorIndexN2, model.StorIndexN1);
            model.EventStart = s;
            model.EventEnd = "";
            model.EventCompanyGuid = "";
            model.EventProjectGuid = "";
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

            model.EventUserIds = new List<string>() { };
            model.EventProjectIds = new List<string>() { };
            model.EventCompanyIds = new List<string>() { };

            #endregion Nulls

            DeNSo.Session.New.Set(model);
            Anth_Log.TraceEvent("Event Management", "Information", "tmp", "Event Created");
            return model;
        }

        public Anth_EventModel Edit(string id, string alias, string type, string det, string date, string s, string tags) {
            var oldItem = DeNSo.Session.New.Get<Anth_EventModel>(m => m.EventGuid == id && m.IsDeleted == false).FirstOrDefault();
            oldItem.IsDeleted = true;
            oldItem.Aned = "e";
            var model = new Anth_EventModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = oldItem.ARelGuid;
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.EventId = Guid.NewGuid().ToString();
            model.EventGuid = oldItem.EventGuid;
            model.EventUserGuid = oldItem.EventUserGuid;
            model.EventAlias = AnthillaSecurity.Encrypt(alias, model.StorIndexN2, model.StorIndexN1);
            model.EventType = AnthillaSecurity.Encrypt(type, model.StorIndexN2, model.StorIndexN1);
            model.EventDetail = AnthillaSecurity.Encrypt(det, model.StorIndexN2, model.StorIndexN1);
            model.EventDate = date;
            model.EventLenght = AnthillaSecurity.Encrypt("l", model.StorIndexN2, model.StorIndexN1);
            model.EventStart = s;
            model.EventEnd = "";
            model.Tags = tag.Tagger(tags).ToList();
            model.EventUserIds = oldItem.EventUserIds;
            model.EventProjectIds = oldItem.EventUserIds;
            model.EventCompanyIds = oldItem.EventUserIds;

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
            Anth_Log.TraceEvent("Event Management", "Information", "tmp", "Event Edited");
            return model;
        }

        public Anth_EventModel Delete(string id) {
            var deletedItem = DeNSo.Session.New.Get<Anth_EventModel>(model => model.EventGuid == id && model.IsDeleted == false).FirstOrDefault();
            if (deletedItem != null) {
                deletedItem.ADt = DateTime.Now.ToString();
                deletedItem.Aned = "d";
                deletedItem.IsDeleted = true;
                DeNSo.Session.New.Set(deletedItem);
                Anth_Log.TraceEvent("Event Management", "Information", "tmp", "Event Deleted");
            }
            return deletedItem;
        }

        public Anth_Dump GetByDateHour2(string date, string hour) {
            var item = DeNSo.Session.New.Get<Anth_EventModel>(c => c.EventDate == date && c.EventStart == hour).FirstOrDefault();
            var n = new Anth_Dump();
            if (item == null) {
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("Event Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            var i = map.EventToDump(item);
            Anth_Log.TraceEvent("Event Management", "Information", "tmp", "Event getbydate");
            return i;
        }

        public List<Anth_Dump> GetByDateHour(string date, string hour) {
            var table = DeNSo.Session.New.Get<Anth_EventModel>(c => c.EventDate == date && c.EventStart == hour).ToList();
            var newTable = new List<Anth_Dump>();
            foreach (Anth_EventModel item in table) {
                var i = map.EventToDump(item);
                newTable.Add(i);
            }
            Anth_Log.TraceEvent("Event Management", "Information", "tmp", "Event getalldec");
            return newTable;
        }

        public List<Anth_Dump> GetByDate(string date) {
            var table = DeNSo.Session.New.Get<Anth_EventModel>(c => c.EventDate == date).ToList();
            var newTable = new List<Anth_Dump>();
            foreach (Anth_EventModel item in table) {
                var i = map.EventToDump(item);
                newTable.Add(i);
            }
            Anth_Log.TraceEvent("Event Management", "Information", "tmp", "Event getalldec");
            return newTable;
        }

        public void AssignProject(string evguid, string projguid) {
            var item = DeNSo.Session.New.Get<Anth_EventModel>(c => c.EventGuid == evguid).FirstOrDefault();
            item.EventProjectGuid = projguid;
            DeNSo.Session.New.Set(item);
        }

        public void AssignCompany(string evguid, string compguid) {
            var item = DeNSo.Session.New.Get<Anth_EventModel>(c => c.EventGuid == evguid).FirstOrDefault();
            item.EventCompanyGuid = compguid;
            DeNSo.Session.New.Set(item);
        }
    }
}