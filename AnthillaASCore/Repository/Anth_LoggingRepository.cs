using System.Collections.Generic;
using System.Linq;

using AnthillaASCore.Logging;

namespace AnthillaASCore.Repositories
{
    public class Anth_LoggingRepository
    {
        public IEnumerable<Anth_LogModel> LogTable = DeNSo.Session.New.Get<Anth_LogModel>(model => model.IsDeleted == false).ToArray();

        public IEnumerable<Anth_LogModel> GetAll()
        {
            return LogTable;
        }

        public Anth_LogModel GetById(string id)
        {
            return DeNSo.Session.New.Get<Anth_LogModel>(c => c.LogGuid == id).FirstOrDefault();
        }

        public Anth_LogModel GetByDate(System.DateTime date)
        {
            Anth_LogModel log = (from c in GetAll()
                                 where c.IsDeleted == false && c.DateTime == date
                                 select c).FirstOrDefault();
            return log;
        }
    }
}