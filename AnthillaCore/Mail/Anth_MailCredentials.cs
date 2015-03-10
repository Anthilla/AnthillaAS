using System;

namespace AnthillaCore.Mail {

    public class Anth_MailCredentials {
        private Anth_MailConfigRepository configRepo = new Anth_MailConfigRepository();

        public Tuple<string, string> RetrieveCredentials(string userGuid, string type) {
            Tuple<string, string, string, string> setting = configRepo.GetSettingByTypeTuple(userGuid, type);
            Tuple<string, string> tuple = new Tuple<string, string>(setting.Item3, setting.Item4);
            return tuple;
        }
    }
}