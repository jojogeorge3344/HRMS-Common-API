using Chef.Common.Core;
using Chef.Trading.Types;
using System.ComponentModel.DataAnnotations;


namespace Chef.Trading.Models;

public class Warehouse : Model
{
    [Required]
    [Unique(true)]
    [StringLength(6)]
    [Code]
    public string WarehouseCode { get; set; }
    [Required]
    [Unique(true)]
    public string WarehouseName { get; set; }
    [Required]
    public WarehouseType WarehouseType { get; set; }
    [Required]
    [ForeignKeyId(typeof(Plant))]
    public int PlantId { get; set; }
    [ForeignKeyId(typeof(Location))]
    public int LocationId { get; set; }

    [ForeignKeyId(typeof(Address))]
    public int ShipToAddressId { get; set; }
    [ForeignKeyId(typeof(Address))]
    public int BillToAddressId { get; set; }
}
