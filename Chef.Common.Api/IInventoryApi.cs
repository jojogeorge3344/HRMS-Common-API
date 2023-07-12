using Refit;

namespace Chef.Common.Api;

public interface IInventoryApi
{
    [Put("/QuantityAdjustment/UpdateStatus/{inventoryAdjustmentId}/{status}/{remark}")]
    Task<int> UpdateInventoryAdjustmentStatus(int inventoryAdjustmentId, int status, string remark);

    [Put("/WarehouseTransferRequest/UpdateStatus/{warehouseTransferId}/{status}/{remark}")]
    Task<int> UpdateWareHoseTranferStatus(int warehouseTransferId, int status, string remark);

    [Put("/CycleCounting/UpdateStatus/{cycleCountingId}/{status}/{remark}")]
    Task<int> UpdateCycleCountingStatus(int cycleCountingId, int status, string remark);

    [Put("/CostCorrectionAdjustment/UpdateStatus/{costCorrectionId}/{status}/{remark}")]
    Task<int> UpdateCostCorrectionStatus(int costCorrectionId, int status, string remark);
}
