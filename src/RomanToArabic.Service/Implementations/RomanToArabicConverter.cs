using RomanToArabic.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RomanToArabic.Service.Implementations
{
    public class RomanToArabicConverter : IRomanToArabicConverter
    {
        private readonly IDictionary<char, int> _romanNumeralsToArabicNumberMap;

        private readonly IInputValidator _inputValidator;

        public RomanToArabicConverter(IInputValidator inputValidator, IDictionary<char, int> romanNumeralsToArabicNumberMap)
        {
            _inputValidator = inputValidator;
            _romanNumeralsToArabicNumberMap = romanNumeralsToArabicNumberMap;
        }

        public int Convert(string romanInput)
        {
            if (string.IsNullOrEmpty(romanInput) || string.IsNullOrWhiteSpace(romanInput))
                throw new ArgumentNullException();

            romanInput = romanInput.ToUpperInvariant();

            var isRomanNumber = _inputValidator.IsRomanNumber(romanInput);

            if (!isRomanNumber)
                throw new ArgumentException("Invalid roman value");
            
            if (romanInput.Length == 1)
                return _romanNumeralsToArabicNumberMap[romanInput.First()];

            var total = 0;

            char currentChacter, nextCharacter;
            int currentValue, nextValue;

            for (int i = 0; i < romanInput.Length; i++)
            {
                currentChacter = romanInput[i];

                currentValue = _romanNumeralsToArabicNumberMap[currentChacter];

                var nextIndex = i + 1;

                if(nextIndex < romanInput.Length)
                {
                    nextCharacter = romanInput[i + 1];
                    nextValue = _romanNumeralsToArabicNumberMap[nextCharacter];
                }
                else
                {
                    nextValue = 0;
                }

                if (currentValue < nextValue)
                    total -= currentValue;
                else
                    total += currentValue;
            }

            return total;
        }
    }
}