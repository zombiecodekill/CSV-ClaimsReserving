using ClaimsReserving.Services;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;

namespace ClaimsReserving.Tests.Services
{
    [TestFixture]
    public class CsvLoaderTests
    {
        private CsvLoader _target;
        private IDirectoryFinder _directoryFinder;
        private IConfiguration _configuration;

        [SetUp]
        public void Setup()
        {
            _directoryFinder = Substitute.For<IDirectoryFinder>();
            _configuration = Substitute.For<IConfiguration>();
            _configuration.GetValue<string>("InputFilesDirectory")
                .Returns("D:\\WTWDIP\\ClaimsReserving\\ClaimsReserving\\InputFiles");
            _target = new CsvLoader(_directoryFinder, _configuration);
        }

        [Test]
        public void SetsFindFilesMessageWhenDirectoryNotFound()
        {
            // Act
            var result = _target.FindFilesMessage();

            // Assert
            Assert.That(result, Is.EqualTo("Could not find InputFiles directory"));
        }
    }
}