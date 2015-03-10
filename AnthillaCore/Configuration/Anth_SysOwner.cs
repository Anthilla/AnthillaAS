using AnthillaCore.Logging;
using AnthillaCore.Models;
using AnthillaCore.Repository;
using System.Linq;

namespace AnthillaCore.Configuration {

    public class Anth_SysOwner {
        private Anth_UserRepository userRepo = new Anth_UserRepository();
        private Anth_CompanyRepository compRepo = new Anth_CompanyRepository();

        public Anth_CompanyModel MakeOwner(string compGuid) {
            var comp = DeNSo.Session.New.Get<Anth_CompanyModel>(c => c.IsDeleted == false && c.CompanyGuid == compGuid).FirstOrDefault();
            comp.CompanyOwner = 1;
            DeNSo.Session.New.Set(comp);

            var userlist = userRepo.GetByCompany(compGuid);
            foreach (var u in userlist) {
                var g = u.AnthillaGuid;
                userRepo.MakeInsider(g);
            }

            return comp;
        }

        public Anth_Dump GetOwner() {
            var table = compRepo.GetAll();
            Anth_Dump company = (from c in table
                                 where c.AnthillaOwner == 1
                                 select c).FirstOrDefault();
            if (company == null) {
                var n = new Anth_Dump();
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("Company Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            Anth_Log.TraceEvent("Company Management", "Information", "tmp", "Company get owner");
            return company;
        }
    }
}