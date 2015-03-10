//Phlegyas
using System.ComponentModel.DataAnnotations;

namespace AnthillaPhlegyas.Model {

    public class AS_ClientIdentifier {   //criptato con storindex

        [Key]
        public string _Id { get; set; }

        public string ClientGuid { get; set; }

        public byte[] StorIndexN1 { get; set; }

        public byte[] StorIndexN2 { get; set; }

        public byte[] ClientAddress { get; set; }

        public byte[] UserGuid { get; set; }
    }
}