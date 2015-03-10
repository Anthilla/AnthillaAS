using AnthillaCore.Compression;
using AnthillaCore.Configuration;
using AnthillaCore.Models;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AnthillaCore {

    public class DatabaseHelper {
        private static string server = SelfConfig.GetAnthillaSrvUri();
        private static string storage = StorageConfig.GetAnthillaStorageUri();
        private static string firewall = FirewallConfig.GetAnthillaFirewallUri();

        private static async Task<List<Anth_Dump>> USERS() {
            HttpClient client = new HttpClient();
            List<Anth_Dump> users = new List<Anth_Dump>() { };

            var serveruri = server + "/user";
            Task<string> servercontentTask = client.GetStringAsync(serveruri);
            var serverjson = JsonCompression.Decompress(await servercontentTask);
            List<Anth_Dump> serverusers = JsonConvert.DeserializeObject<List<Anth_Dump>>(serverjson);

            var storageuri = storage + "/user";
            Task<string> storagecontentTask = client.GetStringAsync(storageuri);
            var storagejson = JsonCompression.Decompress(await storagecontentTask);
            List<Anth_Dump> storageusers = JsonConvert.DeserializeObject<List<Anth_Dump>>(storagejson);

            var firewalluri = firewall + "/user";
            Task<string> firewallcontentTask = client.GetStringAsync(firewalluri);
            var firewalljson = JsonCompression.Decompress(await firewallcontentTask);
            List<Anth_Dump> firewallusers = JsonConvert.DeserializeObject<List<Anth_Dump>>(firewalljson);

            users = serverusers.Concat(storageusers).Concat(firewallusers).ToList();

            return users;
        }

        public static List<Anth_Dump> UserList() {
            return USERS().Result;
        }
    }

    public class UserDatabase : IUserMapper {

        public static List<Tuple<string, string, Guid>> USERS() {
            List<Tuple<string, string, Guid>> userList = new List<Tuple<string, string, Guid>>() { };
            var UserDb = DatabaseHelper.UserList();
            foreach (Anth_Dump user in UserDb) {
                Tuple<string, string, Guid> userid = MapUserFromDb.Map(user);
                userList.Add(userid);
            }
            userList.Add(new Tuple<string, string, Guid>("admin", "admin", new Guid("00000000-0000-0000-0000-000000000500")));
            return userList;
        }

        public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context) {
            var Users = USERS();
            var UserRecord = Users.Where(u => u.Item3 == identifier).FirstOrDefault();
            return UserRecord == null
                       ? null
                       : new UserIdentity { UserName = UserRecord.Item1 };
        }

        public static Guid? ValidateUser(string username, string password) {
            var Users = USERS();
            var UserRecord = Users.Where(u => u.Item1 == username && u.Item2 == password).FirstOrDefault();
            if (UserRecord == null) {
                return null;
            }
            return UserRecord.Item3;
        }
    }
}