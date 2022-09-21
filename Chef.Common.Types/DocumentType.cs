namespace Chef.Common.Types
{
    public enum DocumentType
    {
        ManualEntryJournal = 1000,
        SupplierTransactionJournal = 2000,
        CustomerTransactionJournal = 3000,
        BankPayable = 4000,
        BankReceivable = 4001,
        SupplierAdvicePaymentInvoice = 6000,
        SupplierOtherPayments = 5000,
        SupplierAllocation = 5001,
        PettyCashPayable = 5002,
        PettyCashReceivable = 5003,
        ReceiptRegister = 7000,
        SalesInvoice = 7001,
        CustomerCreditNote = 7002,
        CustomerAllocation = 7003,
        SupplierCreditNote = 7004,
        BankReconciliation = 7005,
        PurchaseInvoice = 7006,
        CashCollectionDeposit = 7007,
        IntegrationTransaction = 8000,
        GoodsReceiptNote = 10000,
       
    }
}