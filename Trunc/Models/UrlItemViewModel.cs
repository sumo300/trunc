using System;
using System.ComponentModel.DataAnnotations;
using Trunc.Data;

namespace Trunc.Models {
    public class UrlItemViewModel : UrlItemModel {
        public long Id { get; set; }

        [Display(Name = "CreatedOn", ResourceType = typeof(ModelRes.UrlItemViewModel))]
        public DateTime CreatedOn { get; set; }

        public string CreatedOnFormatted { get { return CreatedOn.ToString("G"); } }

        [Display(Name = "TouchedOn", ResourceType = typeof(ModelRes.UrlItemViewModel))]
        public DateTime? TouchedOn { get; set; }

        public string TouchedOnFormatted {
            get { return TouchedOn.HasValue ? TouchedOn.Value.ToString("G") : ModelRes.ExpireMode.Never; }
        }

        public string ShortUrl {
            get { return string.IsNullOrWhiteSpace(CustomUrl) ? UrlGenerator.Encode(Id) : CustomUrl; }
        }

        public string FullShortUrl {
            get { return ShortUrl.ToAbsoluteUrl(); }
        }

        public string OriginUrlForDisplay {
            get {
                string url = OriginUrl;

                if (url.Length > 30) {
                    return url.Substring(0, 30) + "...";
                }

                return url;
            }
        }

        [Display(Name = "ExpiryDate", ResourceType = typeof(ModelRes.UrlItemViewModel))]
        public string ExpiryDate {
            get {
                switch (ExpireMode) {
                    case ExpireMode.ByCreated:
                        return CreatedOn.AddDays(ExpireInDays).ToString("G");
                    case ExpireMode.ByLastAccessed:
                        return TouchedOn.HasValue ? TouchedOn.Value.AddDays(ExpireInDays).ToString("G") : ModelRes.ExpireMode.Never;
                    default:
                        return "N/A";
                }
            }
        }

        [Display(Name = "HitCount", ResourceType = typeof(ModelRes.UrlItemViewModel))]
        public int HitCount { get; set; }
        public int UrlItemId { get; set; }
    }
}