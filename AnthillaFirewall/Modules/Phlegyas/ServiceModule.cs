using AnthillaCore.Compression;
using AnthillaPhlegyas;
using Nancy;

namespace AnthillaFirewall.Phlegyas {

    public class ServiceModule : NancyModule {
        private AnthillaServiceProviding provide = new AnthillaServiceProviding();

        public ServiceModule()
            : base("/phlegyas") {
            //return string service
            Get["/{client}/service/request"] = x => {
                string client = x.client;
                var y = provide.GetService(client);
                string json = JsonCompression.Set(y);
                return json;
            };
        }
    }
}