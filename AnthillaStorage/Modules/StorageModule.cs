using System;
using Nancy;

namespace AnthillaStorage
{
    public class StorageModule : NancyModule
    {
        public StorageModule()
            : base("/storage")
        {
            Get["/action"] = x =>
            {
                Console.Beep();
                return View["home"];
            };
        }
    }
}