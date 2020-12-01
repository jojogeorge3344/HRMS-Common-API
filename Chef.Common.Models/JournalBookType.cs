using Chef.Common.Core;
using Chef.Common.Types;
using System.ComponentModel.DataAnnotations;

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