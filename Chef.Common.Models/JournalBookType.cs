using System.ComponentModel.DataAnnotations;
using Chef.Common.Core;
using Chef.Common.Types;

namespace Chef.Common.Models
{
    public class JournalBookType : Model
    {
        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [StringLength(4)]
        public string Code { get; set; }

        public JournalBookCategoryType JournalBookCategoryType { get; set; }
    }
}