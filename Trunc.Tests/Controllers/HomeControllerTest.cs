using System;
using System.Web.Mvc;
using NUnit.Framework;
using Trunc.Controllers;
using Trunc.Data;

namespace Trunc.Tests.Controllers {
    [TestFixture]
    public class HomeControllerTest {
        [Test]
        public void Index() {
            // Arrange
            var controller = new HomeController(new TruncRepository(AppDomain.CurrentDomain.SetupInformation.ApplicationBase));

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
