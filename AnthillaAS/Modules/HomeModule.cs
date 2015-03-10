using Nancy;
using Nancy.Security;

namespace AnthillaAS {

    public class HomeModule : NancyModule {

        public HomeModule() {
            //this.RequiresAuthentication();

            Get["/"] = x => {
                return View["page-home"];
            };
        }
    }
}