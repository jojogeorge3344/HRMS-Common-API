using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Models
{
    public class Bank : Model
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        [StringLength(8)]
        public string Code { get; set; }
    }
}