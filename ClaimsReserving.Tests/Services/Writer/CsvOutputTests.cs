using System.Collections.Generic;
using ClaimsReserving.Models;
using ClaimsReserving.Services.Writer;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;

namespace ClaimsReserving.Tests.Services.Writer
{
    [TestFixture]
    public class CsvOutputTests
    {
        private CsvOutput _target;
        private IConfiguration _configuration;
        private ISummaryRecord _summaryRecord;

        [SetUp]
        public void Setup()
        {
            _configuration = Substitute.For<IConfiguration>();
            _summaryRecord = Substitute.For<ISummaryRecord>();
            _configuration.GetValue<string>("OutputFilesDirectory")
                .Returns("D:\\WTWDIP\\ClaimsReserving\\ClaimsReserving\\OutputFiles");
            _target = new CsvOutput(_configuration, _summaryRecord);
        }

        [Test]
        public void WritesFile()
        {
            // Arrange
            var records = new List<YearlyData>
            {
                new YearlyData { DevelopmentYear = 1990, ProductName = "Comp", OriginYear = 1990, IncrementalValue = 100m }
            };
            var recordsPerProduct = new List<List<YearlyData>> { records };

            // Act
            _target.Write(recordsPerProduct, "inputFile.csv");
        }
    }
}
