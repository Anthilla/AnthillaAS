using AnthillaCore.Repository;
using System;

namespace AnthillaCore.Naming {

    public class Anth_NamingActions {

        private static string CapitalizeByRules(Tuple<bool, bool, bool> rule, string alias) {
            string newAlias = "";

            if (rule.Item1 == true) {
                var arr = alias.ToCharArray();
                arr[0] = Char.ToUpperInvariant(arr[0]);
                newAlias = new String(arr);
            }
            else if (rule.Item2 == true) {
                newAlias = alias.ToLowerInvariant();
            }
            else if (rule.Item3 == true) {
                newAlias = alias.ToUpperInvariant();
            }
            else {
                var arr = alias.ToCharArray();
                arr[0] = Char.ToUpperInvariant(arr[0]);
                newAlias = new String(arr);
            }

            return newAlias;
        }

        private static string SpacingByRules(string c, string alias) {
            //string ch = c.Replace(" ", "");
            string newAlias = alias.Replace(" ", c);
            return newAlias;
        }

        public static string SetCompanyAlias(string alias) {
            string newAlias;
            var naming = Anth_Naming.GetByType("company");

            var space = naming.Spacing;
            var capit = naming.Capitalization;

            var spaced = SpacingByRules(space, alias);
            var capitd = CapitalizeByRules(capit, spaced);

            newAlias = capitd;

            return newAlias;
        }

        public static string SetUserAlias(string alias) {
            string newAlias;
            var naming = Anth_Naming.GetByType("user");

            var space = naming.Spacing;
            var capit = naming.Capitalization;

            var spaced = SpacingByRules(space, alias);
            var capitd = CapitalizeByRules(capit, spaced);

            newAlias = capitd;

            return newAlias;
        }

        public static string SetProjectAlias(string alias) {
            string newAlias;
            var naming = Anth_Naming.GetByType("project");

            var space = naming.Spacing;
            var capit = naming.Capitalization;

            var spaced = SpacingByRules(space, alias);
            var capitd = CapitalizeByRules(capit, spaced);

            newAlias = capitd;

            return newAlias;
        }

        public static string SetGroupAlias(string alias) {
            string newAlias;
            var naming = Anth_Naming.GetByType("group");

            var space = naming.Spacing;
            var capit = naming.Capitalization;

            var spaced = SpacingByRules(space, alias);
            var capitd = CapitalizeByRules(capit, spaced);

            newAlias = capitd;

            return newAlias;
        }

        public static string SetTagAlias(string alias) {
            string newAlias;
            var naming = Anth_Naming.GetByType("tag");

            var space = naming.Spacing;
            var capit = naming.Capitalization;

            var spaced = SpacingByRules(space, alias);
            var capitd = CapitalizeByRules(capit, spaced);

            newAlias = capitd;

            return newAlias;
        }

        public static string SetFileAlias(string getalias) {
            string alias = getalias;
            string newAlias;
            var naming = Anth_Naming.GetByType("file");

            var space = naming.Spacing;
            var capit = naming.Capitalization;
            var prefix = naming.Prefix;
            var suff = naming.Suffix;

            //string aliasWname = DateTime.Now.ToString("yyyyMMdd") + "_" + alias;

            var preAlias = prefix + " " + alias + " " + suff;

            var spaced = SpacingByRules(space, preAlias);
            var capitd = CapitalizeByRules(capit, spaced);

            newAlias = capitd;

            return newAlias.Replace("__", "_");
        }

        private Anth_ProjectRepository projRepo = new Anth_ProjectRepository();

        public string SetFileAliasPost(DateTime _dateTime, string _projectGuid, string _fileName, string version) {
            string na = "";
            var date = DateTime.Now.ToString("yyyyMMdd") + "_";
            na += date;
            var project = projRepo.GetById(_projectGuid);
            var projectAlias = project.AnthillaAlias + "_";
            na += projectAlias;
            var filename = _fileName;
            na += filename;
            if (version != null && version != "" && version != " " && version != String.Empty) {
                na += "_" + version;
            }
            return na;
        }
    }
}