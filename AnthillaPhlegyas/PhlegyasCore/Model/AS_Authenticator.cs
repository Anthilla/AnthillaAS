//Phlegyas

namespace AnthillaPhlegyas.Model {

    public class AS_Authenticator : PhlegyasToken {   //stessi valori di PhlegyasToken ma criptati con ClientTGSessionKey

        public bool IsValid { get; set; }
    }
}