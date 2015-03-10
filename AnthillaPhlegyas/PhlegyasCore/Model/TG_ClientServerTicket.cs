//Phlegyas

namespace AnthillaPhlegyas.Model {

    public class TG_ClientServerTicket : PhlegyasToken { //valori criptati con ServiceGrantingKey (anche quelli del token di base)

        public bool IsValid { get; set; }

        public byte[] ServiceGuid { get; set; }
    }
}