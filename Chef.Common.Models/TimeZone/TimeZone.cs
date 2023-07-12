using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Models;

public class TimeZone : Model
{
    [Required]
    public string TimeZoneId { get; set; }

    [Required]
    public string DisplayName { get; set; }

    [Required]
    public string BaseUtcOffset { get; set; }
}
