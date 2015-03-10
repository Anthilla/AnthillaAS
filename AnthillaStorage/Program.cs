using AnthillaCore;
using AnthillaCore.Configuration;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using Nancy;
using Owin;
using System;
using System.Threading;

namespace AnthillaStorage {

    internal static class Program {

        private static void Main(string[] args) {
            DateTime startTime = DateTime.Now;
            Console.Title = "AnthillaStorage";

            Console.WriteLine(ConsoleTime.GetTime(DateTime.Now) + "loading application...");

            StorageConfig.WriteDefaults();
            Console.WriteLine(ConsoleTime.GetTime(DateTime.Now) + "setting core storage configuration...");

            var stop = new ManualResetEvent(false);
            Console.CancelKeyPress +=
                (sender, e) => {
                    Console.WriteLine("^C");
                    Database.ShutDown();
                    stop.Set();
                    e.Cancel = true;
                };

            string uri = StorageConfig.GetAnthillaStorageUri();
            Console.WriteLine(ConsoleTime.GetTime(DateTime.Now) + "initializing anthillastorage");
            using (WebApp.Start<Startup>(uri)) {
                Console.WriteLine(ConsoleTime.GetTime(DateTime.Now) + "loading service");
                Console.WriteLine(ConsoleTime.GetTime(DateTime.Now) + "    service type -> server");
                Console.WriteLine(ConsoleTime.GetTime(DateTime.Now) + "                 -> server url -> {0}", uri);
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
            Database.Start();
            Console.WriteLine(ConsoleTime.GetTime(DateTime.Now) + "    denso-db -> loaded");
            app.UseNancy();
            Console.WriteLine(ConsoleTime.GetTime(DateTime.Now) + "    nancy-fx -> loaded");
        }
    }

    public class Database {

        public static void Start() {
            string[] databases;

            string root = StorageConfig.GetAnthillaDb();
            databases = new string[] { root };

            string raid = StorageConfig.GetAnthillaDbRaid();
            if (raid != null && raid != "") {
                databases = new string[] { root, raid };
            }

            DeNSo.Configuration.BasePath = databases;
            DeNSo.Configuration.EnableJournaling = true;
            DeNSo.Configuration.DBCheckTimeSpan = new System.TimeSpan(0, 1, 0);
            DeNSo.Configuration.ReindexCheck = new System.TimeSpan(0, 1, 0);
            DeNSo.Configuration.EnableOperationsLog = false;
            string db = "Database";
            DeNSo.Session.DefaultDataBase = db;
            DeNSo.Session.Start();
        }

        public static void ShutDown() {
            DeNSo.Session.ShutDown();
        }
    }
}