using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Chef.Common.Core;
using SqlKata.Extensions;

namespace Chef.Common.Models;

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
    [RegularExpression("[0-9]+", ErrorMessage = "Invalid zip code")]
    [StringLength(8, ErrorMessage = "Zip code should be maximum length of 8 characters")]
    public string ZipCode { get; set; }

    [ForeignKey("Common.Currency")]
    public int CurrencyId { get; set; }

    [StringLength(3)]
    public string CurrencyCode { get; set; }

    [Required]
    [StringLength(maximumLength: 18, MinimumLength = 10, ErrorMessage = "Phone number should be minimum of 10 and maximum of 18 characters")]
    [RegularExpression("^([+]?[0-9- ]{1,18})+$", ErrorMessage = "Invalid phone number")]
    public string Phone { get; set; }

    [Required]
    [StringLength(maximumLength: 18, MinimumLength = 10, ErrorMessage = "Fax should be minimum of 10 and maximum of 18 characters")]
    [RegularExpression("^([+]?[0-9- ]{1,18})+$", ErrorMessage = "Invalid fax")]
    public string Fax { get; set; }

    [Required]
    [RegularExpression("^\\S+@\\S+\\.\\S+$", ErrorMessage = "Invalid email")]
    [StringLength(320, ErrorMessage = "Email should be maximum length of 320 characters")]
    public string Email { get; set; }

    public string Website { get; set; }

    public int SerialNumber { get; set; }

    public bool IsActive { get; set; } = true;

    [Unique(true)]
    public int TaxJurisdictionId { get; set; }

    [Write(false)]
    [Skip(true)]
    [SqlKata.Ignore]
    public string TaxJurisdictionName { get; set; }

    public string TaxName { get; set; }

    [Required]
    [RegularExpression("[0-9a-zA-Z]*", ErrorMessage = "Trade license should be alphanumeric only")]
    [StringLength(50, ErrorMessage = "Trade license should be maximum length of 50 characters")]
    public string TradeLicense { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "Tax registration number should be maximum length of 50 characters")]
    public string TaxRegistrationNumber { get; set; }
}