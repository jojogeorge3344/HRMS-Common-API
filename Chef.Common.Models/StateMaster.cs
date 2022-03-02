using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chef.Common.Models
{
    public class StateMaster : Model
    {
        [Required]
        [StringLength(126)]
        public string Name { get; set; }

        [Required]
        [ForeignKey("Common.CountryMaster")]
        public int CountryId { get; set; }
    }
}
