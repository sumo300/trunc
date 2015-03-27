using System;
using System.ComponentModel.DataAnnotations;
using Biggy.Core;

namespace Trunc.Data {
    public class UrlItem {
        public UrlItem() {
            CreatedOn = DateTime.Now;
            TouchedOn = DateTime.Now;
        }

        [PrimaryKey(false)]
        public string ShortenUrl { get; set; }

        public string OriginUrl { get; set; }

        public double ExpireInDays { get; set; }

        public ExpireMode ExpireMode { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime TouchedOn { get; set; }
    }

    public enum ExpireMode {
        [Display(Name = "By Created")]
        ByCreated = 0,

        [Display(Name = "By Last Accessed")]
        ByLastAccessed = 1,

        [Display(Name = "Never")]
        Never = 2
    }
}
