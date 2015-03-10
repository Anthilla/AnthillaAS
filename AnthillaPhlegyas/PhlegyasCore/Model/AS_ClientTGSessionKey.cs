//Phlegyas

namespace AnthillaPhlegyas.Model {

    public class AS_ClientTGSessionKey : PhlegyasToken {   //valori criptati con UserKey

        public bool IsValid { get; set; }

        public byte[] Value { get; set; }
    }
}