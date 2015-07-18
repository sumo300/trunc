using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Mvc;
using System.Web.UI;
using AutoMapper;
using Trunc.Data;
using Trunc.Models;

namespace Trunc.Controllers {
    public class HomeController : Controller {
        private readonly IRepository<UrlItem> _urlRepo;
        private readonly IRepository<UrlHit> _hitRepo;

        public HomeController(IRepository<UrlItem> urlRepo, IRepository<UrlHit> hitRepo) {
            _urlRepo = urlRepo;
            _hitRepo = hitRepo;
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
                item = _urlRepo.All().FirstOrDefault(i => i.CustomUrl == model.CustomUrl);

                if (item != null) {
                    return View("Exists", Mapper.Map<UrlItemViewModel>(item));
                }
            }

            item = Mapper.Map<UrlItem>(model);

            try {
                _urlRepo.Add(item);
            } catch (Exception e) {
                Debug.WriteLine(e);
                return View("Exists", Mapper.Map<UrlItemViewModel>(item));
            }

            return View("Success", Mapper.Map<UrlItemViewModel>(item));
        }

        [Route("{shortenUrl}")]
        public ActionResult Index(string shortenUrl) {
            UrlItem item = _urlRepo.GetById(UrlGenerator.Decode(shortenUrl)) ?? _urlRepo.All().FirstOrDefault(i=>i.CustomUrl.Equals(shortenUrl));

            if (item == null) {
                return View("NotFound");
            }

            if (IsExpired(item)) {
                _urlRepo.Delete(item);
                return View("Expired");
            }

            var urlHit = new UrlHit {
                UrlItemId = item.Id,
                ClientIp = Request.UserHostAddress
            };
            
            _urlRepo.Update(item);
            _hitRepo.Add(urlHit);
            
            return Redirect(item.OriginUrl);
        }

        private bool IsExpired(UrlItem item) {
            DateTime expiry = DateTime.Now.AddDays(1);
            
            switch ((ExpireMode)item.ExpireMode) {
                case ExpireMode.ByCreated:
                    expiry = item.CreatedOn.AddDays(item.ExpireInDays);
                    break;
                case ExpireMode.ByLastAccessed:
                    var lastTouched = _hitRepo.All().Where(h => h.UrlItemId == item.Id).Max(h => h.HitOn);
                    expiry = lastTouched.AddDays(item.ExpireInDays);
                    break;
            }
            return DateTime.Now > expiry;
        }

        public ActionResult Browse() {
            IEnumerable<UrlItem> items = _urlRepo.All().OrderByDescending(i => i.CreatedOn);
            var itemsWithLastHit = GetUrlItemsForViewModel(items);

            var model = new BrowseViewModel {
                Items = itemsWithLastHit.Take(100),
                TableCaption = ViewRes.Browse.TableCaptionNewest100
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
                TableCaption = string.Format(ViewRes.Browse.TableCaptionNewest100Filtered, filter)
            };

            IEnumerable<UrlItem> items = _urlRepo.All()
                .OrderByDescending(i => i.CreatedOn)
                .Where(item => item.OriginUrl.Contains(filter) || item.CustomUrl.Contains(filter));

            var itemsWithLastHit = GetUrlItemsForViewModel(items);

            model.Items = itemsWithLastHit.Take(100);

            TempData["filter"] = filter;

            return View(model);
        }

        private IEnumerable<UrlItemViewModel> GetUrlItemsForViewModel(IEnumerable<UrlItem> items) {
            IEnumerable<UrlHit> hits = _hitRepo.All();

            var hitCount = hits
                .GroupBy(x => x.UrlItemId)
                .Select(grouping => new HitModel {
                    UrlItemId = grouping.Key,
                    HitCount = grouping.Count()
                });

            var maxHit = hits
                    .GroupBy(x => x.UrlItemId)
                    .Select(grouping => new LatestHitModel {
                        UrlItemId = grouping.Key,
                        HitOn = grouping.Max(x => x.HitOn)
                    });

            return items.GroupJoin(hitCount, item => item.Id, hc => hc.UrlItemId, (x, y) => {
                HitModel hit = y.FirstOrDefault();
                return new UrlItemViewModel {
                    CreatedOn = x.CreatedOn,
                    CustomUrl = x.CustomUrl,
                    ExpireInDays = (int) x.ExpireInDays,
                    ExpireMode = (ExpireMode) x.ExpireMode,
                    HitCount = hit == null ? 0 : hit.HitCount,
                    Id = x.Id,
                    OriginUrl = x.OriginUrl
                };
            })
            .GroupJoin(maxHit, item => item.Id, mh => mh.UrlItemId, (x, y) => {
                LatestHitModel latest = y.FirstOrDefault();
                return new UrlItemViewModel {
                    CreatedOn = x.CreatedOn,
                    CustomUrl = x.CustomUrl,
                    ExpireInDays = x.ExpireInDays,
                    ExpireMode = x.ExpireMode,
                    HitCount = x.HitCount,
                    Id = x.Id,
                    OriginUrl = x.OriginUrl,
                    TouchedOn = latest == null ? (DateTime?)null : latest.HitOn
                };
            });
        }
    }
}
