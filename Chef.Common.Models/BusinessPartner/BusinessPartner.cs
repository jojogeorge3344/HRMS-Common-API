using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Chef.Common.Core;

namespace Chef.Common.Models
{
    public class BusinessPartner : Model
    {
        [StringLength(126)]
        [Required]
        public string Name { get; set; }

        [Required]
        public string BpType { get; set; }

        [StringLength(16)]
        public string Code { get; set; }

        [StringLength(256)]
        public string AddressLine1 { get; set; }

        [StringLength(256)]
        public string AddressLine2 { get; set; }

        [Required]
        public int CityId { get; set; }

        [StringLength(126)]
        [Required]
        public string CityName { get; set; }

        [ForeignKey("Common.State")]
        [Required]
        public int StateId { get; set; }

        [Required]
        public string StateName { get; set; }

        [ForeignKey("Common.Country")]
        [Required]
        public int CountryId { get; set; }

        [Required]
        public string CountryName { get; set; }

        [Required]
        public string ZipCode { get; set; }

        [ForeignKey("Common.Currency")]
        public int CurrencyId { get; set; }

        [StringLength(3)]
        public string CurrencyCode { get; set; }

        [Required]
        public string Phone { get; set; }

        public string Fax { get; set; }

        [Required]
        public string Email { get; set; }

        public string Website { get; set; }

        public int SerialNumber { get; set; }

        public bool IsActive { get; set; } = false;

        [Required]
        [Unique(true)]
        public int TaxJurisdictionId { get; set; }

        public string TaxName { get; set; }

        public string TradeLicense { get; set; }

        public string TaxRegistrationNumber { get; set; }
    }
}