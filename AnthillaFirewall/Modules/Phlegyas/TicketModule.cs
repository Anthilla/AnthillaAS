using AnthillaCore.Compression;
using AnthillaPhlegyas;
using Nancy;

namespace AnthillaFirewall.Phlegyas {

    public class TicketModule : NancyModule {
        private AnthillaTicketGranting tkt = new AnthillaTicketGranting();

        public TicketModule()
            : base("/ticket") {
            //return bool
            Get["/{client}"] = x => {
                string client = x.client;
                var y = tkt.SetClientConnection(client);
                string json = JsonCompression.Set(y);
                return json;
            };
        }
    }
}