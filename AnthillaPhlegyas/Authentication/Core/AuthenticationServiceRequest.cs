using AnthillaCore.Security;
using AnthillaPhlegyas.Core;
using AnthillaPhlegyas.Model;

//Phlegyas
using System;
using System.Linq;

namespace AnthillaPhlegyas {

    public class AuthenticationServiceRequest {
        private Timestamp tsGen = new Timestamp();
        private AuthUserRepository ua = new AuthUserRepository();

        private AS_TGTicket GetTGTicket(string clientGuid) {
            var user = ua.GetUser(clientGuid);
            var byteUser = AnthillaSecurity.Encrypt(user.UserGuid, CoreSecurity.CoreKey(), CoreSecurity.CoreVector());
            var sessionTable = DeNSo.Session.New.Get<AS_TGTicket>().ToList();
            var session = (from s in sessionTable
                           where s.UserGuid == byteUser
                           select s).FirstOrDefault();
            return session;
        }

        private byte[] GetCTGKey(string clientGuid) {
            var user = ua.GetUser(clientGuid);
            var userKey = user.StorIndexN1;
            var ticket = GetTGTicket(clientGuid);
            AS_ClientTGSessionKey key = ticket.SessionKey;
            var KeyValue = key.Value;
            return KeyValue;
        }

        public void SetTGTicketService(string clientGuid, string serviceGuid) {
            var session = GetTGTicket(clientGuid);
            var tgtService = new AS_TGTicketService();
            tgtService._Id = session._Id;
            tgtService.ClientSessionGuid = clientGuid;
            tgtService.StorIndexN1 = session.StorIndexN1;
            tgtService.StorIndexN2 = session.StorIndexN2;
            tgtService.UserGuid = session.UserGuid;
            tgtService.Expires = session.Expires;
            tgtService.SessionKey = session.SessionKey;
            tgtService.ServiceGuid = AnthillaSecurity.Encrypt(serviceGuid, tgtService.StorIndexN2, tgtService.StorIndexN1);
            DeNSo.Session.New.Set(tgtService);
        }

        public void SetAuthenticator(string clientGuid) {   //criptato con client tg key
            var user = ua.GetUser(clientGuid);
            var key = GetCTGKey(clientGuid);
            var authn = new AS_Authenticator();
            authn._Id = Guid.NewGuid().ToString();
            authn.ClientSessionGuid = clientGuid;
            authn.StorIndexN2 = CoreSecurity.CreateRandomKey();
            authn.StorIndexN1 = CoreSecurity.CreateRandomVector();
            authn.UserGuid = AnthillaSecurity.Encrypt(user.UserGuid, key, authn.StorIndexN1);
            authn.Timestamp = AnthillaSecurity.Encrypt(tsGen.SetTimestamp().ToString(), key, authn.StorIndexN1);
            authn.Expires = AnthillaSecurity.Encrypt(tsGen.SetValidity(3).ToString(), key, authn.StorIndexN1);
            DeNSo.Session.New.Set(authn);
        }
    }
}