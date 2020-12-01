using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Models
{
    public class Company : Model
    {
        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [Required]
        [StringLength(8)]
        [Code]
        public string Code { get; set; }

        [StringLength(256)]
        public string AddressLine1 { get; set; }

        [StringLength(256)]
        public string AddressLine2 { get; set; }

        [Required]
        public int CityId { get; set; }

        [Required]
        [StringLength(126)]
        public string CityName { get; set; }

        [Required]
        public int StateId { get; set; }

        [Required]
        [StringLength(126)]
        public string StateName { get; set; }

        [Required]
        public int CountryId { get; set; }

        [StringLength(126)]
        [Required]
        public string CountryName { get; set; }
        [Required]
        [StringLength(8)]
        public string ZipCode { get; set; }
        [Required]
        public int CurrencyId { get; set; }

        [StringLength(3)]
        [Required]
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

        public string TradeLicense { get; set; }

        public int BaseCompanyId { get; set; }

        [StringLength(8)]
        public string BaseCompanyCode { get; set; }

        [StringLength(32)]
        [Required]
        public string TaxId { get; set; }

        [StringLength(32)]
        [Required]
        public string TaxRegistrationNumber { get; set; }

        [StringLength(64)]
        [Required]
        [Unique(true)]
        public string Host { get; set; }
    }
}