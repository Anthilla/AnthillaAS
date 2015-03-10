//Phlegyas

namespace AnthillaPhlegyas.Model {

    //Ticket Granting Ticket
    public class AS_TGTicket : PhlegyasToken {   //valori criptati con TGKey

        public byte[] ClientGuid { get; set; }

        public AS_ClientTGSessionKey SessionKey { get; set; }
    }
}