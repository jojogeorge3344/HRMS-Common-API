using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Dtos
{
    public class ItemClassDeleteDto
    {
        [Required]
        public string ItemClassCode { get; set; } = string.Empty;
        [Required]
        public string Host { get; set; }

    }
}
