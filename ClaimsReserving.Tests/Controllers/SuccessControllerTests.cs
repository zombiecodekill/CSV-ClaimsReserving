using ClaimsReserving.Controllers;
using ClaimsReserving.Models;
using ClaimsReserving.Services;
using ClaimsReserving.Services.Writer;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;

namespace ClaimsReserving.Tests.Controllers
{
    [TestFixture]
    public class SuccessControllerTests
    {
        private SuccessController _target;
        private ICsvLoader _csvLoader;
        private ICsvOutput _csvOutput;

        [SetUp]
        public void Setup()
        {
            _csvLoader = Substitute.For<ICsvLoader>();
            _csvOutput = Substitute.For<ICsvOutput>();
            _target = new SuccessController(_csvLoader, _csvOutput);
        }

        [Test]
        public void ReturnsViewResultWithSuccessModel()
        {
            // Arrange
            int id = 1;

            // Act
            var result = (ViewResult)_target.Index(id);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void ResultContainsSuccessModel()
        {
            // Arrange
            int id = 1;
            _csvLoader.GetFileById(1).Returns(new FileModel { Id = id, Name = "test.csv" });
            _csvOutput.GetOutputFileName("test.csv").Returns("testOutput.csv");

            // Act
            var result = (ViewResult)_target.Index(id);
            var successModel = (SuccessModel)result.Model;

            // Assert
            Assert.That(successModel.Id, Is.EqualTo(1));
            Assert.That(successModel.Name, Is.EqualTo("test.csv"));
            Assert.That(successModel.OutputFileName, Is.EqualTo("testOutput.csv"));
        }
    }
}
