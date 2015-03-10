using AnthillaCore.Security;
using AnthillaPhlegyas.Core;
using AnthillaPhlegyas.Keys;
using AnthillaPhlegyas.Model;

//Phlegyas
using System;

namespace AnthillaPhlegyas {

    public class AuthenticationAction {
        private KeyGen keyGen = new KeyGen();
        private Timestamp tsGen = new Timestamp();
        private AuthUserRepository userRepo = new AuthUserRepository();

        private AS_ClientTGSessionKey SetClientTGSessionKey(string username, string password) {   //valori criptati con user.StorIndexN2
            var user = userRepo.GetUser(username, password);
            var key = new AS_ClientTGSessionKey();
            key._Id = Guid.NewGuid().ToString();
            key.StorIndexN2 = CoreSecurity.CreateRandomKey();
            key.StorIndexN1 = CoreSecurity.CreateRandomVector();
            key.UserGuid = AnthillaSecurity.Encrypt(user.UserGuid, user.StorIndexN2, user.StorIndexN1);
            key.Value = keyGen.GenerateCryptedKeyValue(key.StorIndexN2);
            key.Timestamp = AnthillaSecurity.Encrypt(tsGen.SetTimestamp().ToString(), key.StorIndexN2, key.StorIndexN1);
            key.Expires = AnthillaSecurity.Encrypt(tsGen.SetValidity(12).ToString(), key.StorIndexN2, key.StorIndexN1);
            DeNSo.Session.New.Set(key);
            return key;
        }

        public void SetTGTicket(string clientGuid, string username, string password) {   //valori criptati con TGKey
            var user = userRepo.GetUser(username, password);
            var ticket = new AS_TGTicket();
            ticket._Id = Guid.NewGuid().ToString();
            ticket.StorIndexN2 = CoreSecurity.CreateRandomKey();
            ticket.StorIndexN1 = CoreSecurity.CreateRandomVector();
            ticket.UserGuid = AnthillaSecurity.Encrypt(user.UserGuid, TicketGrantingKey.TGK(), ticket.StorIndexN1);
            ticket.Timestamp = AnthillaSecurity.Encrypt(tsGen.SetTimestamp().ToString(), ticket.StorIndexN2, ticket.StorIndexN1);
            ticket.Expires = AnthillaSecurity.Encrypt(tsGen.SetValidity(72).ToString(), ticket.StorIndexN2, ticket.StorIndexN1);
            ticket.SessionKey = SetClientTGSessionKey(username, password);
            DeNSo.Session.New.Set(ticket);
        }
    }
}