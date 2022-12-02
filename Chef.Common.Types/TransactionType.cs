namespace Chef.Common.Types
{
    public enum TransactionType
    {
        PurchaseReturn = 1,
        SalesOrderInvoice = 2,
        SalesOrderReturn= 3,
        PurchaseReceipt = 4,
        SalesOrderDelivery = 5,
        WarehouseTransferorder = 6,
        WarehouseTransfercommit = 7,
        WarehouseInventoryAdjustmentCost = 8,
        WarehouseInventoryAdjustmentExistingQty = 9,
        WarehouseInventoryAdjustmentNewQty = 10,
        WarehouseInventoryCyclecounting = 11,
        WarehouseTransferconfirmation=12,
        WarehouseDirectTransfer=13,
        VanSalesOrderDelivery=14,
        VanSalesOrderInvoice=15,
        VanSalesOrderReturn=16,
        RetailSalesOrderDelivery=17,
        RetailSalesOrderInvoice=18,
        RetailSalesOrderReturn=19,
        MatchandApproval = 22
    }
}
