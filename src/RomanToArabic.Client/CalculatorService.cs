using Microsoft.Extensions.Logging;
using RomanToArabic.Service.Interfaces;
using System;

namespace RomanToArabic.Client
{
    public class CalculatorService
    {
        private readonly IRomanToArabicConverter _converter;
        private readonly ILogger<CalculatorService> _logger;

        public CalculatorService(IRomanToArabicConverter converter, ILogger<CalculatorService> logger)
        {
            _converter = converter;
            _logger = logger;
        }

        public void Calculate(string input)
        {
            try
            {
                var result = _converter.Convert(input);

                _logger.LogInformation($"{result}");
            }
            catch(Exception exc)
            {
                _logger.LogError(exc, exc.Message);
            }
        }
    }
}
