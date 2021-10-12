using System;
using System.Text.RegularExpressions;

using RomanToArabic.Service.Interfaces;

namespace RomanToArabic.Service.Implementations
{
    public class InputValidator : IInputValidator
    {
        private const string RomanNumberPattern = @"^M{0,3}(CM|CD|D?C{0,3})?(XC|XL|L?X{0,3})?(IX|IV|V?I{0,3})?$";

        public bool IsRomanNumber(string inputText)
        {
            if(string.IsNullOrEmpty(inputText) || string.IsNullOrWhiteSpace(inputText))
                throw new ArgumentNullException("Input cannot be null or empty");

            var regex = new Regex(RomanNumberPattern);

            var isValidRomanNumber = regex.IsMatch(inputText);

            return isValidRomanNumber;
        }
    }
}