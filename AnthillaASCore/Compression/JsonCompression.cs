using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using Newtonsoft.Json;

namespace AnthillaASCore.Compression
{
    public class JsonCompression
    {
        public static string Set(dynamic item)
        {
            string json = JsonConvert.SerializeObject(item);
            var bytes = Encoding.Unicode.GetBytes(json);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var ds = new DeflateStream(mso, CompressionMode.Compress))
                {
                    msi.CopyTo(ds);
                }
                return Convert.ToBase64String(mso.ToArray());
            }
        }

        public static string Decompress(string s)
        {
            var bytes = Convert.FromBase64String(s);

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var ds = new DeflateStream(msi, CompressionMode.Decompress))
                {
                    ds.CopyTo(mso);
                }
                return Encoding.Unicode.GetString(mso.ToArray());
            }
        }
    }
}