using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Models
{
    public class CurrencyMaster : Model
    {
        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [Required]
        [StringLength(3)]
        public string Code { get; set; }

        [StringLength(3)]
        public string Symbol { get; set; }
    }
}