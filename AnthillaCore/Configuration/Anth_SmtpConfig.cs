using AnthillaCore.Models;
using AnthillaCore.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnthillaCore.Configuration {

    public class Anth_SmtpConfig {

        public smtpConfig ConfigSmtp(string url, string account, string password) {
            smtpConfig smtpModel = new smtpConfig();
            var key = CoreSecurity.CreateRandomKey();
            var vec = CoreSecurity.CreateRandomVector();
            smtpModel.StorIndexN2 = key;
            smtpModel.StorIndexN1 = vec;
            smtpModel.SmtpId = Guid.NewGuid().ToString();
            smtpModel.SmtpGuid = "E04CF32C-7718-4479-8AEC-F866E4D198DE";
            smtpModel.SmtpUrl = AnthillaSecurity.Encrypt(url, key, vec);
            smtpModel.SmtpAccount = AnthillaSecurity.Encrypt(account, key, vec);
            smtpModel.SmtpPassword = AnthillaSecurity.Encrypt(password, key, vec);
            DeNSo.Session.New.Set(smtpModel);
            return smtpModel;
        }

        public string[] GetSmtpSetting() {
            var smtpModel = DeNSo.Session.New.Get<smtpConfig>(m => m.SmtpGuid == "E04CF32C-7718-4479-8AEC-F866E4D198DE" && m.IsDeleted == false).FirstOrDefault();
            var list = new List<string>();
            var key = smtpModel.StorIndexN2;
            var vec = smtpModel.StorIndexN1;
            var s1 = AnthillaSecurity.Decrypt(smtpModel.SmtpUrl, key, vec);
            var s2 = AnthillaSecurity.Decrypt(smtpModel.SmtpAccount, key, vec);
            var s3 = AnthillaSecurity.Decrypt(smtpModel.SmtpPassword, key, vec);
            list.Add(s1);
            list.Add(s2);
            list.Add(s3);
            string[] arr = list.ToArray();
            return arr;
        }
    }
}