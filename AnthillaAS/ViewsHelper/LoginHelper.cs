using AnthillaCore.Compression;
using AnthillaCore.Configuration;
using AnthillaCore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AnthillaAS.ViewsHelper {

    public class LoginHelper {
        private static HttpClient client = new HttpClient();
        private static string storage = StorageConfig.GetAnthillaStorageUri();
        private static string firewall = FirewallConfig.GetAnthillaFirewallUri();

        private static async Task<Tuple<bool, string>> GetStorageClient(string username, string pwdHashed) {
            var uri = storage + "/authentication/" + username + "/" + pwdHashed;
            Task<string> contentTask = client.GetStringAsync(uri);
            var json = JsonCompression.Decompress(await contentTask);
            Tuple<bool, string> clientGuid = JsonConvert.DeserializeObject<Tuple<bool, string>>(json);
            return clientGuid;
        }

        public static Tuple<bool, string> ConfirmStorageAuthentication(string username, string pwdHashed) {
            return GetStorageClient(username, pwdHashed).Result;
        }

        private async Task<List<Anth_Dump>> SGUSERS() {
            var uri = storage + "/user";
            Task<string> contentTask = client.GetStringAsync(uri);
            var json = JsonCompression.Decompress(await contentTask);
            List<Anth_Dump> eventItem = JsonConvert.DeserializeObject<List<Anth_Dump>>(json);
            return eventItem;
        }

        public List<Anth_Dump> StorageUserList() {
            return SGUSERS().Result;
        }

        private static async Task<Tuple<bool, string>> GetFirewallClient(string username, string pwdHashed) {
            var uri = firewall + "/authentication/" + username + "/" + pwdHashed;
            Task<string> contentTask = client.GetStringAsync(uri);
            var json = JsonCompression.Decompress(await contentTask);
            Tuple<bool, string> clientGuid = JsonConvert.DeserializeObject<Tuple<bool, string>>(json);
            return clientGuid;
        }

        public static Tuple<bool, string> ConfirmFirewallAuthentication(string username, string pwdHashed) {
            return GetFirewallClient(username, pwdHashed).Result;
        }

        private async Task<List<Anth_Dump>> FWUSERS() {
            var uri = firewall + "/user";
            Task<string> contentTask = client.GetStringAsync(uri);
            var json = JsonCompression.Decompress(await contentTask);
            List<Anth_Dump> eventItem = JsonConvert.DeserializeObject<List<Anth_Dump>>(json);
            return eventItem;
        }

        public List<Anth_Dump> FirewallUserList() {
            return FWUSERS().Result;
        }
    }
}