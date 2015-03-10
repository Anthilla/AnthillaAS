using Newtonsoft.Json;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace AnthillaCore.Compression {

    public static class StringExtension {

        public static string ToHex(this String stringValue) {
            char[] chars = stringValue.ToCharArray();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char c in chars) {
                stringBuilder.Append(((Int16)c).ToString(""));
            }
            string hexed = stringBuilder.ToString();
            return hexed;
        }

        public static string FromHex(this String hexValue) {
            string StrValue = "";
            while (hexValue.Length > 0) {
                StrValue += System.Convert.ToChar(System.Convert.ToUInt32(hexValue.Substring(0, 2), 16)).ToString();
                hexValue = hexValue.Substring(2, hexValue.Length - 2);
            }
            return StrValue;
        }
    }

    public class JsonCompression {

        public static string Set(dynamic item) {
            string json = JsonConvert.SerializeObject(item);
            var bytes = Encoding.Unicode.GetBytes(json);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream()) {
                using (var ds = new DeflateStream(mso, CompressionMode.Compress)) {
                    msi.CopyTo(ds);
                }
                return Convert.ToBase64String(mso.ToArray());
            }
        }

        public static string Decompress(string s) {
            var bytes = Convert.FromBase64String(s);

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream()) {
                using (var ds = new DeflateStream(msi, CompressionMode.Decompress)) {
                    ds.CopyTo(mso);
                }
                return Encoding.Unicode.GetString(mso.ToArray());
            }
        }

        public static string Comprhex(dynamic item) {
            string json = JsonConvert.SerializeObject(item);
            var bytes = Encoding.Unicode.GetBytes(json);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream()) {
                using (var ds = new DeflateStream(mso, CompressionMode.Compress)) {
                    msi.CopyTo(ds);
                }
                string r = Convert.ToBase64String(mso.ToArray());
                var h = r.ToHex();
                return h;
            }
        }

        public static string Decomprhex(string s) {
            var h = s.FromHex();
            var bytes = Convert.FromBase64String(h);

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream()) {
                using (var ds = new DeflateStream(msi, CompressionMode.Decompress)) {
                    ds.CopyTo(mso);
                }
                string r = Encoding.Unicode.GetString(mso.ToArray());
                return r;
            }
        }
    }
}