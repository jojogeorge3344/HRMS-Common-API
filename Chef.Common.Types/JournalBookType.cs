using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Types
{
    public enum JournalBookTypeCodes
    {

        ManualDoubleEntryJournal = 100,
        InterBranchTransactionJournal = 101,
        YearClosingJournal = 102,
        GLCurrencyRevaluation = 103,
        Openingbalancejournal = 104,
        RecurringandReversalJournal = 105,
        CustomerCreditNote = 200,
        CustomerCreditNoteAllocation = 201,
        CustomerDepositAllocation = 202,
        CustomeronAccountReceiptsAllocation = 203,
        CustomerAdvanceReceipt = 204,
        CustomerDepositReceipt = 205,
        SupplierAdvanceRefund = 206,
        SupplierDepositRefund = 207,
        ManualSalesInvoiceJournal = 208,
        TradeSalesInvoices = 209,
        CustomerReceiptAgainstInvoices = 210,
        CustomerAdvanceAllocation = 211,
        IntercompanyTransactionJournal = 300,
        BankReconciliation = 301,
        ARAPBalanceWriteOff = 302,
        ARAPKnockOff = 303,
        BadDebtsWriteOff = 304,
        SupplierCurrencyRevaluation = 305,
        CustomerCurrencyRevaluation = 306,
        BanktoBankTransfers = 307,
        PettyCashPayments = 400,
        PettyCashReimbursement = 401,
        SupplierInvoiceWithoutPOMatching = 500,
        SupplierAdvancePayment = 502,
        SupplierDepositPayment = 503,
        CustomerRefundofDeposit = 504,
        CustomerRefundofAdvance = 505,
        SupplierCreditNote = 507,
        SupplierDepositAllocation = 509,
        SupplierAdvanceAllocation = 510,
        SupplierCreditNoteAllocation = 511,
        SupplierInvoiceWithPOMatching = 512,
        MaterialIssueIntegration = 608,
        ProductionClosingIntegration = 613,
        PurchaseModuleIntegration = 600,
        MachineTimesheetIntegration = 611,
       // ManpowerTimesheetIntegration = 610,
        ManpowerTimesheetIntegration = 609,
        MatchandApproval = 619,
        SalesReturnIntegration = 618,
        SalesInvoicingIntegration = 617,
        SalesIssueIntegration = 616,
        SalesModuleIntegration = 614,
        OverheadAbsorptionIntegration = 612,
        SupplierPaymentAgainstInvoices = 501,
        SupplierOnAccountPayment = 506,
        SupplierOnAccountPaymentAllocation = 508,
        CashCollectionAccount = 700,
        GoodsReceiptsIntegration = 601,
        GoodsReturnIntegration = 602,
        InventoryModuleIntegration = 603,
        InventoryAdjustmentIntegration = 604,
        InventoryCycleCountingIntegration = 605,
        WarehouseTransferIntegration = 606,
        ManufacturingModuleIntegration = 607,


    }
}
