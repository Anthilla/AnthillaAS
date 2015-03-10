using AnthillaCore.Functions;
using AnthillaCore.Logging;
using AnthillaCore.Mapper;
using AnthillaCore.Models;
using System.Collections.Generic;
using System.Linq;

namespace AnthillaCore.Repository {

    public class Anth_FunctionRepository {
        private Anth_Mapper map = new Anth_Mapper();

        public List<Anth_Dump> GetAll() {
            var FunctionTable = Anth_FunctionTable.Functions();
            var table = new List<Anth_Dump>();
            foreach (Anth_FunctionModel item in FunctionTable) {
                var i = map.FunctionToDump(item);
                table.Add(i);
            }
            Anth_Log.TraceEvent("Function Management", "Information", "tmp", "Function getalldec");
            return table;
        }

        public List<Anth_Dump> GetAllRead() {
            var FunctionTable = Anth_FunctionTable.Functions();
            var fread = (from f in FunctionTable
                         where f.FunctionType == "r"
                         select f).ToList();
            var ssss = fread;
            var table = new List<Anth_Dump>();
            foreach (Anth_FunctionModel item in ssss) {
                var i = map.FunctionToDump(item);
                table.Add(i);
            }
            Anth_Log.TraceEvent("Function Management", "Information", "tmp", "Function Get all read");
            return table;
        }

        public List<Anth_Dump> GetAllWrite() {
            var FunctionTable = Anth_FunctionTable.Functions();
            var fread = (from f in FunctionTable
                         where f.FunctionType == "w"
                         select f).ToList();
            var ssss = fread;
            var table = new List<Anth_Dump>();
            foreach (Anth_FunctionModel item in ssss) {
                var i = map.FunctionToDump(item);
                table.Add(i);
            }
            Anth_Log.TraceEvent("Function Management", "Information", "tmp", "Function Get all write");
            return table;
        }

        public Anth_Dump GetById(string id) {
            var FunctionTable = Anth_FunctionTable.Functions();
            var item = (from f in FunctionTable
                        where f.FunctionGuid == id
                        select f).FirstOrDefault();
            var n = new Anth_Dump();
            if (item == null) {
                n.AnthillaError = "404 Item not found";
                Anth_Log.TraceEvent("Function Management", "Error", "tmp", "Item not found, id not exist");
                return n;
            }
            var i = map.FunctionToDump(item);
            Anth_Log.TraceEvent("Function Management", "Information", "tmp", "Function getbyId");
            return i;
        }

        public string GetValue(string id) {
            var f = GetById(id);
            var s = f.AnthillaValue;
            return s;
        }

        public string GetAlias(string id) {
            var f = GetById(id);
            var s = f.AnthillaAlias;
            return s;
        }
    }
}