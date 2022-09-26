using Chef.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Dtos
{
    public class ItemClassEntryDto : ItemClass
    {
        public bool AutoGenerateCode { get; set; } = true;

        public string Host { get; set; }

        [Required]
        public string ItemFamilyCode { get; set; }
    }
}
