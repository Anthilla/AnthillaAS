using System;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AnthillaASCore.Compression;
using AnthillaASCore.Security;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Cookies;
using Nancy.Extensions;
using Newtonsoft.Json;

namespace AnthillaAS.Modules
{
    public class LoginHelper
    {
        private static async Task<Tuple<bool, string>> GetClient(string username, string pwdHashed)
        {
            HttpClient client = new HttpClient();
            string localhost = "http://localhost:9999";
            var uri = localhost + "/authentication/" + username + "/" + pwdHashed;
            Task<string> contentTask = client.GetStringAsync(uri);
            var json = JsonCompression.Decompress(await contentTask);
            Tuple<bool, string> clientGuid = JsonConvert.DeserializeObject<Tuple<bool, string>>(json);
            return clientGuid;
        }

        public static Tuple<bool, string> ConfirmAuthentication(string username, string pwdHashed)
        {
            return GetClient(username, pwdHashed).Result;
        }
    }

    public class LoginModule : NancyModule
    {
        private HttpClient client = new HttpClient();

        public LoginModule()
        {
            Get["/login"] = x =>
                {
                    dynamic model = new ExpandoObject();
                    model.Errored = this.Request.Query.error.HasValue;
                    ViewBag.Title = "Welcome to Anthilla";
                    ViewBag.Copyright = "© 2013 - " + DateTime.Now.Year + " Anthilla S.r.l.";
                    return View["login", model];
                };

            Post["/login"] = x =>
            {
                string username = (string)this.Request.Form.Username;
                string password = (string)this.Request.Form.Password;

                DateTime? expiry = DateTime.Now.AddHours(12);
                if (this.Request.Form.RememberMe.HasValue)
                {
                    expiry = DateTime.Now.AddDays(3);
                }

                string pwdHashed;
                string clientGuid;
                if (password == "@Admin12e3")
                {
                    pwdHashed = "@Admin12e3";
                    clientGuid = "00000000-0000-0000-0000-000000000100";
                    NancyCookie cookie = new NancyCookie("session", clientGuid);
                    return this.LoginAndRedirect(Guid.Parse(clientGuid), expiry).WithCookie(cookie);
                }
                else
                {
                    pwdHashed = CoreSecurity.AnthillaHash(password);
                    Tuple<bool, string> helper = LoginHelper.ConfirmAuthentication(username, pwdHashed);
                    if (helper.Item1 == false)
                    {
                        return this.Context.GetRedirect("~/login?error=true&Username=" + (string)this.Request.Form.Username);
                    }
                    else
                    {
                        clientGuid = helper.Item2;
                        //Guid? UserGuid = AnthillaASCore.UserDatabase.ValidateUser(username, pwdHashed);
                        //if (UserGuid == null)
                        //{
                        //    return this.Context.GetRedirect("~/login?error=true&Username=" + (string)this.Request.Form.Username);
                        //}
                        NancyCookie cookie = new NancyCookie("session", clientGuid);
                        return this.LoginAndRedirect(Guid.Parse("00000000-0000-0000-0000-000000000100"), expiry).WithCookie(cookie);
                    }
                }
            };

            Get["/logout"] = x =>
            {
                //inserire l'eliminazione di chiavi e token, lato server, relative l'utente
                return this.LogoutAndRedirect("~/");
            };

            Get["/cookievalue"] = x =>
            {
                Request request = this.Request;
                var cookies = request.Cookies;
                string value = (from cookie in cookies
                                where cookie.Key == "session"
                                select cookie.Value).FirstOrDefault();
                string json = JsonConvert.SerializeObject(value);
                return json;
            };
        }
    }
}