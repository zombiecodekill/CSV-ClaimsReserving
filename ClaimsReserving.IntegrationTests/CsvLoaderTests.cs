using System.Linq;
using ClaimsReserving.Services;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NSubstitute;

namespace ClaimsReserving.IntegrationTests
{
    /* Running these integration tests will read the CSV files from the path set below */
    public class CsvLoaderTests
    {
        private CsvLoader _target;
        private IDirectoryFinder _directoryFinder;
        private IConfiguration _configuration;
        private string _path;

        [SetUp]
        public void Setup()
        {
            _directoryFinder = new DirectoryFinder();
            _configuration = Substitute.For<IConfiguration>();
            _target = new CsvLoader(_directoryFinder, _configuration);
            _path = "D:\\WTWDIP\\ClaimsReserving\\ClaimsReserving\\InputFiles\\";
        }

        [Test]
        public void LoadsPristineStandardExampleCsvFile()
        {
            // Act
            var result = _target.LoadCsvFile(_path + "PristineStandardExample.csv");

            // Assert
            Assert.That(result.Count, Is.EqualTo(12));
        }

        [Test]
        public void LoadsPristineCompleteExampleCsvFile()
        {
            // Act
            var result = _target.LoadCsvFile(_path + "PristineCompleteExample.csv");

            // Assert
            Assert.That(result.Count, Is.EqualTo(20));
        }

        [Test]
        public void LoadsSingleSpacedCsvFile()
        {
            // Act
            var result = _target.LoadCsvFile(_path + "SingleSpaced.csv");

            // Assert
            Assert.That(result.Count, Is.EqualTo(12));
        }

        [Test]
        public void LoadsMissingOriginYear1994CsvFile()
        {
            // Act
            var result = _target.LoadCsvFile(_path + "MissingOriginYear1994.csv");

            // Assert
            Assert.That(result.Count, Is.EqualTo(13));
        }

        [Test]
        public void LoadsDevelopmentYearBeforeOriginYearCsvFile()
        {
            // Act
            var result = _target.LoadCsvFile(_path + "DevelopmentYearBeforeOriginYear.csv");

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void LoadsMissingIncrementalValuesCsvFile()
        {
            // Act
            var result = _target.LoadCsvFile(_path + "MissingIncrementalValues.csv").ToList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(13));
            Assert.That(result[3].IncrementalValue, Is.Null);
            Assert.That(result[12].IncrementalValue, Is.Null);
        }

        [Test]
        public void LoadsOriginYearTooOldCsvFile()
        {
            // Act
            var result = _target.LoadCsvFile(_path + "OriginYearTooOld.csv").ToList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void LoadsOriginYearAndDevelopmentYearTooOldCsvFile()
        {
            // Act
            var result = _target.LoadCsvFile(_path + "OriginYearAndDevelopmentYearTooOld.csv");

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
        }
    }
}