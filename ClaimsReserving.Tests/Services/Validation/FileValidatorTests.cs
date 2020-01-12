using System.Collections.Generic;
using ClaimsReserving.Models;
using ClaimsReserving.Services.Validation;
using NUnit.Framework;

namespace ClaimsReserving.Tests.Services.Validation
{
    [TestFixture]
    public class FileValidatorTests
    {
        private FileValidator _target;

        [SetUp]
        public void Setup()
        {
            _target = new FileValidator();
        }

        [Test]
        public void AddsErrorWhenEmptyData()
        {
            // Arrange
            var file = new FileModel { Id = 1, Name = "file.csv" };
            var records = new List<YearlyData>();

            // Act
            var result = _target.Validate(file, records);

            // Assert
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("No data found in file."));
        }
    }
}
