using AnthillaPhlegyas.Model;
using System.Linq;

namespace AnthillaPhlegyas.Expiration {

    public static class Delete {

        public static void As_Authenticator(string clientGuid) {
            var item = DeNSo.Session.New.Get<AS_Authenticator>(a => a.ClientSessionGuid == clientGuid).FirstOrDefault();
            DeNSo.Session.New.Delete(item);
        }

        public static void AS_ClientTGSessionKey(string clientGuid) {
            var item = DeNSo.Session.New.Get<AS_ClientTGSessionKey>(a => a.ClientSessionGuid == clientGuid).FirstOrDefault();
            DeNSo.Session.New.Delete(item);
        }

        public static void AS_TGTicket(string clientGuid) {
            var item = DeNSo.Session.New.Get<AS_TGTicket>(a => a.ClientSessionGuid == clientGuid).FirstOrDefault();
            DeNSo.Session.New.Delete(item);
        }

        public static void AS_TGTicketService(string clientGuid) {
            var item = DeNSo.Session.New.Get<AS_TGTicketService>(a => a.ClientSessionGuid == clientGuid).FirstOrDefault();
            DeNSo.Session.New.Delete(item);
        }

        public static void TG_ClientServerTicket(string clientGuid) {
            var item = DeNSo.Session.New.Get<TG_ClientServerTicket>(a => a.ClientSessionGuid == clientGuid).FirstOrDefault();
            DeNSo.Session.New.Delete(item);
        }

        public static void TG_ClientServerSessionKey(string clientGuid) {
            var item = DeNSo.Session.New.Get<TG_ClientServerSessionKey>(a => a.ClientSessionGuid == clientGuid).FirstOrDefault();
            DeNSo.Session.New.Delete(item);
        }

        public static void SG_ServiceAuthenticator(string clientGuid) {
            var item = DeNSo.Session.New.Get<SG_ServiceAuthenticator>(a => a.ClientSessionGuid == clientGuid).FirstOrDefault();
            DeNSo.Session.New.Delete(item);
        }
    }
}