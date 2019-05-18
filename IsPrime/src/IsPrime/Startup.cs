using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace IsPrime
{
    public class Startup
    {
        public void Configure(IApplicationBuilder appBuilder)
        {
            appBuilder.UseMvc();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }
    }
}