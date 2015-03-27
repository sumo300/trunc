using System;
using System.Web.Mvc;
using AutoMapper;
using Trunc.Data;
using Trunc.Models;

namespace Trunc.Controllers {
    [HandleError]
    public class HomeController : Controller {
        private readonly IRepository<UrlItem> _repo;

        public HomeController(IRepository<UrlItem> repo) {
            _repo = repo;
        }

        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UrlItemViewModel model) {
            if (string.IsNullOrWhiteSpace(model.ShortenUrl)) {
                model.ShortenUrl = UrlGenerator.GetRandomUrl(6);
            }

            var item = _repo.GetById(model.ShortenUrl);

            if (item != null) {
                return View("Exists", model);
            }

            item = Mapper.Map<UrlItem>(model);

            _repo.Add(item);

            return View("Success", model);
        }

        [Route("{shortenUrl}")]
        public ActionResult Index(string shortenUrl) {
            var item = _repo.GetById(shortenUrl);

            if (item == null) {
                return View("NotFound");
            }

            if (IsExpired(item)) {
                _repo.Delete(item);
                return View("Expired");
            }

            item.TouchedOn = DateTime.Now;
            _repo.Update(item);

            return Redirect(item.OriginUrl);
        }

        private static bool IsExpired(UrlItem item) {
            DateTime expiry = DateTime.Now.AddDays(1);

            switch (item.ExpireMode) {
                case ExpireMode.ByCreated:
                    expiry = item.CreatedOn.AddDays(item.ExpireInDays);
                    break;
                case ExpireMode.ByLastAccessed:
                    expiry = item.TouchedOn.AddDays(item.ExpireInDays);
                    break;
            }
            return DateTime.Now > expiry;
        }

        public ActionResult Browse() {
            throw new NotImplementedException();
        }
    }
}
