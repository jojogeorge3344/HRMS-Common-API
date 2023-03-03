using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Models;

public class Prospect : Model
{
    [Required]
    public string ProspectCode { get; set; }

    [Required]
    public string ProspectName { get; set; }

    [Required]
    public string AddressLine1 { get; set; }

    public string AddressLine2 { get; set; }

    [Required]
    public string Currency { get; set; }

    [Required]
    public int BpType { get; set; }

    [Required]
    public int CityId { get; set; }

    [Required]
    public string CityName { get; set; }

    [Required]
    public int StateId { get; set; }

    [Required]
    public string StateName { get; set; }
    [Required]
    public int CountryId { get; set; }
    [Required]
    public string CountryName { get; set; }

    [Required]
    public string ZipCode { get; set; }

    [Required]
    public string Faxno { get; set; }

    [Required]
    public string ContactNumber { get; set; }

    [Required]
    public string ContactPerson { get; set; }


    [Required]
    public string Email { get; set; }

    [Required]
    public bool IsAssigned { get; set; }

    [Required]
    public bool IsActive { get; set; }

    public int BusinessPartnerId { get; set; }
    [Required]
    public int TaxJurisdictionId { get; set; }
    public int TaxNumber { get; set; }
}

