using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;

namespace Chef.Trading.Models;

public class Address : Model
{
    [Required]
    [Unique(true)]
    [Code]
    public string AddressCode { get; set; }

    public string AddressName { get; set; }

    [Required]
    public string FaxNo { get; set; }
    [Required]

    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }

    [Required]
    public string CityName { get; set; }

    [Required]
    public string StateName { get; set; }

    [Required]
    public string CountryName { get; set; }

    [Required]
    public int CityId { get; set; }

    [Required]
    public int StateId { get; set; }

    [Required]
    public int CountryId { get; set; }

    [Required]
    public string ZipCode { get; set; }
    public string ContactPerson { get; set; }
    public string ContactNumber { get; set; }
    public string Email { get; set; }
}