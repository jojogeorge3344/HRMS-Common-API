namespace Chef.Common.Types
{
    public enum TransactionType
    {
        None = 0,
        GoodsReceipt = 1,
        GoodsReturn = 2,
        InventoryAdjustment = 3,
        InventoryCycleCounting = 4,
        WarehouseTransfer = 5,
        MaterialIssue = 6,
        ManpowerTimesheet = 7,
        MachineTimeSheet = 8,
        OverheadAbsorption = 9,
        ProductionClosing = 10,
        SalesIssue = 11,
        SalesInvoicing = 12,
        SalesReturn = 13,
        MatchandApproval = 14
    }
}
