using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trunc.Data;

namespace Trunc.Tests.Data {
    [TestClass]
    public class TruncRepositoryTests {
        private TruncRepository _repo;

        [TestInitialize]
        public void Init() {
            _repo = new TruncRepository(AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
        }

        [TestMethod]
        public void Add() {
            var item = new UrlItem {
                ExpireMode = ExpireMode.Never,
                OriginUrl = "http://www.google.com/"
            };

            _repo.Add(item);

            UrlItem storedItem = _repo.GetById(UrlGenerator.Decode(item.CustomUrl));

            Assert.IsNotNull(storedItem);
        }

        [TestMethod]
        public void Delete() {
            var item = new UrlItem {
                ExpireMode = ExpireMode.Never,
                OriginUrl = "http://www.google.com/",
                CustomUrl = "flarg"
            };

            _repo.Add(item);
            _repo.Delete(item);

            Assert.IsFalse(_repo.Exists(UrlGenerator.Decode(item.CustomUrl)));
        }
    }
}
