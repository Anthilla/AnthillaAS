using AnthillaCore;
using AnthillaCore.Configuration;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using Nancy;
using Owin;
using System;
using System.Threading;

namespace AnthillaAS {

    internal static class Program {

        private static void Main(string[] args) {
            DateTime startTime = DateTime.Now;
            Console.Title = "AnthillaAS";
            Thread.Sleep(2000);
            var stop = new ManualResetEvent(false);
            Console.CancelKeyPress +=
                (sender, e) => {
                    Console.WriteLine("^C");
                    stop.Set();
                    e.Cancel = true;
                };

            string uri = StorageConfig.GetAnthillaASUri();
            Console.WriteLine(ConsoleTime.GetTime(DateTime.Now) + "initializing anthillaas");
            using (WebApp.Start<Startup>(uri)) {
                Console.WriteLine(ConsoleTime.GetTime(DateTime.Now) + "loading service");
                Console.WriteLine(ConsoleTime.GetTime(DateTime.Now) + "    service type -> client");
                Console.WriteLine(ConsoleTime.GetTime(DateTime.Now) + "                 -> client url -> {0}", uri);
                Console.WriteLine(ConsoleTime.GetTime(DateTime.Now) + "service is now running");
                var elapsed = DateTime.Now - startTime;
                Console.WriteLine(ConsoleTime.GetTime(DateTime.Now) + "loaded in: " + elapsed);
                stop.WaitOne();
                Console.WriteLine("");
            }
        }
    }

    internal class Startup {

        public void Configuration(IAppBuilder app) {
            Console.WriteLine(ConsoleTime.GetTime(DateTime.Now) + "loading service configuration");
            var hubConfiguration = new HubConfiguration { EnableDetailedErrors = false };
            app.MapSignalR(hubConfiguration);
            Console.WriteLine(ConsoleTime.GetTime(DateTime.Now) + "    signalR -> loaded");
            bool errorTrace = false;
            StaticConfiguration.DisableErrorTraces = errorTrace;
            Console.WriteLine(ConsoleTime.GetTime(DateTime.Now) + "    disableerrortraces -> {0}", errorTrace);
            app.UseNancy();
            Console.WriteLine(ConsoleTime.GetTime(DateTime.Now) + "    nancy-fx -> loaded");
        }
    }
}