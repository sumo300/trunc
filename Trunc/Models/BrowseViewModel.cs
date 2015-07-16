using System.Collections.Generic;

namespace Trunc.Models {
    public class BrowseViewModel {
        public IEnumerable<UrlItemViewModel> Items { get; set; }

        public string TableCaption { get; set; }
    }
}