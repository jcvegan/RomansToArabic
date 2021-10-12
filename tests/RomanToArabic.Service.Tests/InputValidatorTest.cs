using System;

using NUnit.Framework;

using RomanToArabic.Service.Interfaces;
using RomanToArabic.Service.Implementations;

namespace RomanToArabic.Service.Tests
{
    public class InputValidatorTest
    {
        private IInputValidator _inputValidator;

        [SetUp]
        public void Initialize()
        {
            _inputValidator = new InputValidator();
        }

        [Test]
        public void TestSendingNull()
        {
            Assert.Throws<ArgumentNullException>(() => _inputValidator.IsRomanNumber(null));
        }

        [Test]
        public void TestSendingEmpty()
        {
            Assert.Throws<ArgumentNullException>(() => _inputValidator.IsRomanNumber(""));
        }

        [Test]
        public void TestKnownRoman()
        {
            var isValid = _inputValidator.IsRomanNumber("XV");

            Assert.True(isValid);
        }

        [Test]
        public void TestAnything()
        {
            var isValid = _inputValidator.IsRomanNumber("anything");

            Assert.False(isValid);
        }
    }
}