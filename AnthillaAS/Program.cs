using System;
using System.Threading;
using AnthillaASCore;
using Microsoft.Owin.Hosting;
using Nancy;
using Owin;

namespace AnthillaAS
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var stop = new ManualResetEvent(false);
            Console.CancelKeyPress +=
                (sender, e) =>
                {
                    Console.WriteLine("^C");
                    stop.Set();
                    e.Cancel = true;
                };
            var url = "http://+:8084/";
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine(ConsoleTime.GetTime(DateTime.Now) + "loading service");
                Console.WriteLine(ConsoleTime.GetTime(DateTime.Now) + "    service type -> client");
                Console.WriteLine(ConsoleTime.GetTime(DateTime.Now) + "                 -> client url -> {0}", url);
                stop.WaitOne();
                Console.WriteLine(ConsoleTime.GetTime(DateTime.Now) + "service is now running");
                Console.WriteLine("");
            }
        }
    }

    internal class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Console.WriteLine(ConsoleTime.GetTime(DateTime.Now) + "loading service configuration");
            //var configuration = new HubConfiguration { EnableDetailedErrors = true };
            //app.MapSignalR(configuration);
            StaticConfiguration.DisableErrorTraces = false;
            Console.WriteLine(ConsoleTime.GetTime(DateTime.Now) + "    disableerrortraces -> false");
            app.UseNancy();
            Console.WriteLine(ConsoleTime.GetTime(DateTime.Now) + "    nancy-fx -> loaded");
        }
    }
}