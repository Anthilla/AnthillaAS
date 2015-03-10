using AnthillaCore.Security;
using AnthillaPhlegyas.Model;
using System;
using System.Linq;

namespace AnthillaPhlegyas {

    public class RegisterClient {

        public void Register(string clientAddress, string userGuid) {
            Console.WriteLine(clientAddress);

            var client = new AS_ClientIdentifier();
            client._Id = Guid.NewGuid().ToString();
            client.ClientGuid = Guid.NewGuid().ToString();
            client.StorIndexN2 = CoreSecurity.CoreKey();
            client.StorIndexN1 = CoreSecurity.CoreVector();
            client.ClientAddress = AnthillaSecurity.Encrypt(clientAddress, client.StorIndexN2, client.StorIndexN1);
            client.UserGuid = AnthillaSecurity.Encrypt(userGuid, client.StorIndexN2, client.StorIndexN1);
            DeNSo.Session.New.Set(client);
        }

        public string GetRegisteredUser(string clientAddress)//user.guid
        {
            var clientTable = DeNSo.Session.New.Get<AS_ClientIdentifier>().ToList();
            var client = (from c in clientTable
                          where AnthillaSecurity.Decrypt(c.ClientAddress, c.StorIndexN2, c.StorIndexN1) == clientAddress
                          select c).FirstOrDefault();

            var cryptedUserGuid = client.UserGuid;
            var userGuid = AnthillaSecurity.Decrypt(cryptedUserGuid, client.StorIndexN2, client.StorIndexN1);
            return userGuid;
        }
    }
}