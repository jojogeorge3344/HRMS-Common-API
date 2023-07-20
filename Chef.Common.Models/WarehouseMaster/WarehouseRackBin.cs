using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;


namespace Chef.Trading.Models;

public class WarehouseRackBin : Model
{
    [Required]
    [ForeignKeyId(typeof(Warehouse))]
    [Unique(true), Composite(Index = 1)]
    public int WarehouseId { get; set; }
    [Required]
    [Unique(true), Composite(Index = 2)]
    public string Rack { get; set; }
    [Unique(true), Composite(Index = 3)]
    public string RackNumber { get; set; }

    public string[] Bins { get; set; }
}
