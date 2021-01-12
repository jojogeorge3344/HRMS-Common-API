using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chef.Common.Models
{
    public class BusinessSitesDetail : Model
    {
        [Required]
        [ForeignKey("BusinessSites")]
        public int BusinessSitesId { get; set; }

        [Required]
        public int BusinessPartnerId { get; set; }

        [Required]
        [StringLength(16)]
        public string BusinessPartnerCode { get; set; }

        [Required]
        [StringLength(126)]
        public string BusinessPartnerName { get; set; }
    }
}
