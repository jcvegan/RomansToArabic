using Microsoft.Extensions.DependencyInjection;
using RomanToArabic.Service.Implementations;
using RomanToArabic.Service.Interfaces;
using System.Collections.Generic;

namespace RomanToArabic.Service
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddRomanToArabicCalculatorService(this IServiceCollection services, IDictionary<char, int> romanToArabicMap)
        {
            services.AddSingleton<IDictionary<char, int>>(romanToArabicMap);

            services.AddTransient<IInputValidator, InputValidator>();
            services.AddTransient<IRomanToArabicConverter, RomanToArabicConverter>();
            return services;
        }
    }
}