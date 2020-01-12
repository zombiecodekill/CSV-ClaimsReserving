using System;
using ClaimsReserving.Models;
using ClaimsReserving.Services.Validation.Record;
using NUnit.Framework;

namespace ClaimsReserving.Tests.Services.Validation.Record
{
    [TestFixture]
    public class YearValidatorTests
    {
        private YearValidator _target;

        [SetUp]
        public void Setup()
        {
            _target = new YearValidator(new Defects());
        }

        [Test]
        public void TwoErrorsWhenDevelopmentYearIs1066AndOlderThanOriginYear()
        {
            // Arrange
            var record = new YearlyData {DevelopmentYear = 1066, OriginYear = 2000, ProductName = "Comp"};

            // Act
            var result = _target.Validate(record);

            // Assert
            Assert.That(result.Errors[0], Is.EqualTo("The Development Year 1066 is less than the Origin Year 2000"));
            Assert.That(result.Errors[1], Is.EqualTo("The Development Year 1066 is too old"));
        }


        [Test]
        public void WarningWhenOriginYearInTheFuture()
        {
            // It is also reasonable to consider this case an error - would normally check something like this with a B.A.

            // Arrange
            var record = new YearlyData { DevelopmentYear = 3000, OriginYear = 3000, ProductName = "Comp" };

            // Act
            var result = _target.Validate(record);

            // Assert
            Assert.That(result.Warnings[0], Is.EqualTo("The Origin Year 3000 post dates the year that this PC’s system clock is set to (" 
                                                       + DateTime.Now.Year + ")."));

        }
    }
}
