using Microsoft.AspNetCore.Hosting;

namespace IsPrime
{
    class Program
    {
        public static void Main()
        {
            new WebHostBuilder()
                .UseUrls("http://*:9021")
                .UseStartup<Startup>()
                .UseKestrel()
                .Build()
                .Run();
        }
    }
}