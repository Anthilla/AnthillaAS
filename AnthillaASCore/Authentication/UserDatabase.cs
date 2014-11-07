using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AnthillaASCore.Models;
using AnthillaASCore.Repositories;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Security;
using Newtonsoft.Json;

namespace AnthillaASCore
{
    public static class UserDatabaseHelper
    {
        private static async Task<List<Anth_Dump>> GetUser()
        {
            HttpClient client = new HttpClient();
            string localhost = "http://localhost:9999";
            var uri = localhost + "/user";
            Task<string> contentTask = client.GetStringAsync(uri);
            List<Anth_Dump> eventItem = JsonConvert.DeserializeObject<List<Anth_Dump>>(await contentTask);
            return eventItem;
        }

        public static List<Anth_Dump> UserList()
        {
            return GetUser().Result;
        }
    }

    public class UserDatabase : IUserMapper
    {
        private static List<Tuple<string, string, Guid>> Users = new List<Tuple<string, string, Guid>>();
        private static Anth_UserRepository repo = new Anth_UserRepository();

        static UserDatabase()
        {
            var UserDb = repo.GetAll();
            foreach (Anth_Dump user in UserDb)
            {
                Tuple<string, string, Guid> userid = MapUserFromDb.Map(user);
                Users.Add(userid);
            }
            Users.Add(new Tuple<string, string, Guid>("Admin", "@Admin12e3", new Guid("00000000-0000-0000-0000-000000000100")));
        }

        public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
        {
            var UserRecord = Users.Where(u => u.Item3 == identifier).FirstOrDefault();

            return UserRecord == null
                       ? null
                       : new UserIdentity { UserName = UserRecord.Item1 };
        }

        public static Guid? ValidateUser(string username, string password)
        {
            var UserRecord = Users.Where(u => u.Item1 == username && u.Item2 == password).FirstOrDefault();
            if (UserRecord == null)
            {
                return null;
            }
            return UserRecord.Item3;
        }
    }
}