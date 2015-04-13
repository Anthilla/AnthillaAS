///-------------------------------------------------------------------------------------
///     Copyright (c) 2014, Anthilla S.r.l. (http://www.anthilla.com)
///     All rights reserved.
///
///     Redistribution and use in source and binary forms, with or without
///     modification, are permitted provided that the following conditions are met:
///         * Redistributions of source code must retain the above copyright
///           notice, this list of conditions and the following disclaimer.
///         * Redistributions in binary form must reproduce the above copyright
///           notice, this list of conditions and the following disclaimer in the
///           documentation and/or other materials provided with the distribution.
///         * Neither the name of the Anthilla S.r.l. nor the
///           names of its contributors may be used to endorse or promote products
///           derived from this software without specific prior written permission.
///
///     THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
///     ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
///     WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
///     DISCLAIMED. IN NO EVENT SHALL ANTHILLA S.R.L. BE LIABLE FOR ANY
///     DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
///     (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
///     LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
///     ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
///     (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
///     SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
///
///     20150413
///-------------------------------------------------------------------------------------

using AnthillaAS.Config;
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

            string uri = SelfConfig.GetAntdUri();
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