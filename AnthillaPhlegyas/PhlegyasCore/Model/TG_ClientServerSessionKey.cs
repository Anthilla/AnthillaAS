//Phlegyas

namespace AnthillaPhlegyas.Model {

    public class TG_ClientServerSessionKey : PhlegyasToken {

        public bool IsValid { get; set; }

        public byte[] Value { get; set; }
    }
}