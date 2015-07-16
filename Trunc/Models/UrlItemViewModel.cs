using System;
using Trunc.Data;

namespace Trunc.Models {
    public class UrlItemViewModel : UrlItemModel {
        public long Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedOnFormatted { get { return CreatedOn.ToString("G"); } }

        public DateTime? TouchedOn { get; set; }

        public string TouchedOnFormatted {
            get { return TouchedOn.HasValue ? TouchedOn.Value.ToString("G") : "Never"; }
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

        public string ExpiryDate {
            get {
                switch (ExpireMode) {
                    case ExpireMode.ByCreated:
                        return CreatedOn.AddDays(ExpireInDays).ToString("G");
                    case ExpireMode.ByLastAccessed:
                        return TouchedOn.HasValue ? TouchedOn.Value.AddDays(ExpireInDays).ToString("G") : "Never";
                    default:
                        return "N/A";
                }
            }
        }

        public int HitCount { get; set; }
        public int UrlItemId { get; set; }
    }
}