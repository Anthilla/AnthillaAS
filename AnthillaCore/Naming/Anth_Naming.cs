using AnthillaCore.Logging;
using AnthillaCore.Models;
using AnthillaCore.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnthillaCore.Naming {

    public class Anth_Naming {
        public IEnumerable<Anth_NamingModel> NomenTable = DeNSo.Session.New.Get<Anth_NamingModel>(model => model.IsDeleted == false).ToArray();

        public IEnumerable<Anth_NamingModel> GetAll() {
            return NomenTable;
        }

        public Anth_NamingModel GetById(string id) {
            var item = DeNSo.Session.New.Get<Anth_NamingModel>(c => c.NamingGuid == id).FirstOrDefault();
            if (item == null) {
                var n = new Anth_NamingModel();
                n.NamingGuid = "404 Item not found";
                Anth_Log.TraceEvent("Naming Setting", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            Anth_Log.TraceEvent("Naming Setting", "Information", "tmp", "naming getbyId");
            return item;
        }

        public static Anth_NamingModel GetByType(string type) {
            var item = DeNSo.Session.New.Get<Anth_NamingModel>(c => c.NamingType == type).FirstOrDefault();
            if (item == null) {
                return null;
            }
            Anth_Log.TraceEvent("Naming Setting", "Information", "tmp", "naming getbyId");
            return item;
        }

        public static Anth_NamingModel SetForCompany(int capit, int space) {
            var model = new Anth_NamingModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.NamingGuid = "company";
            model.NamingType = "company";
            model.Capitalization = CapitalizationRule(capit);
            model.Spacing = SpacingRule(space).ToString();
            Anth_Log.TraceEvent("Naming for company", "Information", "tmp", "Naming set created");
            DeNSo.Session.New.Set(model);
            return model;
        }

        public static Anth_NamingModel SetForUser(int capit, int space) {
            var model = new Anth_NamingModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.NamingGuid = "user";
            model.NamingType = "user";
            model.Capitalization = CapitalizationRule(capit);
            model.Spacing = SpacingRule(space).ToString();
            Anth_Log.TraceEvent("Naming for user", "Information", "tmp", "Naming set created");
            DeNSo.Session.New.Set(model);
            return model;
        }

        public static Anth_NamingModel SetForProject(int capit, int space) {
            var model = new Anth_NamingModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.NamingGuid = "project";
            model.NamingType = "project";
            model.Capitalization = CapitalizationRule(capit);
            model.Spacing = SpacingRule(space).ToString();
            Anth_Log.TraceEvent("Naming for project", "Information", "tmp", "Naming set created");
            DeNSo.Session.New.Set(model);
            return model;
        }

        public static Anth_NamingModel SetForGroup(int capit, int space) {
            var model = new Anth_NamingModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.NamingGuid = "group";
            model.NamingType = "group";
            model.Capitalization = CapitalizationRule(capit);
            model.Spacing = SpacingRule(space).ToString();
            Anth_Log.TraceEvent("Naming for group", "Information", "tmp", "Naming set created");
            DeNSo.Session.New.Set(model);
            return model;
        }

        public static Anth_NamingModel SetForTag(int capit, int space) {
            var model = new Anth_NamingModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.NamingGuid = "tag";
            model.NamingType = "tag";
            model.Capitalization = CapitalizationRule(capit);
            model.Spacing = SpacingRule(space).ToString();
            Anth_Log.TraceEvent("Naming for tag", "Information", "tmp", "Naming set created");
            DeNSo.Session.New.Set(model);
            return model;
        }

        public static Anth_NamingModel SetForFile(int capit, int space, string prefix, string suffix/*, dynamic[] order, int aliasPosition*/) {
            var model = new Anth_NamingModel();
            model.ADt = DateTime.Now.ToString();
            model.Aned = "n";
            model.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            model.IsDeleted = false;
            model.StorIndexN2 = CoreSecurity.CreateRandomKey();
            model.StorIndexN1 = CoreSecurity.CreateRandomVector();
            model.NamingGuid = "file";
            model.NamingType = "file";
            model.Capitalization = CapitalizationRule(capit);
            model.Spacing = SpacingRule(space).ToString();
            model.SpaceChar = SpacingRule(space);
            model.Prefix = prefix;
            model.Suffix = suffix;

            //model.FileAliasPosition = aliasPosition;
            //model.FileAliasElements = order;

            Anth_Log.TraceEvent("Naming for file", "Information", "tmp", "Naming set created");
            DeNSo.Session.New.Set(model);
            return model;
        }

        //public dynamic[] SetDefaultLayoutOrder(int _position, DateTime _dateTime)
        //{
        //    dynamic[] array = new dynamic[]{};

        //    string date = _dateTime.ToString("yyyyMMdd");
        //    string projectName = "projectName";
        //    string version = "version";

        //    array[_position - 2] = date;
        //    array[_position - 1] = projectName;
        //    array[_position] = "alias";
        //    array[_position + 1] = version;

        //    return array;
        //}

        private static Tuple<bool, bool, bool> CapitalizationRule(int type) {
            Tuple<bool, bool, bool> tuple;
            switch (type) {
                case 0:
                    tuple = new Tuple<bool, bool, bool>(true, false, false);
                    break;

                case 1:
                    tuple = new Tuple<bool, bool, bool>(false, true, false);
                    break;

                case 2:
                    tuple = new Tuple<bool, bool, bool>(false, false, true);
                    break;

                default:
                    tuple = new Tuple<bool, bool, bool>(false, false, false);
                    break;
            }
            return tuple;
        }

        private static char SpacingRule(int type) {
            char space;
            switch (type) {
                case 0:
                    space = ' ';
                    break;

                case 1:
                    space = '.';
                    break;

                case 2:
                    space = '-';
                    break;

                case 3:
                    space = '_';
                    break;

                default:
                    space = ' ';
                    break;
            }
            return space;
        }

        public Anth_NamingModel Delete(string id) {
            var deletedItem = DeNSo.Session.New.Get<Anth_NamingModel>(model => model.NamingGuid == id && model.IsDeleted == false).FirstOrDefault();
            if (deletedItem != null) {
                deletedItem.ADt = DateTime.Now.ToString();
                deletedItem.Aned = "d";
                deletedItem.IsDeleted = true;
                DeNSo.Session.New.Set(deletedItem);
                Anth_Log.TraceEvent("Naming Setting", "Information", "tmp", "Naming set deleted");
            }
            return deletedItem;
        }

        public string CreateUserAlias(string name, string surname) {
            string subnam = name.Substring(0, 3);
            string subsur = name.Substring(0, 3);
            string alias = subnam + subsur;
            return alias;
        }
    }
}