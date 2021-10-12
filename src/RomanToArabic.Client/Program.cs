using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RomanToArabic.Service.Installer;
using System.Collections.Generic;
using System.Linq;

namespace RomanToArabic.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = CreateHostBuilder(args).Build())
            {
                var service = host.Services.GetRequiredService<CalculatorService>();

                service.Calculate(args[0]);
            }
        }

        static IHostBuilder CreateHostBuilder(string[] args) 
        { 
            return Host.CreateDefaultBuilder(args)
                       .ConfigureAppConfiguration(ConfigureAppConfiguration) 
                       .ConfigureServices(ConfigureServices); 
        }
        
        static void ConfigureAppConfiguration(HostBuilderContext context, IConfigurationBuilder builder)
        {
            var environment = context.HostingEnvironment.EnvironmentName;

            builder.AddJsonFile("appsettings.json")
                   .AddJsonFile($"appsettings.{environment}.json", optional: true);
        }

        static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            IDictionary<char, int> romanNumeralsMap = context.Configuration.GetSection("RomanNumerals")
                                                               .Get<IDictionary<string,int>>()
                                                               .ToDictionary(kv => kv.Key[0], kv => kv.Value);

            services.AddRomanToArabicCalculatorService(romanNumeralsMap);

            services.AddTransient<CalculatorService>();
        }
    }
}
