using AnthillaCore.Security;

//Phlegyas
using System;
using System.Security.Cryptography;

namespace AnthillaPhlegyas.Core {

    public class KeyGen {

        private byte[] Hasher(string text) {
            byte[] PlainBytes = new System.Text.UTF8Encoding().GetBytes(text);
            new RNGCryptoServiceProvider().GetNonZeroBytes(PlainBytes);
            byte[] buffer = new byte[PlainBytes.Length];
            byte[] hash = new SHA256Managed().ComputeHash(buffer);
            byte[] inArray = new byte[hash.Length];
            Array.Copy((Array)hash, 0, (Array)inArray, 0, hash.Length);
            return inArray;
        }

        public byte[] GenerateStorIndex() {
            string stor = Guid.NewGuid().ToString();
            byte[] hashCore = CoreSecurity.Hash256(stor);
            return hashCore;
        }

        public byte[] HashValue(string val) {
            byte[] hashed = CoreSecurity.Hash256(val);
            return hashed;
        }

        public byte[] GenerateKeyValue() {
            var str = Guid.NewGuid().ToString();
            byte[] dataToEncrypt = AnthillaSecurity.Encrypt(str, CoreSecurity.CreateRandomKey(), CoreSecurity.CreateRandomVector());
            return dataToEncrypt;
        }

        public byte[] GenerateCryptedKeyValue(byte[] key) {
            var str = Guid.NewGuid().ToString();
            byte[] dataToEncrypt = AnthillaSecurity.Encrypt(str, key, CoreSecurity.CreateRandomVector());
            return dataToEncrypt;
        }

        public byte[] Keify(string dataString) {
            byte[] dataToEncrypt = AnthillaSecurity.Encrypt(dataString, CoreSecurity.CreateRandomKey(), CoreSecurity.CreateRandomVector());
            return dataToEncrypt;
        }
    }
}