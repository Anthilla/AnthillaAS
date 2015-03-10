using AnthillaCore.Security;
using AnthillaPhlegyas.Core;
using AnthillaPhlegyas.Keys;
using AnthillaPhlegyas.Model;
using System;

//Phlegyas
using System.Linq;

namespace AnthillaPhlegyas {

    public class ServiceGrantingAction {
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

        private TG_ClientServerTicket GetTicketClientServer(string guid) {
            var ticket = DeNSo.Session.New.Get<TG_ClientServerTicket>(t => t.ClientSessionGuid == guid).FirstOrDefault();
            return ticket;
        }

        private AS_Authenticator GetAuthenticator(string guid) {
            //            var user = ua.GetUserByGuid(guid);
            var auth = DeNSo.Session.New.Get<AS_Authenticator>(a => a.ClientSessionGuid == guid).FirstOrDefault();
            return auth;
        }

        private byte[] GetClientServerSessionKey(string guid) {
            var CSSKey = DeNSo.Session.New.Get<TG_ClientServerSessionKey>(k => k.ClientSessionGuid == guid).FirstOrDefault();
            var value = CSSKey.Value;
            return value;
        }

        private string GetServiceGuidFromTicket(string guid) {
            var key = SGKey.SGKey;
            var ticket = GetTicketClientServer(guid);
            var valueEnc = ticket.ServiceGuid;
            var valueDec = AnthillaSecurity.Decrypt(valueEnc, key, ticket.StorIndexN1);
            return valueDec;
        }

        public void SetServiceAuthenticator(string clientGuid) {   //client server session key
            var user = ua.GetUser(clientGuid);
            var key = GetClientServerSessionKey(user.UserGuid);
            var auth = GetAuthenticator(user.UserGuid);
            var service = GetServiceGuidFromTicket(user.UserGuid);
            var newAuth = new SG_ServiceAuthenticator();
            newAuth._Id = auth._Id;
            newAuth.StorIndexN1 = auth.StorIndexN1;
            newAuth.StorIndexN2 = auth.StorIndexN2;
            newAuth.UserGuid = AnthillaSecurity.Encrypt(user.UserGuid, key, newAuth.StorIndexN1);
            newAuth.ServiceGuid = AnthillaSecurity.Encrypt(service, key, newAuth.StorIndexN1);

            var b1 = auth.Timestamp;
            var b2 = AnthillaSecurity.Encrypt("1", key, newAuth.StorIndexN1);
            var b3 = new byte[b1.Length + b2.Length];
            Buffer.BlockCopy(b1, 0, b3, 0, b1.Length);
            Buffer.BlockCopy(b2, 0, b3, b1.Length, b2.Length);
            newAuth.Timestamp = b3;

            newAuth.Expires = auth.Expires;
            DeNSo.Session.New.Set(newAuth);
        }
    }
}