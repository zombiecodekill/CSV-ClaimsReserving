using ClaimsReserving.Controllers;
using ClaimsReserving.Models;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace ClaimsReserving.Tests.Controllers
{
    [TestFixture]
    public class FailureControllerTests
    {
        private FailureController _target;

        [SetUp]
        public void Setup()
        {
            _target = new FailureController();
        }

        [Test]
        public void ReturnsViewResultWithDefectsModel()
        {
            // Arrange
            var defects = new Defects();

            // Act
            var result = (ViewResult) _target.Index(defects);

            // Arrange
            Assert.That(result, Is.TypeOf<ViewResult>());
            Assert.That(result.Model, Is.TypeOf<Defects>());
        }
    }
}
