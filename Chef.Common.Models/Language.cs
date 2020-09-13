using System.ComponentModel.DataAnnotations;
using Chef.Common.Core;

namespace Chef.Common.Models
{
    public class Language : Model
    {
        [Required]
        [StringLength(126)]
        public string Name { get; set; }

        [Required]
        [StringLength(4)]
        public string Code { get; set; }
    }
}