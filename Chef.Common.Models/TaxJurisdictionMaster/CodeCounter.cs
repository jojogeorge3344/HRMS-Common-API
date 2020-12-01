using Chef.Common.Core;
using Chef.Common.Types;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Models
{
    public class CodeCounter : Model
    {
        [Required]

        public CodeType CodeType { get; set; }
        public int Counter { get; set; }

        public int DeleteCounter { get; set; }
    }
}