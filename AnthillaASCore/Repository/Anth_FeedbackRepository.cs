using System;
using System.Collections.Generic;
using System.Linq;

using AnthillaASCore.Logging;
using AnthillaASCore.Models;
using AnthillaASCore.Security;

namespace AnthillaASCore.Repositories
{
    public class Anth_FeedbackRepository
    {
        public IEnumerable<Anth_FeedbackModel> FeedbackTable = DeNSo.Session.New.Get<Anth_FeedbackModel>(model => model.IsDeleted == false).ToArray();

        public IEnumerable<Anth_FeedbackModel> GetAll()
        {
            Anth_Log.TraceEvent("Feedback Management", "Information", "tmp", "Feedback getalldec");
            return FeedbackTable;
        }

        public Anth_FeedbackModel GetById(string id)
        {
            var item = DeNSo.Session.New.Get<Anth_FeedbackModel>(c => c.FeedbackGuid == id).FirstOrDefault();
            Anth_Log.TraceEvent("Feedback Management", "Information", "tmp", "Feedback getbyId");
            return item;
        }

        public Anth_FeedbackModel Create(string user, string comment, string type)
        {
            var model = new Anth_FeedbackModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.FeedbackId = Guid.NewGuid().ToString();
            model.FeedbackGuid = Guid.NewGuid().ToString();
            model.User = user; //tmp
            model.Comment = comment;
            model.Type = type;

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
            Anth_Log.TraceEvent("Feedback Management", "Information", "tmp", "Feedback Created");
            return model;
        }

        public Anth_FeedbackModel Delete(string id)
        {
            var deletedItem = DeNSo.Session.New.Get<Anth_FeedbackModel>(model => model.FeedbackGuid == id && model.IsDeleted == false).FirstOrDefault();
            if (deletedItem != null)
            {
                deletedItem.ADt = DateTime.Now.ToString();
                deletedItem.Aned = "d";
                deletedItem.IsDeleted = true;
                DeNSo.Session.New.Set(deletedItem);
                Anth_Log.TraceEvent("Feedback Management", "Information", "tmp", "Feedback Deleted");
            }
            return deletedItem;
        }
    }
}