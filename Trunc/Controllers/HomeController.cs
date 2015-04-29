using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
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

            UrlItem item;

            if (!string.IsNullOrWhiteSpace(model.CustomUrl)) {
                item = _repo.All().FirstOrDefault(i => i.CustomUrl == model.CustomUrl);

                if (item != null) {
                    return View("Exists", Mapper.Map<UrlItemViewModel>(item));
                }
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
            UrlItem item = _repo.GetById(UrlGenerator.Decode(shortenUrl)) ?? _repo.All().FirstOrDefault(i=>i.CustomUrl.Equals(shortenUrl));

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
            
            switch ((ExpireMode)item.ExpireMode) {
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
            IEnumerable<UrlItem> items = _repo.All()
                .OrderByDescending(i => i.CreatedOn)
                .Take(100);
            var model = new BrowseViewModel {
                Items = Mapper.Map<IEnumerable<UrlItemViewModel>>(items),
                TableCaption = "Displaying the newest 100 URLs"
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Browse(string filter) {
            if (!ModelState.IsValid) {
                return View();
            }

            var model = new BrowseViewModel {
                TableCaption = string.Format("Displaying the newest 100 URLs filtered by \"{0}\"", filter)
            };

            IEnumerable<UrlItem> items = _repo.All()
                .Where(item => item.OriginUrl.Contains(filter) || item.CustomUrl.Contains(filter))
                .OrderByDescending(i => i.CreatedOn)
                .Take(100);
                                    
            model.Items = Mapper.Map<IEnumerable<UrlItemViewModel>>(items);

            TempData["filter"] = filter;

            return View(model);
        }
    }
}
