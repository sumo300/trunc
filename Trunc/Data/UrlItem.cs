using System;
using System.ComponentModel.DataAnnotations;
using Biggy.Core;

namespace Trunc.Data {
    public class UrlItem {
        private string _customUrl;

        public UrlItem() {
            CreatedOn = DateTime.Now;
            TouchedOn = DateTime.Now;
        }

        [PrimaryKey(true)]
        public int Id { get; set; }

        public string CustomUrl {
            get {
                return string.IsNullOrWhiteSpace(_customUrl) ? UrlGenerator.Encode(Id) : _customUrl;
            }
            set { _customUrl = value; }
        }

        public string OriginUrl { get; set; }

        public double ExpireInDays { get; set; }

        public ExpireMode ExpireMode { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime TouchedOn { get; set; }
    }

    public enum ExpireMode {
        [Display(Name = "Never")]
        Never = 0,

        [Display(Name = "By Created")]
        ByCreated = 1,

        [Display(Name = "By Last Accessed")]
        ByLastAccessed = 2,
    }
}
