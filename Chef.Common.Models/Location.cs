using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chef.Common.Models;

public class Location : Model
{
    [Required]
    [StringLength(5)]
    public string Code { get; set; }

    [Required]
    [StringLength(128)]
    public string Name { get; set; }

    [Required]
    [ForeignKey("Common.City")]
    public int CityId { get; set; }


    [Required]
    [ForeignKey("Common.State")]
    public int StateId { get; set; }

    [Required]
    [ForeignKey("Common.Country")]
    public int CountryId { get; set; }

    [Required]
    public string CountryName { get; set; }

    [Required]
    public string StateName { get; set; }

    [Required]
    public string CityName { get; set; }

    [Required]
    public string Address { get; set; }

    public string Latitude { get; set; }

    public string Longitude { get; set; }
}
