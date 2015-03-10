using AnthillaCore.Repository;
using Newtonsoft.Json;
using System;
using System.IO;

namespace AnthillaCore.Configuration {

    public class Anth_CheckDatabase {
        private Anth_CompanyRepository repo = new Anth_CompanyRepository();

        public Tuple<bool, string> CheckDatabase() {
            Tuple<bool, string> databaseExist; //valid, message
            string root = SelfConfig.GetAnthillaDb();

            string tryPath = Path.Combine(root, "Database/denso.jnl");
            if (!File.Exists(tryPath)) {
                databaseExist = new Tuple<bool, string>(false, "There's no database in this folder");
                return databaseExist;
            }

            string tryPathConfig = Path.Combine(root, "Database/sysConfig.coll");
            if (!File.Exists(tryPathConfig)) {
                databaseExist = new Tuple<bool, string>(false, "There's no database configuration");
                return databaseExist;
            }

            string tryDbContent = JsonConvert.SerializeObject(repo.GetAll());
            if (tryDbContent == "" && tryDbContent == " " && tryDbContent == null && tryDbContent == "[]") {
                databaseExist = new Tuple<bool, string>(true, "Database is ok but it's empty");
                return databaseExist;
            }

            databaseExist = new Tuple<bool, string>(true, "Your database exists and it's ok");
            return databaseExist;
        }

        public int CheckDatabaseInt() {
            string root = SelfConfig.GetAnthillaDb();

            string tryPath = Path.Combine(root, "Database/denso.jnl");
            if (!File.Exists(tryPath)) {
                return 3;
            }

            string tryPathConfig = Path.Combine(root, "Database/sysConfig.coll");
            if (!File.Exists(tryPathConfig)) {
                return 2;
            }

            string tryDbContent = JsonConvert.SerializeObject(repo.GetAll());
            if (tryDbContent == "" && tryDbContent == " " && tryDbContent == null && tryDbContent == "[]") {
                return 1;
            }

            return 0;
        }
    }
}