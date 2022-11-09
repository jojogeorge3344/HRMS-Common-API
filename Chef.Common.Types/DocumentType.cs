using System.ComponentModel;

namespace Chef.Common.Types
{
    public enum DocumentType
    {
        [Description("Manual Journal Entry")]
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
        [Description("Receipt Register")]
        ReceiptRegister = 7000,
        [Description("Sales Invoice")]
        SalesInvoice = 7001,
        [Description("Customer Credit Notes")]
        CustomerCreditNote = 7002,
        CustomerAllocation = 7003,
        [Description("Supplier Credit Note")]
        SupplierCreditNote = 7004,
        BankReconciliation = 7005,
        PurchaseInvoice = 7006,
        CashCollectionDeposit = 7007,
        IntegrationTransaction = 8000,
        GoodsReceiptNote = 10000,
        IntegrationHeader = 9000
    }
}