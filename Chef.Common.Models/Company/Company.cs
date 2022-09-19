using System.ComponentModel.DataAnnotations;
using Chef.Common.Core;

namespace Chef.Common.Models
{
    public class Company : Model
    {
        [Required]
        public string Name { get; set; }

        [Required]
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
        public string ZipCode { get; set; }
        [Required]
        public int CurrencyId { get; set; }

        [Required]
        public string CurrencyCode { get; set; }

        [Required]
        public string Phone { get; set; }

        public string Fax { get; set; }

        [Required]
        public string Email { get; set; }

        public string Website { get; set; }

        public string TradeLicense { get; set; }

        public int BaseCompanyId { get; set; }

        public string BaseCompanyCode { get; set; }

        public string BaseCompanyName { get; set; }

        
        [Required]
        public string TaxId { get; set; }

        
        [Required]
        public string TaxRegistrationNumber { get; set; }

        
        //[Required]
        [Unique(true)]
        public string Host { get; set; }
        public byte[] logo { get; set; }
    }
}