using System;
using System.Threading.Tasks;
using System.Xml;
using cadence.dotnet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Neon.Cadence;
using Neon.Common;

namespace WorkflowConcurrencyTest
{
    class Program
    {
        public static async Task Main(string[] args)
        {
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
                    services.AddHostedService<ZegoWorker>();
                    services.Configure<CadenceSettings>(hostContext.Configuration.GetSection(nameof(CadenceSettings)));
                    NeonHelper.ServiceContainer.AddLogging();
                    NeonHelper.ServiceContainer.AddScoped<ICorrelationHandler, CorrelationHandler>();
                });
        }
    }
}
