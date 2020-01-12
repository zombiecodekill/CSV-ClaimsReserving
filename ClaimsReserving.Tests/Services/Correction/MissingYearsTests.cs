using System.Collections.Generic;
using ClaimsReserving.Models;
using ClaimsReserving.Services.Correction;
using NUnit.Framework;

namespace ClaimsReserving.Tests.Services.Correction
{
    [TestFixture]
    public class MissingYearsTests
    {
        private MissingYears _target;
        private List<YearlyData> _records;
        private MinAndMaxYears _minAndMaxYears;

        [SetUp]
        public void Setup()
        {
            _target = new MissingYears();
        }

        [Test]
        public void AddsMissingYearsForCompProduct()
        {
            // Arrange
            _records = new List<YearlyData>()
            {
                new YearlyData { ProductName = "Comp", DevelopmentYear = 1992, OriginYear = 1992, IncrementalValue = 110m },
                new YearlyData { ProductName = "Comp", DevelopmentYear = 1993, OriginYear = 1992, IncrementalValue = 170m },
                new YearlyData { ProductName = "Comp", DevelopmentYear = 1993, OriginYear = 1993, IncrementalValue = 200m }
            };

            _minAndMaxYears = new MinAndMaxYears { Minimum = 1990, Maximum = 1993 };

            // Act
            var result = _target.AddForProduct(_records, _minAndMaxYears);

            // Assert
            Assert.That(result.Count, Is.EqualTo(10));

            Assert.That(result[0].DevelopmentYear, Is.EqualTo(1992), "1st record - Development Year");
            Assert.That(result[0].OriginYear, Is.EqualTo(1992), "1st record - Origin Year");

            Assert.That(result[1].DevelopmentYear, Is.EqualTo(1993), "2nd record - Development Year");
            Assert.That(result[1].OriginYear, Is.EqualTo(1992), "2nd record - Origin Year");

            Assert.That(result[2].DevelopmentYear, Is.EqualTo(1993), "3rd record - Development Year");
            Assert.That(result[2].OriginYear, Is.EqualTo(1993), "3rd record - Origin Year");

            Assert.That(result[3].DevelopmentYear, Is.EqualTo(1990), "4th record - Development Year");
            Assert.That(result[3].OriginYear, Is.EqualTo(1990), "4th record - Origin Year");

            Assert.That(result[4].DevelopmentYear, Is.EqualTo(1991), "5th record - Development Year");
            Assert.That(result[4].OriginYear, Is.EqualTo(1990), "5th record - Origin Year");

            Assert.That(result[5].DevelopmentYear, Is.EqualTo(1992), "6th record - Development Year");
            Assert.That(result[5].OriginYear, Is.EqualTo(1990), "6th record - Origin Year");

            Assert.That(result[6].DevelopmentYear, Is.EqualTo(1993), "7th record - Development Year");
            Assert.That(result[6].OriginYear, Is.EqualTo(1990), "7th record - Development Year");

            Assert.That(result[7].DevelopmentYear, Is.EqualTo(1991), "8th record - Development Year");
            Assert.That(result[7].OriginYear, Is.EqualTo(1991), "8th record - Origin Year");

            Assert.That(result[8].DevelopmentYear, Is.EqualTo(1992), "9th record - Development Year");
            Assert.That(result[8].OriginYear, Is.EqualTo(1991), "9th record - Origin Year");

            Assert.That(result[9].DevelopmentYear, Is.EqualTo(1993), "10th record - Development Year");
            Assert.That(result[9].OriginYear, Is.EqualTo(1991), "10th record - Origin Year");
        }

        [Test]
        public void AddsMissingYearForNonCompProduct()
        {
            // Arrange
            _records = new List<YearlyData>()
            {
                new YearlyData { ProductName = "Non-Comp", DevelopmentYear = 1990, OriginYear = 1990, IncrementalValue = 45.2m },
                // Missing DevelopmentYear 1992
                new YearlyData { ProductName = "Non-Comp", DevelopmentYear = 1991, OriginYear = 1990, IncrementalValue = 64.8m },
                new YearlyData { ProductName = "Non-Comp", DevelopmentYear = 1993, OriginYear = 1990, IncrementalValue = 37.0m },
                new YearlyData { ProductName = "Non-Comp", DevelopmentYear = 1991, OriginYear = 1991, IncrementalValue = 50.0m },
                new YearlyData { ProductName = "Non-Comp", DevelopmentYear = 1992, OriginYear = 1991, IncrementalValue = 75.0m },
                new YearlyData { ProductName = "Non-Comp", DevelopmentYear = 1993, OriginYear = 1991, IncrementalValue = 25.0m },
                new YearlyData { ProductName = "Non-Comp", DevelopmentYear = 1992, OriginYear = 1992, IncrementalValue = 55.0m },
                new YearlyData { ProductName = "Non-Comp", DevelopmentYear = 1993, OriginYear = 1992, IncrementalValue = 85.0m },
                new YearlyData { ProductName = "Non-Comp", DevelopmentYear = 1993, OriginYear = 1993, IncrementalValue = 100.0m }
            };

            _minAndMaxYears = new MinAndMaxYears { Minimum = 1990, Maximum = 1993 };

            // Act
            var result = _target.AddForProduct(_records, _minAndMaxYears);

            // Assert
            Assert.That(result.Count, Is.EqualTo(10));

            Assert.That(result[9].DevelopmentYear, Is.EqualTo(1992), "10th record - Development Year");
            Assert.That(result[9].OriginYear, Is.EqualTo(1990), "10th record - Origin Year");
        }
    }
}
