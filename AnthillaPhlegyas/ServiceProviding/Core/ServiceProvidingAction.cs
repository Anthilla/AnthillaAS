using AnthillaCore.Security;
using AnthillaPhlegyas.Core;
using AnthillaPhlegyas.Model;
using System;

//Phlegyas
using System.Linq;

namespace AnthillaPhlegyas {

    public class ServiceProvidingAction {
        private AuthUserRepository ua = new AuthUserRepository();

        private AS_TGTicketService GetTGTicketService(string guid) {
            var tkt = DeNSo.Session.New.Get<AS_TGTicketService>(u => u.ClientSessionGuid == guid).FirstOrDefault();
            return tkt;
        }

        private byte[] GetCTGKey(string guid) {
            var user = ua.GetUserByGuid(guid);
            var userKey = user.StorIndexN1;
            var ticket = GetTGTicketService(guid);
            AS_ClientTGSessionKey key = ticket.SessionKey;
            var KeyValue = key.Value;
            return KeyValue;
        }

        private AS_Authenticator GetAuthenticator(string guid) {
            var auth = DeNSo.Session.New.Get<AS_Authenticator>(a => a.ClientSessionGuid == guid).FirstOrDefault();
            return auth;
        }

        private byte[] GetClientServerSessionKey(string guid) {
            var CSSKey = DeNSo.Session.New.Get<TG_ClientServerSessionKey>(k => k.ClientSessionGuid == guid).FirstOrDefault();
            var value = CSSKey.Value;
            return value;
        }

        private SG_ServiceAuthenticator GetServiceAuthenticator(string guid) {
            var auth = DeNSo.Session.New.Get<SG_ServiceAuthenticator>(a => a.ClientSessionGuid == guid).FirstOrDefault();
            return auth;
        }

        public bool CheckAuthenticator(string clientGuid) {
            var user = ua.GetUser(clientGuid);
            var auth = GetAuthenticator(user.UserGuid);
            var servAuth = GetServiceAuthenticator(user.UserGuid);
            var ts = auth.Timestamp;

            var tsp = servAuth.Timestamp;

            var b2 = AnthillaSecurity.Encrypt("1", auth.StorIndexN2, auth.StorIndexN1);
            var b3 = new byte[ts.Length + b2.Length];
            Buffer.BlockCopy(ts, 0, b3, 0, ts.Length);
            Buffer.BlockCopy(b2, 0, b3, ts.Length, b2.Length);

            if (b3 == tsp) { return true; }
            else return false;
        }

        public string GetService(string clientGuid) {
            var user = ua.GetUser(clientGuid);
            var key = GetClientServerSessionKey(user.UserGuid);
            var auth = GetServiceAuthenticator(user.UserGuid);
            var valueEnc = auth.ServiceGuid;
            var valueDec = AnthillaSecurity.Decrypt(valueEnc, key, auth.StorIndexN1);
            return valueDec;
        }
    }
}