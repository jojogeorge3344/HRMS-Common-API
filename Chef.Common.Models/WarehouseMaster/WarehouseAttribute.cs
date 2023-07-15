using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;


namespace Chef.Trading.Models;

public class WarehouseAttribute : Model, IAttributeModel
{
    [Required]
    [ForeignKeyId(typeof(Warehouse))]
    [Unique(true), Composite(Index = 1)]
    public int WarehouseId { get; set; }
    [Required]
    [Unique(true), Composite(Index = 2)]
    [Field(Order = 3)]
    public string AttributeName { set; get; }
    [Field(Order = 3)]
    public string AttributeValue { set; get; }
}
