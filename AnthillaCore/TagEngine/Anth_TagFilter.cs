using AnthillaCore.Repository;
using System.Collections.Generic;
using System.Linq;

namespace AnthillaCore.TagEngine {

    public class Anth_TagFilter {
        private Anth_CompanyRepository companyRepo = new Anth_CompanyRepository();
        private Anth_ProjectRepository projectRepo = new Anth_ProjectRepository();
        private Anth_UserRepository userRepo = new Anth_UserRepository();
        private Anth_TagPresetRepository presetRepo = new Anth_TagPresetRepository();

        public List<string> GetByCompany() {
            List<string> tagList = new List<string> { };
            var companyTable = companyRepo.GetAll();
            foreach (var company in companyTable) {
                List<string> compTagList = company.AnthillaTags;
                tagList.Union(compTagList, new EqComparer());
            }
            return tagList;
        }

        public List<string> GetByCompany(string guid) {
            List<string> tagList = new List<string> { };
            var company = companyRepo.GetById(guid);
            tagList = company.AnthillaTags;
            return tagList;
        }

        public List<string> GetByProject() {
            List<string> tagList = new List<string> { };
            var projectTable = projectRepo.GetAll();
            foreach (var project in projectTable) {
                List<string> compTagList = project.AnthillaTags;
                tagList.Union(compTagList, new EqComparer());
            }
            return tagList;
        }

        public List<string> GetByProject(string guid) {
            List<string> tagList = new List<string> { };
            var project = projectRepo.GetById(guid);
            tagList = project.AnthillaTags;
            return tagList;
        }

        public List<string> GetByUser() {
            List<string> tagList = new List<string> { };
            var userTable = userRepo.GetAll();
            foreach (var user in userTable) {
                List<string> compTagList = user.AnthillaTags;
                tagList.Union(compTagList, new EqComparer());
            }
            return tagList;
        }

        public List<string> GetByUser(string guid) {
            List<string> tagList = new List<string> { };
            var user = userRepo.GetById(guid);
            tagList = user.AnthillaTags;
            return tagList;
        }

        public List<string> GetPresetForCompany() {
            List<string> tagList = new List<string> { };
            var preset = presetRepo.GetByModel("company");
            if (preset != null) {
                tagList = preset.AnthillaTagList;
            }
            return tagList;
        }

        public List<string> GetPresetForProject() {
            List<string> tagList = new List<string> { };
            var preset = presetRepo.GetByModel("project");
            if (preset != null) {
                tagList = preset.AnthillaTagList;
            }
            return tagList;
        }

        public List<string> GetPresetForUser() {
            List<string> tagList = new List<string> { };
            var preset = presetRepo.GetByModel("user");
            if (preset != null) {
                tagList = preset.AnthillaTagList;
            }
            return tagList;
        }

        #region IEqualityComparer<int> Members

        public class EqComparer : IEqualityComparer<string> {

            public bool Equals(string x, string y) {
                return x == y;
            }

            public int GetHashCode(string obj) {
                return obj.GetHashCode();
            }
        }

        #endregion IEqualityComparer<int> Members
    }
}