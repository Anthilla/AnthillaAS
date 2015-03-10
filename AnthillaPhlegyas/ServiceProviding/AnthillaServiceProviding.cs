//Phlegyas
using AnthillaPhlegyas.Core;

namespace AnthillaPhlegyas {

    public class AnthillaServiceProviding {
        private ServiceGrantingAction granter = new ServiceGrantingAction();
        private ServiceProvidingAction provider = new ServiceProvidingAction();
        private AuthUserRepository use = new AuthUserRepository();

        private void SetClientConnection(string clientGuid) {
            var user = use.GetUser(clientGuid);
            if (user.UserGuid != null) {
                granter.SetServiceAuthenticator(clientGuid);
            }
        }

        public string GetService(string clientGuid) {
            SetClientConnection(clientGuid);
            var user = use.GetUser(clientGuid);
            var check = provider.CheckAuthenticator(clientGuid);
            if (user.UserGuid != null && check == true) {
                var service = provider.GetService(clientGuid);
                return service;
            }
            else return "error";
        }
    }
}