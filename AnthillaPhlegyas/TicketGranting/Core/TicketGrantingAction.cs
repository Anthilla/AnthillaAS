using AnthillaCore.Security;
using AnthillaPhlegyas.Core;
using AnthillaPhlegyas.Keys;
using AnthillaPhlegyas.Model;

//Phlegyas
using System;
using System.Linq;

namespace AnthillaPhlegyas {

    public class TicketGrantingAction {
        private Timestamp tsGen = new Timestamp();
        private KeyGen keyGen = new KeyGen();
        private ServiceGrantingKey SGKey = new ServiceGrantingKey();
        private AuthUserRepository ua = new AuthUserRepository();

        private AS_TGTicketService GetTGTicketService(string guid) {
            var tkt = DeNSo.Session.New.Get<AS_TGTicketService>().FirstOrDefault(u => u.ClientSessionGuid == guid);
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

        private string GetAuthenticatorValue(string guid) {
            AS_Authenticator auth = GetAuthenticator(guid);
            var userGuid = guid;
            var timestamp = auth.Timestamp.ToString();
            if (userGuid != guid) { return "Error"; }
            var value = userGuid.ToString() + "--" + timestamp;
            return value;
        }

        private string GetServiceGuid(string clientGuid) {
            var user = ua.GetUser(clientGuid);
            var userKey = user.StorIndexN1;
            var ticket = GetTGTicketService(user.UserGuid);
            var serviceGuid = ticket.ServiceGuid;
            var dServiceGuid = AnthillaSecurity.Decrypt(serviceGuid, ticket.StorIndexN2, ticket.StorIndexN1);
            return dServiceGuid;
        }

        public void SetTicketClientServer(string clientGuid) {   //criptato con SGKey
            var key = SGKey.SGKey;
            var user = ua.GetUser(clientGuid);
            var serviceGuid = GetServiceGuid(clientGuid);
            var csTicket = new TG_ClientServerTicket();
            csTicket._Id = Guid.NewGuid().ToString();
            csTicket.StorIndexN2 = CoreSecurity.CreateRandomKey();
            csTicket.StorIndexN1 = CoreSecurity.CreateRandomVector();
            csTicket.UserGuid = AnthillaSecurity.Encrypt(user.UserGuid, key, csTicket.StorIndexN1);
            csTicket.ServiceGuid = AnthillaSecurity.Encrypt(serviceGuid, key, csTicket.StorIndexN1);
            csTicket.Expires = AnthillaSecurity.Encrypt(tsGen.SetValidity(6).ToString(), key, csTicket.StorIndexN1);
            DeNSo.Session.New.Set(csTicket);
        }

        public void SetClientServerSessionKey(string clientGuid) {   //criptato con client-tg key
            var user = ua.GetUser(clientGuid);
            var key = GetCTGKey(user.UserGuid);
            var sessionKey = new TG_ClientServerSessionKey();
            sessionKey._Id = Guid.NewGuid().ToString();
            sessionKey.StorIndexN2 = CoreSecurity.CreateRandomKey();
            sessionKey.StorIndexN1 = CoreSecurity.CreateRandomVector();
            sessionKey.UserGuid = AnthillaSecurity.Encrypt(user.UserGuid, key, sessionKey.StorIndexN1);
            sessionKey.Value = keyGen.GenerateKeyValue();
            sessionKey.Expires = AnthillaSecurity.Encrypt(tsGen.SetValidity(6).ToString(), key, sessionKey.StorIndexN1);
            DeNSo.Session.New.Set(sessionKey);
        }
    }
}