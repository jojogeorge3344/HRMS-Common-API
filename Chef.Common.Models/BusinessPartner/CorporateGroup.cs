using System.ComponentModel.DataAnnotations;
using Chef.Common.Core;

namespace Chef.Common.Models
{
    public class CorporateGroup : Model
    {
        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        [Required]
        [StringLength(120)]
        public string Name { get; set; }

        [Required]
        public int HeadOfficeId { get; set; }

        [Required]
        [StringLength(16)]
        public string HeadOfficeCode { get; set; }

        [Required]
        [StringLength(126)]
        public string HeadOfficeName { get; set; }
    }
}
