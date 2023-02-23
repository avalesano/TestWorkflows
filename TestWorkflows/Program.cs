using System.Threading.Tasks;
using cadence.dotnet.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Neon.Diagnostics;

namespace TestWorkflows
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
