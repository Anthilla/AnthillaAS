using AnthillaCore.Security;
using AnthillaPhlegyas.Core;
using AnthillaPhlegyas.Model;
using System;

//Phlegyas
using System.Linq;

namespace AnthillaPhlegyas {

    public class AuthenticationCheck {
        private Timestamp ts = new Timestamp();

        private AS_ClientTGSessionKey GetSession(string clientGuid) {
            var sessionModelTable = DeNSo.Session.New.Get<AS_ClientTGSessionKey>().ToArray();
            var session = (from s in sessionModelTable
                           where s.ClientSessionGuid == clientGuid
                           select s).FirstOrDefault();
            return session;
        }

        private AS_ClientIdentifier GetClient(string address) {
            var clientTable = DeNSo.Session.New.Get<AS_ClientIdentifier>().ToArray();
            var c = (from s in clientTable
                     where s.ClientAddress == AnthillaSecurity.Encrypt(address, s.StorIndexN2, s.StorIndexN1)
                     select s).FirstOrDefault();
            return c;
        }

        private bool CheckSession(string clientGuid) {
            var session = GetSession(clientGuid);
            var exp = session.Expires;
            var decExp = AnthillaSecurity.Decrypt(exp, session.StorIndexN2, session.StorIndexN1);
            var c = Convert.ToInt16(decExp);
            bool check = ts.CheckTimestampValidity(c);
            if (check == false) { return false; }
            else return true;
        }

        private string FirstCheckClient(string clientGuid) {
            var client = GetClient(clientGuid);
            var add = client.ClientAddress;
            var deAdd = AnthillaSecurity.Decrypt(add, client.StorIndexN2, client.StorIndexN1);
            if (add == null) { return ""; }
            else return deAdd;
        }

        public bool CheckSessionValidity(string clientGuid) {
            string c = FirstCheckClient(clientGuid);
            bool val = CheckSession(c);
            if (val == false) { return false; }
            else return true;
        }
    }
}