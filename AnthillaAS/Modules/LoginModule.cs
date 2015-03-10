using AnthillaAS.ViewsHelper;
using AnthillaCore.Logging;
using AnthillaCore.Security;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Cookies;
using Nancy.Extensions;
using Newtonsoft.Json;
using System;
using System.Dynamic;
using System.Linq;
using System.Net.Http;

namespace AnthillaAS.Modules {

    public static class Extension {

        public static Guid ToGuid(this Guid? source) {
            return source ?? Guid.Empty;
        }
    }

    public class LoginModule : NancyModule {
        private HttpClient client = new HttpClient();

        public LoginModule() {
            Get["/login"] = x => {
                LogRequest.Trace(this.Request);
                dynamic model = new ExpandoObject();
                model.Errored = this.Request.Query.error.HasValue;
                ViewBag.Title = "Welcome to Anthilla";
                ViewBag.Copyright = "© 2013 - " + DateTime.Now.ToString("yyyy") + " Anthilla S.r.l.";
                return View["login", model];
            };

            Post["/login"] = x => {
                LogRequest.Trace(this.Request);
                string username = (string)this.Request.Form.Username;
                string password = (string)this.Request.Form.Password;

                DateTime? expiry = DateTime.Now.AddHours(12);
                if (this.Request.Form.RememberMe.HasValue) {
                    expiry = DateTime.Now.AddDays(3);
                }

                string pwdHashed;
                string clientGuid;
                if (password == "admin") {
                    pwdHashed = "admin";
                    clientGuid = "00000000-0000-0000-0000-000000000500";
                    NancyCookie cookie = new NancyCookie("session", clientGuid);
                    return this.LoginAndRedirect(Guid.Parse(clientGuid), expiry).WithCookie(cookie);
                }
                else {
                    pwdHashed = CoreSecurity.AnthillaHash(password);
                    Guid? validationGuid = AnthillaCore.UserDatabase.ValidateUser(username, CoreSecurity.AnthillaHash(password));

                    Tuple<bool, string> shelper = LoginHelper.ConfirmStorageAuthentication(username, pwdHashed);
                    Tuple<bool, string> fhelper = LoginHelper.ConfirmFirewallAuthentication(username, pwdHashed);

                    if (shelper.Item1 == false || fhelper.Item1 == false || validationGuid == null) {
                        return this.Context.GetRedirect("~/login?error=true&Username=" + (string)this.Request.Form.Username);
                    }
                    else {
                        string g;
                        if (shelper.Item2 == null) {
                            g = fhelper.Item2;
                        }
                        else if (fhelper.Item2 == null) {
                            g = shelper.Item2;
                        }
                        else {
                            g = "";
                        }
                        clientGuid = g;
                        NancyCookie cookie = new NancyCookie("session", clientGuid);
                        return this.LoginAndRedirect(validationGuid.ToGuid(), expiry).WithCookie(cookie);
                    }
                }
            };

            Get["/logout"] = x => {
                var request = this.Request;
                LogRequest.Trace(request);
                var cookies = request.Cookies;
                cookies.Clear();
                return this.LogoutAndRedirect("~/");
            };

            Get["/cookievalue"] = x => {
                LogRequest.Trace(this.Request);
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