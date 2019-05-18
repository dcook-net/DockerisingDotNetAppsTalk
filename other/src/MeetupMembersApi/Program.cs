using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MeetupMembers
{
    public class Program
    {
        public static void Main()
        {
            var host = new WebHostBuilder()
                .UseUrls("http://*:9010")
                .UseKestrel()
                .UseStartup<Startup>()
                .ConfigureLogging(b => b.AddConsole())
                .ConfigureAppConfiguration(config => config.AddEnvironmentVariables())
                .Build();
            
            host.Run();
        }
    }
}
