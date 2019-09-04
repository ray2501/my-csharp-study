using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace consoledi
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
                .AddTransient<ITestService>( s => {
                  return new TestService(configuration["Setting:Prefix"]);
                });

            var serviceProvider = services.BuildServiceProvider();
            var testService = serviceProvider.GetService<ITestService>();
            testService.Print("Hello, Dependency Injection.");
        }
    }
}