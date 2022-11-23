using System;
using System.Threading.Tasks;
using System.Xml;
using cadence.dotnet;
using cadence.dotnet.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Neon.Cadence;
using Neon.Common;
using Neon.Diagnostics;

namespace WorkflowConcurrencyTest
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            LogManager.Default.SetLogLevel("debug");
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            //Microsoft DI
            return Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(configHost =>
                    configHost.AddEnvironmentVariables("ASPNETCORE_"))
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddZegoCadence(hostContext.Configuration);
                });
        }
    }
}
