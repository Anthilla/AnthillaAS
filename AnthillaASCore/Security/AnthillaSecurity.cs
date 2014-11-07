using System;

namespace AnthillaASCore.Security
{
    public class AnthillaSecurity : CoreSecurity
    {
        public static byte[] Encrypt(string value, byte[] key, byte[] vector)
        {
            byte[] dataToEncrypt = encryptBytes(value, key, vector);
            return dataToEncrypt;
        }

        public static string Decrypt(byte[] value, byte[] key, byte[] vector)
        {
            string dataToDecrypt = decryptBytes(value, key, vector);
            return dataToDecrypt;
        }

        public static byte[] AnthillaKey(string key)
        {
            byte[] hashCore = Hash256(key);
            byte[] newArray = new byte[32];
            Array.Copy(hashCore, newArray, newArray.Length);
            return newArray;
        }

        public static byte[] AnthillaVector(string vector)
        {
            byte[] hashCore = Hash256(vector);
            byte[] newArray = new byte[16];
            Array.Copy(hashCore, newArray, newArray.Length);
            return newArray;
        }
    }
}