using System.ComponentModel.DataAnnotations;

namespace AnthillaCore.Configuration {

    public class sysConfig {

        [Key]
        public string _Id { get; set; }

        public string sysGuid { get; set; }

        public string sysAlias { get; set; }

        public string licenseKey { get; set; }

        public string actiVationKey { get; set; }

        public string licenseType { get; set; }

        public string portBackend { get; set; }

        public string portFrontend { get; set; }

        public bool securePackage { get; set; } //true set chiave durante upload, false niente chiave
    }
}