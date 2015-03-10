using AnthillaCore.Logging;
using System.Collections.Generic;
using System.Linq;

namespace AnthillaCore.Repository {

    public class Anth_LogRequestRepository {
        public IEnumerable<Anth_RequestModel> LogTable = DeNSo.Session.New.Get<Anth_RequestModel>(model => model.IsDeleted == false).ToArray();

        public IEnumerable<Anth_RequestModel> GetAll() {
            return LogTable;
        }

        public Anth_RequestModel GetById(string id) {
            return DeNSo.Session.New.Get<Anth_RequestModel>(c => c.RequestGuid == id).FirstOrDefault();
        }

        public Anth_RequestModel GetByDate(System.DateTime date) {
            Anth_RequestModel log = (from c in GetAll()
                                     where c.IsDeleted == false && c.DateTime == date
                                     select c).FirstOrDefault();
            return log;
        }
    }
}