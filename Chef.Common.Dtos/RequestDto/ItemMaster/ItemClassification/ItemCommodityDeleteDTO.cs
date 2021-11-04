using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Dtos
{
    public class ItemCommodityDeleteDto
    {
        [Required]
        public string ItemCommodityCode { get; set; } = string.Empty;

        [Required]
        public string Host { get; set; }
    }
}
