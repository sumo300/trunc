using System.ComponentModel.DataAnnotations;
using Trunc.Data;

namespace Trunc.Models {
    public class UrlItemModel {
        [MinLength(3, ErrorMessageResourceName = "CustomUrlMinLength", ErrorMessageResourceType = typeof(ModelRes.UrlItemModel), ErrorMessage = null)]
        [RegularExpression(@"([a-zA-Z0-9_\-]+)", ErrorMessageResourceName = "CustomUrlRegularExpression", ErrorMessageResourceType = typeof(ModelRes.UrlItemModel), ErrorMessage = null)]
        [Display(Name = "CustomUrl", ResourceType = typeof(ModelRes.UrlItemModel))]
        public string CustomUrl { get; set; }

        [Required(ErrorMessageResourceName = "OriginUrlRequired", ErrorMessageResourceType = typeof(ModelRes.UrlItemModel), ErrorMessage = null)]
        [Url(ErrorMessageResourceName = "OriginUrlUrl", ErrorMessageResourceType = typeof(ModelRes.UrlItemModel), ErrorMessage = null)]
        [Display(Name = "OriginUrl", ResourceType = typeof(ModelRes.UrlItemModel))]
        public string OriginUrl { get; set; }

        [Range(1, int.MaxValue, ErrorMessageResourceName = "ExpireInDaysRange", ErrorMessageResourceType = typeof(ModelRes.UrlItemModel), ErrorMessage = null)]
        [Display(Name = "ExpireInDays", ResourceType = typeof(ModelRes.UrlItemModel))]
        public int ExpireInDays { get; set; }

        [Display(Name = "ExpireMode", ResourceType = typeof(ModelRes.UrlItemModel))]
        public ExpireMode ExpireMode { get; set; }
    }
}