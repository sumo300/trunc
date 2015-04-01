using System.ComponentModel.DataAnnotations;
using Trunc.Data;

namespace Trunc.Models {
    public class UrlItemModel {
        [MinLength(3, ErrorMessage = "Must be 3 or more characters")]
        [Display(Name = "Shorten URL")]
        public string ShortenUrl { get; set; }

        [Required]
        [Url]
        [Display(Name = "Origin URL")]
        public string OriginUrl { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Expire in Days")]
        public int ExpireInDays { get; set; }

        [Display(Name = "Expire Mode")]
        public ExpireMode ExpireMode { get; set; }
    }
}