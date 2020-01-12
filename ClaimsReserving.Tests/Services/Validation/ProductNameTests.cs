using System.Collections.Generic;
using ClaimsReserving.Models;
using ClaimsReserving.Services.Validation;
using NUnit.Framework;

namespace ClaimsReserving.Tests.Services.Validation
{
    [TestFixture]
    public class ProductNameTests
    {
        private ProductName _target;

        [SetUp]
        public void Setup()
        {
            _target = new ProductName();
        }

        [Test]
        public void CreatesOneWarningWhenOneDifferentCasingInstance()
        {
            // Arrange
            var records = new List<YearlyData>()
            {
                new YearlyData { ProductName = "Comp"},
                new YearlyData { ProductName = "comp"},
                new YearlyData { ProductName = "Non-Comp"},
                new YearlyData { ProductName = "Non-Comp"},
            };

            // Act
            var result = _target.FindDifferentCasing(new Defects(), records);

            // Assert
            Assert.That(result.Warnings.Count, Is.EqualTo(1));
            Assert.That(result.Warnings[0], Is.EqualTo("Found 1 cases where one record product name is the same name but with different case to another record product name."));
        }

        [Test]
        public void CreatesOneWarningWhenTwoDifferentCasingInstances()
        {
            // Arrange
            var records = new List<YearlyData>()
            {
                new YearlyData { ProductName = "Comp"},
                new YearlyData { ProductName = "comp"},
                new YearlyData { ProductName = "COmp"},
                new YearlyData { ProductName = "Non-Comp"},
                new YearlyData { ProductName = "Non-Comp"},
            };

            // Act
            var result = _target.FindDifferentCasing(new Defects(), records);

            // Assert
            Assert.That(result.Warnings.Count, Is.EqualTo(1));
            Assert.That(result.Warnings[0], Is.EqualTo("Found 2 cases where one record product name is the same name but with different case to another record product name."));
        }
    }
}
