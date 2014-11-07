using System;
using System.IO;
using System.Linq;
using AnthillaASCore.Compression;
using AnthillaASCore.Security;

namespace AnthillaASCore.Configuration
{
    public class licenseKey
    {
        public string id { get; set; }

        public byte[] value { get; set; }
    }

    public class Anth_SysConfig
    {
        public byte[] SetLicenseKey()
        {
            var key = Guid.NewGuid().ToString();
            byte[] hashCore = CoreSecurity.Hash256(key);
            return hashCore;
        }

        public byte[] SetActiVationKey()
        {
            var key = Guid.NewGuid().ToString();
            byte[] hashCore = CoreSecurity.Hash256(key);
            byte[] coreVector = new byte[16];
            Array.Copy((Array)hashCore, 0, (Array)coreVector, 0, coreVector.Length);
            return coreVector;
        }

        public bool CheckExist()
        {
            var sConfig = DeNSo.Session.New.Get<sysConfig>(i => i._Id == "36D99095-54D6-480A-BDED-4D0A2BBC3A08").FirstOrDefault();
            if (sConfig == null)
            {
                return false;
            }
            else if (sConfig != null)
            {
                return true;
            }
            else return false;
        }

        public sysConfig ConfigSystem(string key, string vec, string alias, string type)
        {
            sysConfig sConfig = new sysConfig();
            sConfig._Id = "36D99095-54D6-480A-BDED-4D0A2BBC3A08";
            sConfig.licenseKey = key;
            sConfig.actiVationKey = vec;

            sConfig.sysGuid = Guid.NewGuid().ToString();
            sConfig.sysAlias = alias;
            sConfig.licenseType = type;

            DeNSo.Session.New.Set(sConfig);
            return sConfig;
        }

        public void SetBackDoor(string door)
        {
            var c = JsonCompression.Set(door);
            string path = @"D:\Uber\AnthillaDatabase\config\anthilla_be.txt";
            StreamWriter streamWriter = new StreamWriter(path);
            streamWriter.WriteLine(c);
            streamWriter.Close();
        }

        public string GetBackDoor()
        {
            string path = @"D:\Uber\AnthillaDatabase\config\anthilla_be.txt";
            StreamReader streamReader = new StreamReader(path);
            string line = streamReader.ReadLine();
            streamReader.Close();
            var d = JsonCompression.Decompress(line);
            return d;
        }

        public void SetFrontDoor(string door)
        {
            var c = JsonCompression.Set(door);
            string path = @"D:\Uber\AnthillaDatabase\config\anthilla_fe.txt";
            StreamWriter streamWriter = new StreamWriter(path);
            streamWriter.WriteLine(c);
            streamWriter.Close();
        }

        public string GetFrontDoor()
        {
            string path = @"D:\Uber\AnthillaDatabase\config\anthilla_fe.txt";
            StreamReader streamReader = new StreamReader(path);
            string line = streamReader.ReadLine();
            streamReader.Close();
            var d = JsonCompression.Decompress(line);
            return d;
        }

        public sysConfig GetConfig()
        {
            var sc = DeNSo.Session.New.Get<sysConfig>(i => i._Id == "36D99095-54D6-480A-BDED-4D0A2BBC3A08").FirstOrDefault();
            return sc;
        }
    }
}