using System.ComponentModel.DataAnnotations;

namespace AnthillaASCore.Configuration
{
    public class sysConfig
    {
        [Key]
        public string _Id { get; set; }

        public string sysGuid { get; set; }

        public string sysAlias { get; set; }

        public string licenseKey { get; set; }

        public string actiVationKey { get; set; }

        public string licenseType { get; set; }

        public string doorBackend { get; set; }

        public string doorFrontend { get; set; }
    }
}