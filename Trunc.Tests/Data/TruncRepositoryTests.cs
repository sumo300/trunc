using System;
using System.Linq;
using NUnit.Framework;
using Trunc.App_Start;
using Trunc.Data;

namespace Trunc.Tests.Data {
    [TestFixture]
    public class TruncRepositoryTests {
        private TruncRepository _repo;

        [TestFixtureSetUp]
        public void SetUp() {
            TypeMappingConfig.RegisterTypeMaps();
            _repo = new TruncRepository(AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
        }

        [Test]
        public void Add() {
            var item = new UrlItem {
                ExpireMode = (short) ExpireMode.Never,
                OriginUrl = "http://www.google.com/"
            };

            _repo.Add(item);

            UrlItem storedItem = _repo.GetById(UrlGenerator.Decode(item.CustomUrl));

            Assert.That(storedItem, Is.Not.Null);
        }

        [Test]
        public void Delete() {
            var item = new UrlItem {
                ExpireMode = (short) ExpireMode.Never,
                OriginUrl = "http://www.google.com/",
                CustomUrl = "flarg"
            };

            _repo.Add(item);
            _repo.Delete(item);

            var found = _repo.All().FirstOrDefault(i => i.CustomUrl.Equals(item.CustomUrl));

            Assert.That(found, Is.Null);
        }
    }
}
