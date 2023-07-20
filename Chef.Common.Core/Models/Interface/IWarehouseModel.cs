using Chef.Common.Core;

namespace Chef.Trading.Models;

public interface IWarehouseModel : IModel
{
    public int WarehouseId { get; set; }
    public string WarehouseCode { get; set; }
}
