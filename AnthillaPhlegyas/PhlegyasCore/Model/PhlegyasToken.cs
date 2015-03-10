//Phlegyas
using System.ComponentModel.DataAnnotations;

namespace AnthillaPhlegyas.Model {

    public class PhlegyasToken {

        [Key]
        public string _Id { get; set; }

        public string TokenGuid { get; set; }

        public byte[] StorIndexN1 { get; set; }

        public byte[] StorIndexN2 { get; set; }

        public byte[] UserGuid { get; set; } //userguid || usergroupguid

        public byte[] ActionGuid { get; set; } //request

        public byte[] Timestamp { get; set; } //timestamp

        public byte[] Context { get; set; } //context

        public bool Permission { get; set; }

        public byte[] Function { get; set; } //function

        public byte[] Expires { get; set; }

        public string ClientSessionGuid { get; set; } //preso dal cookie
    }
}