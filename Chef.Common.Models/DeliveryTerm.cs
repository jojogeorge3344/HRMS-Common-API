using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;

namespace Chef.Trading.Models;

public class DeliveryTerm : Model
{
    [Required]
    [Unique(true)]
    [StringLength(6)]
    [Code]
    public string DeliveryCode { get; set; }
    [Required]
    [Unique(true)]
    public int DeliveryDays { get; set; }
    public string Description { get; set; }
}
