namespace Chef.Common.Types
{
    public enum TransactionType
    {
        Return = 1,
        Invoice = 2,
        Receipt = 4,
        Delivery = 5,
        Transferorder = 6,
        Transfercommit = 7,
        InventoryAdjustmentCost = 8,
        InventoryAdjustmentExistingQty = 9,
        InventoryAdjustmentNewQty = 10,
        InventoryCyclecounting = 11,
        MatchandApproval = 12,
    }
}
