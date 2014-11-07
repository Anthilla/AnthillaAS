using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using AnthillaASCore.Configuration;

namespace AnthillaASCore.Security
{
    public class CoreSecurity
    {
        private static string KEY()
        {
            var sys = DeNSo.Session.New.Get<sysConfig>().FirstOrDefault();
            string k = sys.licenseKey;
            return k;
        }

        private static string VECTOR()
        {
            var sys = DeNSo.Session.New.Get<sysConfig>().FirstOrDefault();
            string v = sys.actiVationKey;
            return v;
        }

        //Core Key&Vector
        public static byte[] CoreKey()
        {
            string sysKey = KEY();
            byte[] hashCore = Hash256(sysKey);
            return hashCore;
        }

        public static byte[] CoreVector()
        {
            string sysKey = VECTOR();
            byte[] hashCore = Hash256(sysKey);
            byte[] coreVector = new byte[16];
            Array.Copy((Array)hashCore, 0, (Array)coreVector, 0, coreVector.Length);
            return coreVector;
        }

        #region Random Key&Vector

        public static byte[] CreateRandomKey()
        {
            string key = Guid.NewGuid().ToString();
            byte[] kkk = encryptBytes(key, CoreKey(), CoreVector());
            byte[] hashCore = Hash256(GetString(kkk));
            return hashCore;
        }

        private static byte[] StorRandomKey { get { var x = CreateRandomKey(); return x; } }

        public static byte[] CreateRandomVector()
        {
            string vector = Guid.NewGuid().ToString();
            byte[] hashCore = Hash256(vector);
            byte[] coreVector = new byte[16];
            Array.Copy((Array)hashCore, 0, (Array)coreVector, 0, coreVector.Length);
            return coreVector;
        }

        private static byte[] StorRandomVector { get { var x = CreateRandomVector(); return x; } }

        #endregion Random Key&Vector

        public static byte[] encryptBytes(String textValue, byte[] key, byte[] vector)
        {
            if (textValue != null || textValue != "")
            {
                RijndaelManaged crypt = new RijndaelManaged();
                ICryptoTransform encryptor = crypt.CreateEncryptor(key, vector);
                byte[] dataValueBytes = GetBytes(textValue);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                cryptoStream.Write(dataValueBytes, 0, dataValueBytes.Length);
                cryptoStream.FlushFinalBlock();
                memoryStream.Position = 0;
                byte[] transformedBytes = new byte[memoryStream.Length];
                memoryStream.Read(transformedBytes, 0, transformedBytes.Length);
                cryptoStream.Close();
                memoryStream.Close();
                return transformedBytes;
            }
            else return new byte[] { };
        }

        public static string decryptBytes(byte[] dataValue, byte[] key, byte[] vector)
        {
            if (dataValue != null)
            {
                RijndaelManaged crypt = new RijndaelManaged();
                ICryptoTransform decryptor = crypt.CreateDecryptor(key, vector);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write);
                cryptoStream.Write(dataValue, 0, dataValue.Length);
                cryptoStream.FlushFinalBlock();
                memoryStream.Position = 0;
                byte[] transformedBytes = new byte[memoryStream.Length];
                memoryStream.Read(transformedBytes, 0, transformedBytes.Length);
                cryptoStream.Close();
                memoryStream.Close();
                string arr = GetString(transformedBytes);
                return arr;
            }
            else return String.Empty;
        }

        public static byte[] Hash256(string inputString)
        {
            HashAlgorithm algorithm = SHA256.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string AnthillaHash(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in Hash256(inputString))
                sb.Append(b.ToString("X2"));
            return sb.ToString();
        }

        private static string ToHex(string value)
        {
            char[] chars = value.ToCharArray();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char c in chars)
            {
                stringBuilder.Append(((Int16)c).ToString(""));
            }
            string hexed = stringBuilder.ToString();
            return hexed;
        }

        private static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        private static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
    }
}