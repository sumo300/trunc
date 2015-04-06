using System;
using Trunc.Data;

namespace Trunc.Models {
    public class UrlItemViewModel : UrlItemModel {
        public DateTime CreatedOn { get; set; }

        public DateTime TouchedOn { get; set; }

        public string FullShortUrl {
            get
            {
                return ShortenUrl.ToAbsoluteUrl();
            }
        }

        public string OriginUrlForDisplay
        {
            get
            {
                var url = OriginUrl;

                if (url.Length > 30)
                {
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
                        return TouchedOn.AddDays(ExpireInDays).ToString("G");
                    default:
                        return "N/A";
                }
            }
        }
    }
}
