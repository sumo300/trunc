using System;
using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;
using Trunc.App_Start;
using Trunc.Controllers;
using Trunc.Data;
using Trunc.Models;

namespace Trunc.Tests.Controllers {
    [TestFixture]
    public class HomeControllerTest {
        private IRepository<UrlItem> _repository;
        private const string InvalidFilter = "xxxxxxxxxxxxxxxx";

        [TestFixtureSetUp]
        public void SetUp() {
            TypeMappingConfig.RegisterTypeMaps();
            _repository = new TruncRepository(AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
        }

        [Test]
        public void Index() {
            // Arrange
            var controller = new HomeController(_repository);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Browse() {
            // Arrange
            var controller = new HomeController(_repository);

            // Act
            var result = controller.Browse() as ViewResult;

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Browse_InvalidFilter_ReturnsEmptyItemList() {
            // Arrange
            var controller = new HomeController(_repository);

            // Act
            var result = controller.Browse(InvalidFilter) as ViewResult;
            var model = result.Model as BrowseViewModel;

            // Assert
            Assert.That(model.Items.Count(), Is.EqualTo(0));
        }
    }
}
