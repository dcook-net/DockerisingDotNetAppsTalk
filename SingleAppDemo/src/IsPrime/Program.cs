using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace IsPrime
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
                        .UseUrls("http://*:9021")
                        .UseKestrel();
                })
                .Build()
                .Run();
        }
    }
}