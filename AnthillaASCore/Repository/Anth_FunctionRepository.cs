using System.Collections.Generic;
using System.Linq;
using AnthillaASCore.Functions;

using AnthillaASCore.Logging;
using AnthillaASCore.Mapper;
using AnthillaASCore.Models;

namespace AnthillaASCore.Repositories
{
    public class Anth_FunctionRepository
    {
        private Anth_Mapper map = new Anth_Mapper();
        private Anth_FunctionTable funcs = new Anth_FunctionTable();

        public List<Anth_Dump> GetAll()
        {
            var FunctionTable = funcs.Functions();
            var table = new List<Anth_Dump>();
            foreach (Anth_FunctionModel item in FunctionTable)
            {
                var i = map.FunctionToDump(item);
                table.Add(i);
            }
            Anth_Log.TraceEvent("Function Management", "Information", "tmp", "Function getalldec");
            return table;
        }

        public Anth_Dump GetById(string id)
        {
            var FunctionTable = funcs.Functions();
            var item = (from f in FunctionTable
                        where f.FunctionGuid == id
                        select f).FirstOrDefault();
            var n = new Anth_Dump();
            if (item == null)
            {
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("Function Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            var i = map.FunctionToDump(item);
            Anth_Log.TraceEvent("Function Management", "Information", "tmp", "Function getbyId");
            return i;
        }

        public string GetValue(string id)
        {
            var f = GetById(id);
            var s = f.AnthillaValue;
            return s;
        }
    }
}