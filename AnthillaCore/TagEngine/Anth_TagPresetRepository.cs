using AnthillaCore.Logging;
using AnthillaCore.Mapper;
using AnthillaCore.Models;
using AnthillaCore.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnthillaCore.TagEngine {

    public class Anth_TagPresetRepository {
        private Anth_Mapper map = new Anth_Mapper();
        public IEnumerable<Anth_TagPresetModel> TagPresetTable = DeNSo.Session.New.Get<Anth_TagPresetModel>(model => model.IsDeleted == false).ToArray();
        private Anth_Tagger tag = new Anth_Tagger();

        public IEnumerable<Anth_Dump> GetAll() {
            var table = new List<Anth_Dump>();
            foreach (Anth_TagPresetModel item in TagPresetTable) {
                var i = map.TagPresetToDump(item);
                table.Add(i);
            }
            Anth_Log.TraceEvent("TagPreset Management", "Information", "tmp", "TagPreset getalldec");
            return table;
        }

        //public IEnumerable<Anth_Dump> GetAllPresetModel() {
        //    var table = GetAll();
        //    List<Anth_Dump> preset = (from c in table
        //                               where c.AnthillaParN1 == "1"
        //                               select c).ToList();
        //    Anth_Log.TraceEvent("TagPreset Management", "Information", "tmp", "TagPreset get for preset.type = model");
        //    return preset;
        //}

        //public IEnumerable<Anth_Dump> GetAllPresetObject() {
        //    var table = GetAll();
        //    List<Anth_Dump> preset = (from c in table
        //                               where c.AnthillaParN2 == "1"
        //                               select c).ToList();
        //    Anth_Log.TraceEvent("TagPreset Management", "Information", "tmp", "TagPreset get for preset.type = object");
        //    return preset;
        //}

        public Anth_Dump GetById(string id) {
            var item = DeNSo.Session.New.Get<Anth_TagPresetModel>(c => c.TagPresetGuid == id).FirstOrDefault();
            var n = new Anth_Dump();
            if (item == null) {
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("TagPreset Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            var i = map.TagPresetToDump(item);
            Anth_Log.TraceEvent("TagPreset Management", "Information", "tmp", "TagPreset getbyId");
            return i;
        }

        public Anth_Dump GetByAlias(string alias) {
            var table = GetAll();
            Anth_Dump company = (from c in table
                                 where c.AnthillaAlias == alias
                                 select c).FirstOrDefault();
            if (company == null) {
                var n = new Anth_Dump();
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("TagPreset Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            Anth_Log.TraceEvent("TagPreset Management", "Information", "tmp", "TagPreset getbyAlias");
            return company;
        }

        public Anth_Dump GetByObjectType(string type) {
            var table = GetAll();
            Anth_Dump company = (from c in table
                                 where c.AnthillaType == type
                                 select c).FirstOrDefault();
            Anth_Log.TraceEvent("TagPreset Management", "Information", "tmp", "TagPreset get by object type");
            return company;
        }

        public Anth_Dump GetByObjectId(string guid) {
            var table = GetAll();
            Anth_Dump company = (from c in table
                                 where c.AnthillaTagItem == guid
                                 select c).FirstOrDefault();
            Anth_Log.TraceEvent("TagPreset Management", "Information", "tmp", "TagPreset get by object id");
            return company;
        }

        public Anth_Dump GetByModel(string model) {
            var table = GetAll();
            Anth_Dump item = (from c in table
                              where c.AnthillaTagModel == model
                              select c).FirstOrDefault();
            Anth_Log.TraceEvent("TagPreset Management", "Information", "tmp", "TagPreset get by tag model");
            return item;
        }

        public string GetTagsFromPreset(string type) {
            List<string> tagTable;
            var itemTag = GetByObjectType(type);
            if (itemTag != null) { tagTable = itemTag.AnthillaTagList; }
            else { tagTable = new List<string>() { }; }
            var strResult = "";
            if (tagTable == null) {
                strResult += "";
            }
            else if (tagTable != null) {
                foreach (string s in tagTable) {
                    var splus = s + ", ";
                    strResult += splus;
                }
            }
            else strResult += "";
            return strResult;
        }

        public bool CheckAliasExist(string alias) {
            var table = GetAll();
            Anth_Dump tag = (from c in table
                             where c.AnthillaAlias == alias
                             select c).FirstOrDefault();
            if (tag == null) { return false; }
            else return true;
        }

        public bool CheckModelExist(string model) {
            var table = GetAll();
            Anth_Dump tag = (from c in table
                             where c.AnthillaTagModel == model
                             select c).FirstOrDefault();
            if (tag == null) { return false; }
            else return true;
        }

        public Anth_TagPresetModel CreateItem(string Model, string item, string alias, string tags) {
            var model = new Anth_TagPresetModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.TagPresetId = Guid.NewGuid().ToString();
            model.TagPresetGuid = Guid.NewGuid().ToString();
            model.TagPresetType = AnthillaSecurity.Encrypt("item", model.StorIndexN2, model.StorIndexN1);
            model.TagPresetModel = AnthillaSecurity.Encrypt(Model, model.StorIndexN2, model.StorIndexN1);
            model.TagPresetItem = AnthillaSecurity.Encrypt(item, model.StorIndexN2, model.StorIndexN1);
            model.TagPresetAlias = AnthillaSecurity.Encrypt(alias, model.StorIndexN2, model.StorIndexN1);
            model.TagPreset = tag.PresetTagger(tags);

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
            Anth_Log.TraceEvent("TagPreset Management", "Information", "tmp", "TagPreset Created");
            return model;
        }

        public Anth_TagPresetModel CreateModel(string Model, string alias, string tags) {
            var model = new Anth_TagPresetModel();
            if (CheckModelExist(Model) == true) {
                var table = GetAll();
                Anth_Dump dump = (from c in table
                                  where c.AnthillaTagModel == Model
                                  select c).FirstOrDefault();
                var item = DeNSo.Session.New.Get<Anth_TagPresetModel>(tp => tp.TagPresetGuid == dump.AnthillaGuid).FirstOrDefault();
                item.TagPresetType = AnthillaSecurity.Encrypt("model", item.StorIndexN2, item.StorIndexN1);
                item.TagPresetItem = AnthillaSecurity.Encrypt("model", item.StorIndexN2, item.StorIndexN1);
                item.TagPresetAlias = AnthillaSecurity.Encrypt(alias, item.StorIndexN2, item.StorIndexN1);
                item.TagPreset = tag.PresetTagger(tags);
                DeNSo.Session.New.Set(item);
            }
            else {
                model.ADt = DateTime.Now.ToString();
                model.Aned = "n";
                model.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
                model.IsDeleted = false;
                model.StorIndexN2 = CoreSecurity.CreateRandomKey();
                model.StorIndexN1 = CoreSecurity.CreateRandomVector();
                model.TagPresetId = Guid.NewGuid().ToString();
                model.TagPresetGuid = Guid.NewGuid().ToString();
                model.TagPresetType = AnthillaSecurity.Encrypt("model", model.StorIndexN2, model.StorIndexN1);
                model.TagPresetModel = AnthillaSecurity.Encrypt(Model, model.StorIndexN2, model.StorIndexN1);
                model.TagPresetItem = AnthillaSecurity.Encrypt("model", model.StorIndexN2, model.StorIndexN1);
                model.TagPresetAlias = AnthillaSecurity.Encrypt(alias, model.StorIndexN2, model.StorIndexN1);
                model.TagPreset = tag.PresetTagger(tags);

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
                Anth_Log.TraceEvent("TagPreset Management", "Information", "tmp", "TagPreset Created");
            }
            return model;
        }

        public Anth_TagPresetModel Edit(string id, string type, string Model, string item, string alias, string tags) {
            var oldItem = DeNSo.Session.New.Get<Anth_TagPresetModel>(t => t.TagPresetGuid == id && t.IsDeleted == false).FirstOrDefault();
            oldItem.IsDeleted = true;
            oldItem.Aned = "e";
            var model = new Anth_TagPresetModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = oldItem.ARelGuid;
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.TagPresetId = Guid.NewGuid().ToString();
            model.TagPresetGuid = Guid.NewGuid().ToString();
            model.TagPresetType = AnthillaSecurity.Encrypt(type, model.StorIndexN2, model.StorIndexN1);
            model.TagPresetModel = AnthillaSecurity.Encrypt(Model, model.StorIndexN2, model.StorIndexN1);
            model.TagPresetItem = AnthillaSecurity.Encrypt(item, model.StorIndexN2, model.StorIndexN1);
            model.TagPresetAlias = AnthillaSecurity.Encrypt(alias, model.StorIndexN2, model.StorIndexN1);
            model.TagPreset = tag.PresetTagger(tags);

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
            Anth_Log.TraceEvent("TagPreset Management", "Information", "tmp", "TagPreset Edited");
            return model;
        }

        public Anth_TagPresetModel Delete(string id) {
            var deletedItem = DeNSo.Session.New.Get<Anth_TagPresetModel>(model => model.TagPresetGuid == id && model.IsDeleted == false).FirstOrDefault();
            if (deletedItem != null) {
                deletedItem.ADt = DateTime.Now.ToString();
                deletedItem.Aned = "d";
                deletedItem.IsDeleted = true;
                DeNSo.Session.New.Set(deletedItem);
                Anth_Log.TraceEvent("TagPreset Management", "Information", "tmp", "TagPreset Deleted");
            }
            return deletedItem;
        }

        public void AssignTagToCollection(string tGuid, string tcGuid) {
            var tag = DeNSo.Session.New.Get<Anth_TagModel>(t => t.TagGuid == tcGuid).FirstOrDefault();
            var tagColl = DeNSo.Session.New.Get<Anth_TagPresetModel>(t => t.TagPresetGuid == tcGuid).FirstOrDefault();
            var list = tagColl.TagPreset;
            list.Add(tag);
            DeNSo.Session.New.Set(tagColl);
        }
    }
}