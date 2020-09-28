using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Chef.Common.Core;

namespace Chef.Common.Models
{
    public class City : Model
    {
        [Required]
        [StringLength(126)]
        public string Name { get; set; }

        [Required]
        [ForeignKey("Common.State")]
        public int StateId { get; set; }

        [Required]
        public string StateName { get; set; }

        [Required]
        [ForeignKey("Common.Country")]
        public int CountryId { get; set; }

        [Required]
        public string CountryName { get; set; }
    }
}