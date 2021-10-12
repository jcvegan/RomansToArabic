using NUnit.Framework;
using RomanToArabic.Service.Implementations;
using RomanToArabic.Service.Interfaces;
using System;
using System.Collections.Generic;

namespace RomanToArabic.Service.Tests
{
    public class RomanToArabicConverterTest
    {
        private InputValidator _inputValidator;
        private IRomanToArabicConverter _arabicParser;

        [SetUp]
        public void Initialize()
        {
            var dictionary = new Dictionary<char, int>()
            {
                { 'I', 1 },
                { 'V', 5 },
                { 'X', 10 },
                { 'L', 50 },
                { 'C', 100 },
                { 'D', 500 },
                { 'M', 1000 }
            };

            _inputValidator = new InputValidator();

            _arabicParser = new RomanToArabicConverter(_inputValidator, dictionary);
        }

        [Test(Description = "Send null value")]
        public void TestNullValue()
        {
            Assert.Throws<ArgumentNullException>(() => _arabicParser.Convert(null));
        }

        [Test(Description = "Send empty value")]
        public void TestEmptyValue()
        {
            Assert.Throws<ArgumentNullException>(() => _arabicParser.Convert(""));
        }

        [Test(Description = "Send whitespace value")]
        public void TestWhiespaceValue()
        {
            Assert.Throws<ArgumentNullException>(() => _arabicParser.Convert(" "));
        }

        [Test(Description = "Send non-roman value")]
        public void TestNonRomanValue()
        {
            Assert.Throws<ArgumentException>(() => _arabicParser.Convert("Juan"));
        }

        [TestCase("I", 1, Description = "Test simple letter: I")]
        [TestCase("V", 5, Description = "Test simple letter: V")]
        [TestCase("X", 10, Description = "Test simple letter: X")]
        [TestCase("L", 50, Description = "Test simple letter: L")]
        [TestCase("C", 100, Description = "Test simple letter: C")]
        [TestCase("M", 1000, Description = "Test simple letter: M")]
        public void TestSimpleCases(string romanNumber, int expectedArabicValue)
        {
            var arabicNumber = _arabicParser.Convert(romanNumber);

            Assert.AreEqual(expectedArabicValue, arabicNumber);
        }

        [TestCase("IV", 4, Description = "Test simple letter: I")]
        [TestCase("IX", 9, Description = "Test simple letter: V")]
        [TestCase("XLIX", 49, Description = "Test simple letter: L")]
        [TestCase("XCIX", 99, Description = "Test simple letter: C")]
        [TestCase("CMXCIX", 999, Description = "Test simple letter: M")]
        public void TestLowerNearValues(string romanNumber, int expectedArabicValue)
        {
            var arabicNumber = _arabicParser.Convert(romanNumber);

            Assert.AreEqual(expectedArabicValue, arabicNumber);
        }
    }
}