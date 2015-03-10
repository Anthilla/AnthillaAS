namespace AnthillaCore.Configuration {

    public class SelfConfig {
        private static string cfgAlias = "anthillaspControlSet";

        private static string[] paths = new string[] {
                cfgAlias + "Current",
                cfgAlias + "001",
                cfgAlias + "002"
            };

        private static Anth_ParamWriter config = new Anth_ParamWriter(paths, "anthillaspCore");

        public static void WriteDefaults() {
            string root;
            config.Write("root", "/anthillasp");
            root = config.ReadValue("root");

            if (config.CheckValue("antspport") == false) {
                config.Write("antspport", "8085");
            }

            if (config.CheckValue("antsrvport") == false) {
                config.Write("antsrvport", "9999");
            }

            if (config.CheckValue("antddb") == false) {
                config.Write("antddb", "/Data/Data01Vol01/AnthDB");
            }

            if (config.CheckValue("antddbraid") == false) {
                config.Write("antddbraid", "/Data/Data02Vol01/AnthDBRaid");
            }

            if (config.CheckValue("antrepo") == false) {
                config.Write("antrepo", "/Data/Data01Vol01/AnthRepo");
            }
        }

        public static string GetAnthillaSPPort() {
            return config.ReadValue("antspport");
        }

        public static string GetAnthillaSrvPort() {
            return config.ReadValue("antsrvport");
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

        public static string GetAnthillaSPUri() {
            if (config.CheckValue("antspport") == false) {
                return "http://+:8085/";
            }
            else {
                var port = config.ReadValue("antspport");
                return "http://+:" + port + "/";
            }
        }

        public static string GetAnthillaSrvUri() {
            if (config.CheckValue("antsrvport") == false) {
                return "http://+:9999/";
            }
            else {
                var port = config.ReadValue("antsrvport");
                return "http://+:" + port + "/";
            }
        }

        public static void SetAnthillaSPPort(string val) {
            config.Write("antspport", val);
        }

        public static void SetAnthillaSrvPort(string val) {
            config.Write("antsrvport", val);
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