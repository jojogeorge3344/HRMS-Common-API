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

        [StringLength(16)]
        [Required]
        public string BpType { get; set; }

        [StringLength(16)]
        [Required]
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

        [StringLength(126)]
        [Required]
        public string StateName { get; set; }

        [ForeignKey("Common.Country")]
        [Required]
        public int CountryId { get; set; }

        [StringLength(126)]
        [Required]
        public string CountryName { get; set; }

        [StringLength(8)]
        [Required]
        public string ZipCode { get; set; }

        [ForeignKey("Common.Currency")]
        public int CurrencyId { get; set; }

        [StringLength(3)]
        public string CurrencyCode { get; set; }

        [StringLength(12)]
        [Required]
        public string Phone { get; set; }

        [StringLength(16)]
        public string Fax { get; set; }

        [StringLength(32)]
        [Required]
        public string Email { get; set; }

        [StringLength(62)]
        public string Website { get; set; }
        public int SerialNumber { get; set; }

        public bool IsActive { get; set; }
        [Required]
        [Unique(true)]
        public int TaxJurisdictionId { get; set; }

        public string TaxName { get; set; }
        //public int DefaultPurchaseTax { get; set; }
        //public int DefaultSalesTax { get; set; }
    }
}