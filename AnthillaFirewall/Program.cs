using System;
using System.Threading;
using AnthillaASCore;
using Microsoft.Owin.Hosting;
using Nancy;
using Owin;

namespace AnthillaFirewall
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
            var url = "http://+:9997/";
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine(ConsoleTime.GetTime(DateTime.Now) + "loading service");
                Console.WriteLine(ConsoleTime.GetTime(DateTime.Now) + "    service type -> appliant system . firewall");
                Console.WriteLine(ConsoleTime.GetTime(DateTime.Now) + "                 -> appliant system . firewall url -> {0}", url);
                Console.WriteLine(ConsoleTime.GetTime(DateTime.Now) + "service is now running");
                stop.WaitOne();
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
            Database.Start();
            Console.WriteLine(ConsoleTime.GetTime(DateTime.Now) + "    denso-db -> loaded");
            app.UseNancy();
            Console.WriteLine(ConsoleTime.GetTime(DateTime.Now) + "    nancy-fx -> loaded");
        }
    }

    public class Database
    {
        public static void Start()
        {
            //var root = "/framework/anthilla";
            //var root = "/mnt";
            var root = @"D:\Anthilla\AnthillaData";

            //Environment.SetEnvironmentVariable("ANTH_DB", root, EnvironmentVariableTarget.Machine);

            //Console.WriteLine();
            //Console.WriteLine("GetEnvironmentVariables: ");
            //foreach (DictionaryEntry de in Environment.GetEnvironmentVariables())
            //    Console.WriteLine("  {0} = {1}", de.Key, de.Value);

            //Environment.CurrentDirectory = Environment.GetEnvironmentVariable("windir");
            //DirectoryInfo info = new DirectoryInfo(".");
            //lock (info)
            //{
            //    Console.WriteLine("Directory Info:   " + info.FullName);
            //}

            Start(root);
        }

        public static void Start(string root)
        {
            DeNSo.Configuration.BasePath = new string[] { System.IO.Path.Combine(root, "AnthData") };
            DeNSo.Configuration.EnableJournaling = true;
            DeNSo.Configuration.DBCheckTimeSpan = new System.TimeSpan(0, 1, 0);
            DeNSo.Configuration.ReindexCheck = new System.TimeSpan(0, 1, 0);

            DeNSo.Session.DefaultDataBase = "Data";
            DeNSo.Session.Start();
        }

        public static void ShutDown()
        {
            DeNSo.Session.ShutDown();
        }
    }
}