﻿using System;
using Mono.Unix;
using Mono.Unix.Native;
using Nancy;
using Nancy.Hosting.Self;

namespace nancymonotest
{
    class Program
    {
        static void Main(string[] args)
        {
            var uri = "http://localhost:8888";
            Console.WriteLine("Starting Nancy on " + uri);

            // initialize an instance of NancyHost
            var hostConfiguration = new HostConfiguration();
            hostConfiguration.UrlReservations = new UrlReservations
            {
                CreateAutomatically = true
            };

            var host = new NancyHost(new Uri(uri), new DefaultNancyBootstrapper(), hostConfiguration);
     
            host.Start();  // start hosting

            // check if we're running on mono
            if (Type.GetType("Mono.Runtime") != null)
            {
                // on mono, processes will usually run as daemons - this allows you to listen
                // for termination signals (ctrl+c, shutdown, etc) and finalize correctly
                UnixSignal.WaitAny(new[] {
                    new UnixSignal(Signum.SIGINT),
                    new UnixSignal(Signum.SIGTERM),
                    new UnixSignal(Signum.SIGQUIT),
                    new UnixSignal(Signum.SIGHUP)
                });
            }
            else
            {
                Console.ReadLine();
            }

            Console.WriteLine("Stopping Nancy");
            host.Stop();  // stop hosting
        }
    }
}
