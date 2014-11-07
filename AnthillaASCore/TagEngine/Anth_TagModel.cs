using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AnthillaASCore.Models;

namespace AnthillaASCore.TagEngine
{
    public class Anth_TagModel : Anth_DelEntity
    {
        [Key]
        public string TagId { get { return _Id; } set { _Id = value; } }

        public string TagGuid { get; set; }

        public byte[] TagAlias { get; set; }
    }

    public class Anth_TagPresetModel : Anth_DelEntity
    {
        [Key]
        public string TagPresetId { get { return _Id; } set { _Id = value; } }

        public string TagPresetGuid { get; set; }

        public byte[] TagPresetAlias { get; set; }

        public byte[] TagPresetType { get; set; } //item || model

        public byte[] TagPresetModel { get; set; }

        public byte[] TagPresetItem { get; set; } //se è per un model allora questo valore sarà "null", il valore sarà il guid dell'oggetto

        public List<Anth_TagModel> TagPreset { get; set; }
    }
}