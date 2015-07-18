using System.ComponentModel.DataAnnotations;

namespace Trunc.Data {
    public enum ExpireMode {
        [Display(ResourceType = typeof (ModelRes.ExpireMode), Name = "Never")]
        Never = 0,

        [Display(ResourceType = typeof (ModelRes.ExpireMode), Name = "ByCreated")]
        ByCreated = 1,

        [Display(ResourceType = typeof (ModelRes.ExpireMode), Name = "ByLastAccessed")]
        ByLastAccessed = 2,
    }
}