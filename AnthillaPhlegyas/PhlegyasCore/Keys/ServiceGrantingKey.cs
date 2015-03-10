//Phlegyas
using AnthillaPhlegyas.Core;

namespace AnthillaPhlegyas.Keys {

    public class ServiceGrantingKey {
        private KeyGen keyGen = new KeyGen();

        private byte[] SGK() {
            var g = "3AA999C7-0D30-4FAC-9957-6B8C24337912";
            var key = keyGen.Keify(g);
            return key;
        }

        public byte[] SGKey { get { return SGK(); } }
    }
}