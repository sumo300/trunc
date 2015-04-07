using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Trunc.Data;
using Trunc.Models;

namespace Trunc.Controllers {
    public class HomeController : Controller {
        private readonly IRepository<UrlItem> _repo;

        public HomeController(IRepository<UrlItem> repo) {
            _repo = repo;
        }

        public ActionResult Index() {
            // Defaults
            var model = new UrlItemModel {
                ExpireInDays = 1,
                ExpireMode = ExpireMode.Never
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UrlItemModel model) {
            if (!ModelState.IsValid) {
                return View(model);
            }

            if (string.IsNullOrWhiteSpace(model.ShortenUrl)) {
                model.ShortenUrl = UrlGenerator.GetRandomUrl(6);
            }

            UrlItem item = _repo.GetById(model.ShortenUrl);

            if (item != null) {
                return View("Exists", Mapper.Map<UrlItemViewModel>(item));
            }

            item = Mapper.Map<UrlItem>(model);

            try {
                _repo.Add(item);
            } catch (Exception e) {
                Debug.WriteLine(e);
                return View("Exists", Mapper.Map<UrlItemViewModel>(item));
            }

            return View("Success", Mapper.Map<UrlItemViewModel>(item));
        }

        [Route("{shortenUrl}")]
        public ActionResult Index(string shortenUrl) {
            UrlItem item = _repo.GetById(shortenUrl);

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

        private bool IsExpired(UrlItem item) {
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
            IEnumerable<UrlItem> items = _repo.All();
            var model = new BrowseViewModel {
                Items = Mapper.Map<IEnumerable<UrlItemViewModel>>(items).OrderBy(i => i.ExpiryDate)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Browse(string filter) {
            if (!ModelState.IsValid) {
                return View();
            }

            var model = new BrowseViewModel();
            IEnumerable<UrlItem> items = _repo.All().Where(i=>i.OriginUrl.Contains(filter) || i.ShortenUrl.Contains(filter));
            
            if (items.Any()) {
                model.Items = Mapper.Map<IEnumerable<UrlItemViewModel>>(items).OrderBy(i => i.ExpiryDate);
            }

            TempData["filter"] = filter;

            return View(model);
        }
    }
}
