namespace AnthillaCore.Configuration {

    public class StorageConfig {
        private static string cfgAlias = "anthillaasControlSet";

        private static string[] paths = new string[] {
                cfgAlias + "Current",
                cfgAlias + "001",
                cfgAlias + "002"
            };

        private static Anth_ParamWriter config = new Anth_ParamWriter(paths, "anthillaasStorage");

        public static void WriteDefaults() {
            string root;
            config.Write("root", "/anthillaas/storage");
            root = config.ReadValue("root");

            if (config.CheckValue("antstorageport") == false) {
                config.Write("antstorageport", "9998");
            }

            if (config.CheckValue("antasport") == false) {
                config.Write("anthasport", "8084");
            }

            if (config.CheckValue("antddb") == false) {
                config.Write("antddb", "/Data/Data01Vol01/AnthASDB");
            }

            if (config.CheckValue("antddbraid") == false) {
                config.Write("antddbraid", "/Data/Data02Vol01/AnthASDBRaid");
            }

            if (config.CheckValue("antrepo") == false) {
                config.Write("antrepo", "/Data/Data01Vol01/AnthASRepo");
            }
        }

        public static string GetAnthillaASPort() {
            return config.ReadValue("antasport");
        }

        public static string GetAnthillaStoragePort() {
            return config.ReadValue("antstorageport");
        }

        public static string GetAnthillaDb() {
            return config.ReadValue("antddb");
        }

        public static string GetAnthillaDbRaid() {
            return config.ReadValue("antddbraid");
        }

        public static string GetAnthillaRepo() {
            return config.ReadValue("antrepo");
        }

        public static string GetAnthillaASUri() {
            if (config.CheckValue("antasport") == false) {
                return "http://+:8084/";
            }
            else {
                var port = config.ReadValue("antasport");
                return "http://+:" + port + "/";
            }
        }

        public static string GetAnthillaStorageUri() {
            if (config.CheckValue("antstorageport") == false) {
                return "http://+:9998/";
            }
            else {
                var port = config.ReadValue("antstorageport");
                return "http://+:" + port + "/";
            }
        }

        public static void SetAnthillaASPort(string val) {
            config.Write("antasport", val);
        }

        public static void SetAnthillaStoragePort(string val) {
            config.Write("antstorageport", val);
        }

        public static void SetAnthillaDb(string val) {
            config.Write("antddb", val);
        }

        public static void SetAnthillaDbRaid(string val) {
            config.Write("antddbraid", val);
        }

        public static void SetAnthillaRepo(string val) {
            config.Write("antrepo", val);
        }
    }
}