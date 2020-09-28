using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Dtos
{
    public class ItemSegmentDeleteDto
    {
        [Required]
        public string ItemSegmentCode { get; set; } = string.Empty;
        [Required]
        public string Host { get; set; }
    }
}
