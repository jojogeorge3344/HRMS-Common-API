using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chef.Common.Models
{
    public abstract class SubscribeModel : Model
    {
        [ForeignKey("Company")]
        [Required]
        public int CompanyId { get; set; }
    }
}
