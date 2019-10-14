using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nancy.Owin;
using NancyApplication.Services;

namespace NancyApplication
{
   public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }
        public IServiceCollection Services { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add database access service
            services.AddScoped<INoteService>( service => { 
                 return new PGNoteService(Configuration.GetConnectionString("DefaultConnection"));
            });

            Services = services;
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseOwin().UseNancy(x => x.Bootstrapper = new AutoBootstraper(Services));
        }
   }
}
