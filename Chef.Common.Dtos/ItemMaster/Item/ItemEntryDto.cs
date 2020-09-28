using Chef.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Dtos
{
    public class ItemEntryDto : Item
    {

        [Required]
        public string ItemCommodityCode { get; set; }
        public bool AutoGenerateCode { get; set; } = true;
        public string Host { get; set; }

    }
}
