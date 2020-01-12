using ClaimsReserving.Models;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using ClaimsReserving.Services.Writer;
using CsvHelper;

namespace ClaimsReserving.IntegrationTests
{
    [TestFixture]
    public class CsvOutputTests
    {
        /*  Running these integration tests will actually output the CSV files
            They are not the complete files as the summary record is a dummy here */
        private CsvOutput _target;
        private IConfiguration _configuration;
        private IConfigurationSection _configurationSection;
        private ISummaryRecord _summaryRecord;

        [SetUp]
        public void Setup()
        {
            _configuration = Substitute.For<IConfiguration>();
            _configurationSection = Substitute.For<IConfigurationSection>();
            _configurationSection.Value = "D:\\WTWDIP\\ClaimsReserving\\ClaimsReserving\\OutputFiles";
            _configuration.GetSection(Arg.Any<string>())
                .Returns(_configurationSection);
            _summaryRecord = Substitute.For<ISummaryRecord>();
            _target = new CsvOutput(_configuration, _summaryRecord);
        }

        [Test]
        public void DoesNotWriteToFileWhenNoRecords()
        {
            // Arrange
            var recordsPerProduct = new List<List<YearlyData>>();

            // Act
            _target.Write(recordsPerProduct, "IntegrationTest-NoRecords.csv");

            // Assert
            _summaryRecord.DidNotReceive().Write(Arg.Any<CsvWriter>(), Arg.Any<List<YearlyData>>());
        }

        [Test]
        public void WritesToFileWhenOneRecord()
        {
            // Arrange
            var records = new List<YearlyData>()
            {
                new YearlyData()
                    {OriginYear = 2000, DevelopmentYear = 2000, ProductName = "Comp", IncrementalValue = 100.0m}
            };
            var recordsPerProduct = new List<List<YearlyData>> {records};

            // Act
            _target.Write(recordsPerProduct, "IntegrationTest-OneRecord.csv");

            // Assert
            _summaryRecord.Received().Write(Arg.Any<CsvWriter>(), Arg.Any<List<YearlyData>>());
        }
    }
}
