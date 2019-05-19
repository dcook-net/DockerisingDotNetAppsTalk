using MeetupMembersApi.Models;
using MeetupMembersApi.Mongo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MeetupMembersApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<MongoConfiguration>(Configuration.GetSection("Mongo"));
            
            services.AddSingleton<IMongoDatabaseProvider, MongoDatabaseProvider>();
            services.AddSingleton<IMongoClientBuilder, MongoClientBuilder>();
            services.AddSingleton<IMongoCollectionProvider<Member>, MongoCollectionProvider<Member>>();
            services.AddSingleton<IDataRepository<Member>, MongoDataRepository<Member>>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}