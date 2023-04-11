using Chef.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Chef.Common.Dtos.ResponseDto.BusinessPartner
{
    internal class BusinessPartnerImportDto
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

        [StringLength(126)]
        [Required]
        public string CityName { get; set; }

        [Required]
        public string StateName { get; set; }

        [Required]
        public string CountryName { get; set; }

        [Required]
        public string ZipCode { get; set; }

        [StringLength(3)]
        public string CurrencyCode { get; set; }

        [Required]
        public string Phone { get; set; }

        public string Fax { get; set; }

        [Required]
        public string Email { get; set; }

        public string Website { get; set; }
        
        public int TaxJurisdictionName { get; set; }

        public string TaxName { get; set; }

        public string TradeLicense { get; set; }

        public string TaxRegistrationNumber { get; set; }
    }
}

