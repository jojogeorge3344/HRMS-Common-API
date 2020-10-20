using Chef.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Dtos
{
    public class ItemCommodityEntryDto : ItemCommodity
    {
        public bool AutoGenerateCode { get; set; } = true;
        public string Host { get; set; }
        [Required]
        public string ItemClassCode { get; set; }

    }
}
