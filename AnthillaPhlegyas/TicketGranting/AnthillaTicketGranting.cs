//Phlegyas
using AnthillaPhlegyas.Core;

namespace AnthillaPhlegyas {

    public class AnthillaTicketGranting {
        private TicketGrantingAction act = new TicketGrantingAction();
        private AuthUserRepository use = new AuthUserRepository();

        public bool SetClientConnection(string clientGuid) {
            var user = use.GetUser(clientGuid);
            if (user.UserGuid != null) {
                act.SetTicketClientServer(clientGuid);
                act.SetClientServerSessionKey(clientGuid);
                return true;
            }
            else return false;
        }
    }
}