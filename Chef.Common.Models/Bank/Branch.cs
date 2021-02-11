using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chef.Common.Models
{
    public class Branch : Model
    {
        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [Required]
        [StringLength(8)]
        public string Code { get; set; }     

        [StringLength(256)]
        public string AddressLine1 { get; set; }

        [StringLength(256)]
        public string AddressLine2 { get; set; }

        public int CityId { get; set; }

        [StringLength(256)]
        public string CityName { get; set; }

        public int StateId { get; set; }

        [Required]
        [StringLength(256)]
        public string StateName { get; set; }

        public int CountryId { get; set; }

        [StringLength(126)]
        public string CountryName { get; set; }

        [StringLength(8)]
        public string ZipCode { get; set; }

        [StringLength(16)]
        public string Phone { get; set; }

        [StringLength(32)]
        public string Fax { get; set; }

        [StringLength(32)]
        public string Email { get; set; }

        [ForeignKey("Common.Company")]
        public int CompanyId { get; set; }

        [StringLength(8)]
        public string CompanyCode { get; set; }

        [StringLength(64)]
        public string CompanyName { get; set; }
    }
}
