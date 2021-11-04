using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Chef.Common.Core;

namespace Chef.Common.Models
{
    public class BusinessSitesDetail : Model
    {
        [Required]
        [ForeignKey("BusinessSites")]
        public int BusinessSitesId { get; set; }

        [Required]
        [StringLength(3)]
        public string SiteCode { get; set; }

        [Required]
        [StringLength(126)]
        public string SiteName { get; set; }
    }
}
