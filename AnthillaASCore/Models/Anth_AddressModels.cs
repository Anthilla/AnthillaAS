using System.ComponentModel.DataAnnotations;

namespace AnthillaASCore.Models
{
    public class Anth_AddressModel : Anth_DelEntity
    {
        [Key]
        public string AddressId { get { return _Id; } set { _Id = value; } }

        public string AddressGuid { get; set; }

        public byte[] StreetName { get; set; }

        public byte[] StreetNumber { get; set; }

        public byte[] City { get; set; }

        public byte[] PostalCode { get; set; }

        public byte[] Country { get; set; }

        public byte[] State { get; set; }
    }
}