using AnthillaCore.Security;

//Phlegyas

namespace AnthillaPhlegyas.Keys {

    public static class TicketGrantingKey {

        public static byte[] TGK() {
            var g = "EAEE4493-7130-47B5-8ADB-E4E1DB1E60E9";
            var key = AnthillaSecurity.AnthillaKey(g);
            return key;
        }
    }
}