using ClaimsReserving.Controllers;
using ClaimsReserving.Models;
using ClaimsReserving.Services;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using ClaimsReserving.Services.Validation;
using ClaimsReserving.Services.Writer;
using Microsoft.AspNetCore.Mvc;

namespace ClaimsReserving.Tests.Controllers
{
    [TestFixture]
    public class ClaimsControllerTests
    {
        private ClaimsController _target;
        private ICsvLoader _csvLoader;
        private ICsvOutput _csvOutput;
        private IDirectoryFinder _directoryFinder;
        private IConfiguration _configuration;
        private IFileValidator _fileValidator;
        private IRecordsPerProduct _recordsPerProduct;

        [SetUp]
        public void Setup()
        {
            _csvLoader = Substitute.For<ICsvLoader>();
            _csvOutput = Substitute.For<ICsvOutput>();
            _directoryFinder = Substitute.For<IDirectoryFinder>();
            _configuration = Substitute.For<IConfiguration>();
            _configuration.GetValue<string>("InputFilesDirectory")
                .Returns("D:\\WTWDIP\\ClaimsReserving\\ClaimsReserving\\InputFiles");
            _fileValidator = Substitute.For<IFileValidator>();
            _recordsPerProduct = Substitute.For<IRecordsPerProduct>();
            _target = new ClaimsController(_csvLoader, _directoryFinder, _configuration, _csvOutput, _fileValidator, _recordsPerProduct);
        }

        [Test]
        public void CallsFindFilesMessage()
        {
            // Act
            _target.Index();

            // Assert
            _csvLoader.Received().FindFilesMessage();
        }


        [Test]
        public void DoesNotGetFilesWhenDirectoryNotFound()
        {
            // Arrange
            _directoryFinder.Exists(Arg.Any<string>()).Returns(false);

            // Act
            _target.Index();

            // Assert
            _csvLoader.DidNotReceive().GetFiles();
        }

        [Test]
        public void GetFilesWhenDirectoryFound()
        {
            // Arrange
            _directoryFinder.Exists(Arg.Any<string>()).Returns(true);

            // Act
            _target.Index();

            // Assert
            _csvLoader.Received().GetFiles();
        }

        [Test]
        public void LoadsSelectedFile()
        {
            // Act
            _target.ProcessFile(1);

            // Assert
            _csvLoader.Received().LoadCsvFile(Arg.Any<string>());
        }

        [Test]
        public void ValidatesTheLoadedFileContents()
        {
            // Act
            _target.ProcessFile(1);

            // Assert
            _fileValidator.Received().Validate(Arg.Any<FileModel>(), Arg.Any<List<YearlyData>>());
        }

        [Test]
        public void WritesOutputCsvFileIfNoErrorsFoundAndRecordsIsNotNull()
        {
            // Arrange
            _fileValidator.Validate(Arg.Any<FileModel>(), Arg.Any<List<YearlyData>>()).Returns(new Defects());
            _csvLoader.LoadCsvFile(Arg.Any<string>()).Returns(new List<YearlyData>() {new YearlyData() 
                { DevelopmentYear = 1990, OriginYear = 1990, ProductName = "Comp"}});
            _recordsPerProduct.SortRecordsByProduct(Arg.Any<List<YearlyData>>())
                .Returns(new List<List<YearlyData>>()
            {
                new List<YearlyData>
                {
                    new YearlyData { DevelopmentYear = 1900, OriginYear = 1990, IncrementalValue = 0, ProductName = "Comp"}
                }
            });


            // Act
            _target.ProcessFile(1);

            // Assert
            _csvOutput.Received().Write(Arg.Any<List<List<YearlyData>>>(), Arg.Any<string>());
        }

        [Test]
        public void RedirectsToDefectNotificationScreenIfAnyErrorsFound()
        {
            // Arrange
            _fileValidator.Validate(Arg.Any<FileModel>(), Arg.Any<List<YearlyData>>()).Returns(new Defects
                {
                    Errors = new List<string> { "The Origin Year 1066 is too old" }
                }
            );

            // Act
            var result = (RedirectToActionResult) _target.ProcessFile(1);

            // Assert
            Assert.That(result, Is.TypeOf<RedirectToActionResult>());
            Assert.That(result.ControllerName, Is.EqualTo("Failure"));
            Assert.That(result.ActionName, Is.EqualTo("Index"));
        }
    }
}