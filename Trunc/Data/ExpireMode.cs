using System.ComponentModel.DataAnnotations;

namespace Trunc.Data {
    public enum ExpireMode {
        [Display(Name = "Never")]
        Never = 0,

        [Display(Name = "By Created")]
        ByCreated = 1,

        [Display(Name = "By Last Accessed")]
        ByLastAccessed = 2,
    }
}