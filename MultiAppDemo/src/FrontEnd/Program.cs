using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FrontEnd
{
    public class Program
    {
        public static void Main()
        {
            var host = new WebHostBuilder()
                .UseUrls("http://*:8080")
                .UseKestrel()
                .ConfigureAppConfiguration(config => config.AddEnvironmentVariables())
                .ConfigureLogging(builder => builder.AddConsole())
                .UseStartup<Startup>()
                .Build();
            
            host.Run();
        }
    }
}