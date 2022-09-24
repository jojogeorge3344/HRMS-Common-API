using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Chef.Common.Core;

namespace Chef.Common.Models
{
    public class Branch : Model
    {
        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [Required]
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

        public string ZipCode { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        [ForeignKey("Common.Company")]
        public int CompanyId { get; set; }

        public string CompanyCode { get; set; }

        public string CompanyName { get; set; }
    }
}