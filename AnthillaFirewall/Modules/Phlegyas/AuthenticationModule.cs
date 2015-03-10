using AnthillaCore.Compression;
using AnthillaPhlegyas;
using Nancy;

namespace AnthillaFirewall.Phlegyas {

    public class AuthenticationModule : NancyModule {
        private AnthillaAuthentication auth = new AnthillaAuthentication();

        public AuthenticationModule()
            : base("/authentication") {
            //return clientGuid
            Get["/{username}/{password}"] = x => {
                string username = x.username;
                string password = x.password;
                var y = auth.AuthenticateUser(username, password);
                string json = JsonCompression.Set(y);
                return json;
            };

            //return userGuid
            Get["/query/clients/{clientGuid}"] = x => {
                string clientGuid = x.clientGuid;
                string masterGuid = "00000000-0000-0000-0000-000000000500";
                string json;
                if (clientGuid != masterGuid) {
                    string y = auth.GetUserFromSession(clientGuid);
                    json = JsonCompression.Set(y);
                }
                else {
                    json = JsonCompression.Set(masterGuid);
                }
                return json;
            };

            //return bool
            Get["/request/{client}/{service}]"] = x => {
                string client = x.client;
                string service = x.service;
                var y = auth.ServiceRequest(client, service);
                string json = JsonCompression.Set(y);
                return json;
            };
        }
    }
}