using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trunc.Controllers;
using Trunc.Data;

namespace Trunc.Tests.Controllers {
    [TestClass]
    public class HomeControllerTest {
        [TestMethod]
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
