using AnthillaPhlegyas.Core;

//Phlegyas
using System;

namespace AnthillaPhlegyas {

    public class AnthillaAuthentication {
        private AuthUserRepository uta = new AuthUserRepository();
        private AuthenticationAction auth = new AuthenticationAction();
        private AuthenticationCheck check = new AuthenticationCheck();
        private AuthenticationServiceRequest sr = new AuthenticationServiceRequest();
        private RegisterClient client = new RegisterClient();

        public Tuple<bool, string> AuthenticateUser(string username, string password) {
            string clientGuid = Guid.NewGuid().ToString();
            var user = uta.GetUser(username, password);
            if (user.UserGuid == "404") {
                Tuple<bool, string> falseResponse = new Tuple<bool, string>(false, String.Empty);
                return falseResponse;
            }
            auth.SetTGTicket(clientGuid, username, password);
            client.Register(clientGuid, user.UserGuid);
            Tuple<bool, string> trueResponse = new Tuple<bool, string>(true, clientGuid);
            return trueResponse;
        }

        public string GetUserFromSession(string clientGuid) {
            var guid = client.GetRegisteredUser(clientGuid);
            return guid;
        }

        public bool ServiceRequest(string clientGuid, string serviceGuid) {
            if (check.CheckSessionValidity(clientGuid) == true) {
                sr.SetTGTicketService(clientGuid, serviceGuid);
                sr.SetAuthenticator(clientGuid);
                return true;
            }
            else return false;
        }
    }
}