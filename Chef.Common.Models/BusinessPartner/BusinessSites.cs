using Chef.Common.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Models
{
    public class BusinessSites : Model
    {
        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        [Write(false)]
        [Skip(true)]
        public List<BusinessSitesDetail> BusinessSitesDetails { get; set; }
    }
}
