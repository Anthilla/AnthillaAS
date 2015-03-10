using AnthillaCore.Models;
using AnthillaCore.Repository;
using AnthillaCore.Security;
using AnthillaPhlegyas.Model;

//Phlegyas
using System.Collections.Generic;
using System.Linq;

namespace AnthillaPhlegyas.Core {

    public class AuthUserRepository {
        private Anth_UserRepository userRepo = new Anth_UserRepository();

        private IEnumerable<Anth_Dump> GetAllUsers() {
            var users = userRepo.GetAll();
            return users;
        }

        public string GetUserGuid(string clientGuid) {
            var sessionAuth = DeNSo.Session.New.Get<AS_Authenticator>(a => a.ClientSessionGuid == clientGuid).FirstOrDefault();
            byte[] userGuid = sessionAuth.UserGuid;
            string ug = AnthillaSecurity.Decrypt(userGuid, sessionAuth.StorIndexN2, sessionAuth.StorIndexN1);
            return ug;
        }

        public Anth_UserModel GetUser(string clientGuid) {
            var guid = GetUserGuid(clientGuid);
            var user = DeNSo.Session.New.Get<Anth_UserModel>(u => u.UserGuid == guid).FirstOrDefault();
            return user;
        }

        public Anth_UserModel GetUserById(string guid) {
            var user = DeNSo.Session.New.Get<Anth_UserModel>(u => u.UserGuid == guid).FirstOrDefault();
            return user;
        }

        public Anth_UserModel GetUser(string username, string password) {
            Anth_UserModel user = new Anth_UserModel();
            var table = GetAllUsers();
            var item = (from u in table
                        where u.AnthillaAlias == username && u.AnthillaPassword == password
                        select u).FirstOrDefault();
            if (item != null) {
                var guid = item.AnthillaGuid;
                user = GetUserById(item.AnthillaGuid);
            }
            if (item == null) {
                user.UserGuid = "404";
            }
            return user;
        }

        public bool CheckUser(string clientGuid, string username, string password) {
            var user = GetUser(username, password);
            byte[] uu = user.UserAlias;
            byte[] up = user.UserPassword;
            byte[] duu = AnthillaSecurity.Encrypt(username, user.StorIndexN2, user.StorIndexN1);
            byte[] dup = AnthillaSecurity.Encrypt(password, user.StorIndexN2, user.StorIndexN1);
            if (uu == duu && up == dup) {
                return true;
            }
            else return false;
        }

        public Anth_UserModel GetUserByGuid(string guid) {
            var user = DeNSo.Session.New.Get<Anth_UserModel>(u => u.UserGuid == guid).FirstOrDefault();
            return user;
        }
    }
}