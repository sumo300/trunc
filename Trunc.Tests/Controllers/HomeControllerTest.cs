using System;
using System.Web.Mvc;
using NUnit.Framework;
using Trunc.Controllers;
using Trunc.Data;

namespace Trunc.Tests.Controllers {
    [TestFixture]
    public class HomeControllerTest {
        private IRepository<UrlItem> _repository;

        [TestFixtureSetUp]
        public void SetUp() {
            _repository = new TruncRepository(AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
        }

        [Test]
        public void Index() {
            // Arrange
            var controller = new HomeController(_repository);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
