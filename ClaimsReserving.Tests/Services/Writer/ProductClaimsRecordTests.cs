using System.Collections.Generic;
using ClaimsReserving.Models;
using ClaimsReserving.Services.Writer;
using NUnit.Framework;

namespace ClaimsReserving.Tests.Services.Writer
{
    [TestFixture]
    public class ProductClaimsRecordTests
    {
        private ProductClaimsRecord _target;
        private List<YearlyData> _records;
        private int _minYear = 1990;

        [SetUp]
        public void Setup()
        {
            _target = new ProductClaimsRecord();
            var origin90Dev90Comp = new YearlyData { ProductName = "Comp", OriginYear = 1990, DevelopmentYear = 1990, IncrementalValue = 0.0m };
            var origin90Dev91Comp = new YearlyData { ProductName = "Comp", OriginYear = 1990, DevelopmentYear = 1991, IncrementalValue = 0.0m };
            var origin90Dev92Comp = new YearlyData { ProductName = "Comp", OriginYear = 1990, DevelopmentYear = 1992, IncrementalValue = 0.0m };
            var origin90Dev93Comp = new YearlyData { ProductName = "Comp", OriginYear = 1990, DevelopmentYear = 1993, IncrementalValue = 0.0m };

            var origin91Dev91Comp = new YearlyData { ProductName = "Comp", OriginYear = 1991, DevelopmentYear = 1991, IncrementalValue = 0.0m };
            var origin91Dev92Comp = new YearlyData { ProductName = "Comp", OriginYear = 1991, DevelopmentYear = 1992, IncrementalValue = 0.0m };
            var origin91Dev93Comp = new YearlyData { ProductName = "Comp", OriginYear = 1991, DevelopmentYear = 1993, IncrementalValue = 0.0m };

            var origin92Dev92Comp = new YearlyData { ProductName = "Comp", OriginYear = 1992, DevelopmentYear = 1992, IncrementalValue = 110.0m };
            var origin92Dev93Comp = new YearlyData { ProductName = "Comp", OriginYear = 1992, DevelopmentYear = 1993, IncrementalValue = 170.0m };

            var origin93Dev93Comp = new YearlyData { ProductName = "Comp", OriginYear = 1993, DevelopmentYear = 1993, IncrementalValue = 200.0m };

            _records = new List<YearlyData>
            {
                origin90Dev90Comp, origin90Dev91Comp, origin90Dev92Comp, origin90Dev93Comp,
                origin91Dev91Comp, origin91Dev92Comp, origin91Dev93Comp,
                origin92Dev92Comp, origin92Dev93Comp,
                origin93Dev93Comp
            };

        }

        [Test]
        public void CalculatesRunningTotalsForCompProduct()
        {
            // Arrange
            var minAndMaxYears = new MinAndMaxYears() { Minimum = 1990, Maximum = 1993 };

            // Act
            var result = _target.RunningTotals(_records, minAndMaxYears);

            Assert.That(result[0], Is.EqualTo("0"));
            Assert.That(result[1], Is.EqualTo("0"));
            Assert.That(result[2], Is.EqualTo("0"));
            Assert.That(result[3], Is.EqualTo("0"));
            Assert.That(result[4], Is.EqualTo("0"));
            Assert.That(result[5], Is.EqualTo("0"));
            Assert.That(result[6], Is.EqualTo("0"));
            Assert.That(result[7], Is.EqualTo("110"));
            Assert.That(result[8], Is.EqualTo("280"));
            Assert.That(result[9], Is.EqualTo("200"));
        }

        #region FormattedNumber

        [Test]
        public void FormatsIntegersWithoutTheDecimalPoint()
        {
            var result = _target.FormattedNumber(0.0m);

            // Assert
            Assert.That(result, Is.EqualTo("0"));
        }

        [Test]
        public void DoesNotFormatsDecimalsWithoutTheDecimalPoint()
        {
            var result = _target.FormattedNumber(1.23m);

            // Assert
            Assert.That(result, Is.EqualTo("1.23"));
        }

        #endregion

        #region FindIncrementalValue


        [Test]
        public void FindsIncrementalValueForOrigin1990Dev1990()
        {
            var actual = _target.FindIncrementalValue(
                _records, _minYear, xdiff: 0, ydiff: 0);
            Assert.AreEqual(0.0m, actual);
        }

        [Test]
        public void FindsIncrementalValueForOrigin1990Dev1991()
        {
            var actual = _target.FindIncrementalValue(
                _records, _minYear, xdiff: 1, ydiff: 0);
            Assert.AreEqual(0.0m, actual);
        }

        [Test]
        public void FindsIncrementalValueForOrigin1990Dev1992()
        {
            var actual = _target.FindIncrementalValue(
                _records, _minYear, xdiff: 2, ydiff: 0);
            Assert.AreEqual(0.0m, actual);
        }

        [Test]
        public void FindsIncrementalValueForOrigin1990Dev1993()
        {
            var actual = _target.FindIncrementalValue(
                _records, _minYear, xdiff: 3, ydiff: 0);
            Assert.AreEqual(0.0m, actual);
        }

        [Test]
        public void FindsIncrementalValueForOrigin1991Dev1991()
        {
            var actual = _target.FindIncrementalValue(
                _records, _minYear, xdiff: 1, ydiff: 1);
            Assert.AreEqual(0.0m, actual);
        }


        [Test]
        public void FindsIncrementalValueForOrigin1991Dev1992()
        {
            var actual = _target.FindIncrementalValue(
                _records, _minYear, xdiff: 1, ydiff: 1);
            Assert.AreEqual(0.0m, actual);
        }


        [Test]
        public void FindsIncrementalValueForOrigin1991Dev1993()
        {
            var actual = _target.FindIncrementalValue(
                _records, _minYear, xdiff: 2, ydiff: 1);
            Assert.AreEqual(0.0m, actual);
        }

        [Test]
        public void FindsIncrementalValueForOrigin1992Dev1992()
        {
            var actual = _target.FindIncrementalValue(
                _records, _minYear, 0, 2);
            Assert.AreEqual(110.0m, actual);
        }

        [Test]
        public void FindsIncrementalValueForOrigin1992Dev1993()
        {
            var actual = _target.FindIncrementalValue(
                _records, _minYear, 1, 2);
            Assert.AreEqual(170.0m, actual);
        }

        #endregion


    }
}
