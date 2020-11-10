using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chef.Common.Models
{
    public abstract class SubscribeModel : Model
    {
        [ForeignKeyId(typeof(Company))]
        [Required]
        public int CompanyId { get; set; }

        [ForeignKeyCode(typeof(Company))]
        [Required]
        public string CompanyCode { get; set; }
    }
}
