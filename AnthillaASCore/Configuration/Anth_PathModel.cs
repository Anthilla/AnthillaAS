using System.ComponentModel.DataAnnotations;
using AnthillaASCore.Models;

namespace AnthillaASCore.Configuration
{
    public class root : Anth_DelEntity
    {
        [Key]
        public string RootId { get { return _Id; } set { _Id = value; } }

        public string RootGuid { get; set; }

        public string RootAlias { get; set; }

        public string RootPath { get; set; }
    }
}