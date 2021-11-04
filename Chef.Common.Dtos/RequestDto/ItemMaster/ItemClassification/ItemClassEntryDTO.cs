using System.ComponentModel.DataAnnotations;
using Chef.Common.Models;

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
