using System;
using System.Collections.Generic;

namespace AnthillaASCore.SysConfig
{
    public class Anth_License
    {
        private void SetAll(IEnumerable<Anth_License> licenses)
        {
            foreach (Anth_License item in licenses)
            {
                DeNSo.Session.New.Set(item);
            }
        }

        public string Generate()
        {
            string contractType = Guid.NewGuid().ToString();
            string validationKey = Guid.NewGuid().ToString();
            string accessKey = Guid.NewGuid().ToString();

            string license = contractType.Substring(0, 8) + validationKey.Substring(0, 8) + accessKey.Substring(0, 8);
            return license;
        }

        //public Anth_LicenseModel Create(string type, string val)
        //{
        //    var model = new Anth_LicenseModel();
        //    model.ADt = DateTime.Now.ToString();
        //    model.Aned = "n";
        //    model.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
        //    model.IsDeleted = false;
        //    model.StorIndexN2 = CoreSecurity.CreateRandomKey();
        //    model.StorIndexN1 = CoreSecurity.CreateRandomVector();
        //    model.LicenseId = Guid.NewGuid().ToString();
        //    model.LicenseGuid = Guid.NewGuid().ToString();
        //    model.LicenseType = AnthillaSecurity.Encrypt(type, model.StorIndexN2, model.StorIndexN1);
        //    model.LicenseValue = AnthillaSecurity.Encrypt(val, model.StorIndexN2, model.StorIndexN1);
        //    DeNSo.Session.New.Set(model);
        //    Anth_Log.TraceEvent("License Management", "Information", "tmp", "License Created");
        //    return model;
        //}
    }
}