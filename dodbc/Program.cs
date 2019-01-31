using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace dodbc
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            var services = new ServiceCollection()
                .AddTransient<ITimeService>(s =>
                {
                    return new TimeService(configuration.GetConnectionString("DefaultConnection"));
                });

            var serviceProvider = services.BuildServiceProvider();
            var testService = serviceProvider.GetService<ITimeService>();
            Console.WriteLine(testService.Now());
        }        
    }
}
