using Nancy;

namespace AnthillaFirewall {

    public class HomeModule : NancyModule {

        public HomeModule() {
            Get["/"] = x => {
                return View["home"];
            };
        }
    }
}