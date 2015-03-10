//using AnthillaCore;
using Nancy;
using System;

namespace AnthillaFirewall {

    public class FirewallModule : NancyModule {

        public FirewallModule()
            : base("/firewall") {
            Get["/action"] = x => {
                //OpenFile.Open();
                Console.Beep();
                return View["home"];
            };
        }
    }
}