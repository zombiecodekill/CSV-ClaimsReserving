using System.Collections.Generic;
using ClaimsReserving.Models;
using ClaimsReserving.Services.Writer;
using NUnit.Framework;

namespace ClaimsReserving.Tests.Services.Writer
{
    [TestFixture]
    public class SummaryRecordTests
    {
        private SummaryRecord _target;

        private List<YearlyData> _records;

        [SetUp]
        public void Setup()
        {
            _target = new SummaryRecord();

            _records = new List<YearlyData>()
            {
                new YearlyData { ProductName = "Comp", DevelopmentYear = 1992, OriginYear = 1992, IncrementalValue = 110m },
                new YearlyData { ProductName = "Comp", DevelopmentYear = 1993, OriginYear = 1992, IncrementalValue = 170m },
                new YearlyData { ProductName = "Comp", DevelopmentYear = 1993, OriginYear = 1993, IncrementalValue = 200m },
                new YearlyData { ProductName = "Non-Comp", DevelopmentYear = 1990, OriginYear = 1990, IncrementalValue = 45.2m },
                new YearlyData { ProductName = "Non-Comp", DevelopmentYear = 1990, OriginYear = 1991, IncrementalValue = 64.8m },
                new YearlyData { ProductName = "Non-Comp", DevelopmentYear = 1990, OriginYear = 1993, IncrementalValue = 37.0m },
                new YearlyData { ProductName = "Non-Comp", DevelopmentYear = 1991, OriginYear = 1991, IncrementalValue = 50.0m },
                new YearlyData { ProductName = "Non-Comp", DevelopmentYear = 1991, OriginYear = 1992, IncrementalValue = 75.0m },
                new YearlyData { ProductName = "Non-Comp", DevelopmentYear = 1991, OriginYear = 1993, IncrementalValue = 25.0m },
                new YearlyData { ProductName = "Non-Comp", DevelopmentYear = 1992, OriginYear = 1992, IncrementalValue = 55.0m },
                new YearlyData { ProductName = "Non-Comp", DevelopmentYear = 1992, OriginYear = 1993, IncrementalValue = 85.0m },
                new YearlyData { ProductName = "Non-Comp", DevelopmentYear = 1993, OriginYear = 1993, IncrementalValue = 100.0m }
            };

        }

        [Test]
        public void CalculatesNullWhenNoData()
        {
            // Arrange
            var records = new List<YearlyData>();

            // Act
            var result = _target.Calculate(records);

            // Assert
            Assert.Null(result);
        }

        [Test]
        public void CalculatesEarliestOriginYearWhenDataExists()
        {
            // Act
            var result = _target.Calculate(_records);

            // Assert
            Assert.That(result[0], Is.EqualTo(1990));
        }

        [Test]
        public void CalculatesNumberOfDevelopmentYears()
        {
            // Act
            var result = _target.Calculate(_records);

            // Assert
            Assert.That(result[1], Is.EqualTo(4));
        }
    }
}
