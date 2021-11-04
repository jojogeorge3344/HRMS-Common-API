using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Dtos
{
    public class ItemDeleteDto
    {
        [Required]
        public string ItemCode { get; set; } = string.Empty;

        [Required]
        public string Host { get; set; }
    }
}
