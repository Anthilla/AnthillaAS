using AnthillaCore.Logging;
using System.Collections.Generic;
using System.Linq;

namespace AnthillaCore.Repository {

    public class Anth_LoggingRepository {
        public IEnumerable<Anth_LogModel> LogTable = DeNSo.Session.New.Get<Anth_LogModel>(model => model.IsDeleted == false).ToArray();

        public IEnumerable<Anth_LogModel> GetAll() {
            return LogTable;
        }

        public IEnumerable<Anth_LogModel> GetAllPartial(int number) {
            var table = DeNSo.Session.New.Get<Anth_LogModel>(model => model.IsDeleted == false).ToArray();
            var logList = (from c in table
                           where c != null
                           orderby c.DateTime descending
                           select c).ToArray();
            int take = 40;
            List<Anth_LogModel> partList = logList.Skip(number).Take(take).ToList();
            return partList;
        }

        public Anth_LogModel GetById(string id) {
            return DeNSo.Session.New.Get<Anth_LogModel>(c => c.LogGuid == id).FirstOrDefault();
        }

        public Anth_LogModel GetByDate(System.DateTime date) {
            Anth_LogModel log = (from c in GetAll()
                                 where c.IsDeleted == false && c.DateTime == date
                                 select c).FirstOrDefault();
            return log;
        }
    }
}