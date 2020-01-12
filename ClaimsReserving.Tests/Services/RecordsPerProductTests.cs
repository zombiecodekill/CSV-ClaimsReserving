using System.Collections.Generic;
using ClaimsReserving.Models;
using ClaimsReserving.Services;
using NUnit.Framework;

namespace ClaimsReserving.Tests.Services
{
    [TestFixture]
    public class RecordsPerProductTests
    {
        private RecordsPerProduct _target;
        private List<YearlyData> _records;

        [SetUp]
        public void Setup()
        {
            _target = new RecordsPerProduct();

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

            var origin90Dev90NonComp = new YearlyData { ProductName = "Non-Comp", OriginYear = 1990, DevelopmentYear = 1990, IncrementalValue = 45.2m };
            var origin90Dev91NonComp = new YearlyData { ProductName = "Non-Comp", OriginYear = 1990, DevelopmentYear = 1991, IncrementalValue = 64.8m };

            var origin90Dev92NonComp = new YearlyData { ProductName = "Non-Comp", OriginYear = 1990, DevelopmentYear = 1992, IncrementalValue = 0.0m };
            var origin90Dev93NonComp = new YearlyData { ProductName = "Non-Comp", OriginYear = 1990, DevelopmentYear = 1993, IncrementalValue = 37.0m };

            var origin91Dev91NonComp = new YearlyData { ProductName = "Non-Comp", OriginYear = 1991, DevelopmentYear = 1991, IncrementalValue = 50.0m };
            var origin91Dev92NonComp = new YearlyData { ProductName = "Non-Comp", OriginYear = 1991, DevelopmentYear = 1992, IncrementalValue = 75.0m };
            var origin91Dev93NonComp = new YearlyData { ProductName = "Non-Comp", OriginYear = 1991, DevelopmentYear = 1993, IncrementalValue = 25.0m };

            var origin92Dev92NonComp = new YearlyData { ProductName = "Non-Comp", OriginYear = 1992, DevelopmentYear = 1992, IncrementalValue = 55.0m };
            var origin92Dev93NonComp = new YearlyData { ProductName = "Non-Comp", OriginYear = 1992, DevelopmentYear = 1993, IncrementalValue = 85.0m };

            var origin93Dev93NonComp = new YearlyData { ProductName = "Non-Comp", OriginYear = 1993, DevelopmentYear = 1993, IncrementalValue = 100.0m };



            _records = new List<YearlyData>
            {
                origin90Dev90Comp, origin90Dev91Comp, origin90Dev92Comp, origin90Dev93Comp,
                origin91Dev91Comp, origin91Dev92Comp, origin91Dev93Comp,
                origin92Dev92Comp, origin92Dev93Comp,
                origin93Dev93Comp,
                origin90Dev90NonComp, origin90Dev91NonComp, origin90Dev92NonComp, origin90Dev93NonComp,
                origin91Dev91NonComp, origin91Dev92NonComp, origin91Dev93NonComp,
                origin92Dev92NonComp, origin92Dev93NonComp,
                origin93Dev93NonComp,
            };
        }

        [Test]
        public void SortsRecordsPerProduct()
        {
            // Act
            var result = _target.SortRecordsByProduct(_records);

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Count, Is.EqualTo(10));
            Assert.That(result[0][0].ProductName, Is.EqualTo("Comp"));

            Assert.That(result[1].Count, Is.EqualTo(10));
            Assert.That(result[1][0].ProductName, Is.EqualTo("Non-Comp"));
        }
    }
}
