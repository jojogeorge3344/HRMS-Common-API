using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Dtos
{
    public class ItemFamilyDeleteDto
    {
        [Required]
        public string ItemFamilyCode { get; set; } = string.Empty;

        [Required]
        public string Host { get; set; }
    }
}
