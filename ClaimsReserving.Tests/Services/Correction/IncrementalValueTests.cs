using System.Collections.Generic;
using ClaimsReserving.Models;
using ClaimsReserving.Services.Correction;
using NUnit.Framework;

namespace ClaimsReserving.Tests.Services.Correction
{
    [TestFixture]
    public class IncrementalValueTests
    {
        [Test]
        public void MissingIncrementalValuesAreConsideredToBeZero()
        {
            // Arrange
            var records = new List<YearlyData>() {
                new YearlyData { ProductName = "Comp", OriginYear = 2017, DevelopmentYear = 2017,  IncrementalValue = null},
                new YearlyData { ProductName = "Comp", OriginYear = 2017, DevelopmentYear = 2018,  IncrementalValue = null},
                new YearlyData { ProductName = "Comp", OriginYear = 2017, DevelopmentYear = 2019,  IncrementalValue = null}
            };

            // Act
            var result = IncrementalValue.DefaultNullsToZeroes(records);

            Assert.That(result[0].IncrementalValue, Is.EqualTo(0));
            Assert.That(result[1].IncrementalValue, Is.EqualTo(0));
            Assert.That(result[2].IncrementalValue, Is.EqualTo(0));
        }

        [Test]
        public void NonNullIncrementalValuesAreUnchanged()
        {
            // Arrange
            var records = new List<YearlyData>() {
                new YearlyData { ProductName = "Comp", OriginYear = 2017, DevelopmentYear = 2017,  IncrementalValue = 10m},
                new YearlyData { ProductName = "Comp", OriginYear = 2017, DevelopmentYear = 2018,  IncrementalValue = 11m},
                new YearlyData { ProductName = "Comp", OriginYear = 2017, DevelopmentYear = 2019,  IncrementalValue = 12m}
            };

            // Act
            var result = IncrementalValue.DefaultNullsToZeroes(records);

            Assert.That(result[0].IncrementalValue, Is.EqualTo(10m));
            Assert.That(result[1].IncrementalValue, Is.EqualTo(11m));
            Assert.That(result[2].IncrementalValue, Is.EqualTo(12m));
        }
    }
}
