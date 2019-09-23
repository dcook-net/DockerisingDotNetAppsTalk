using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MeetupMembersApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Host
                .CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webHostBuilder => {
                    webHostBuilder
                        .UseStartup<Startup>()
                        .UseUrls("http://*:9010")
                        .UseKestrel();
                })
                .ConfigureLogging(builder => builder.AddConsole())
                .ConfigureAppConfiguration(config => config.AddEnvironmentVariables())
                .Build()
                .Run();
        }
    }
}
