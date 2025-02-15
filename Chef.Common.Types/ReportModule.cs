﻿namespace Chef.Common.Types;

public enum ReportModule
{
    ManualJournalEntry = 1,
    SalesInvoice = 2,
    CreditNote = 3,
    CustomerAllocation_CreditNoteAllocation = 4,
    CustomerAllocation_ReceiptAgainstInvoices = 5,
    CustomerAllocation_DepositAllocation = 6,
    CustomerAllocation_AdvanceAllocation = 7,
    CustomerAllocation_SupplierAdvanceRefund = 8,
    CustomerAllocation_SupplierDepositRefund = 9,
    APInvoiceRegister = 10,
    DebitNote = 11,
    SupplierAllocation_CreditNoteAllocation = 12,
    SupplierAllocation_OnAccountPaymentAllocation = 13,
    SupplierAllocation_DepositAllocation = 14,
    SupplierAllocation_AdvanceAllocation = 15,
    SupplierAllocation_CustomerAdvanceRefundAllocation = 16,
    SupplierAllocation_CustomerDepositRefundAllocation = 17,
    ReceiptRegister = 18,
    ReceiptProcess = 19,
    PettyCashRegister = 20,
    PettyCashVoucher = 21,
    PettyCashProcess = 22,
    CashCollectionDeposit = 23,
    PaymentAdvice_InvoicePayment = 24,
    PaymentAdvice_OnAccountPaymentToSuppliers = 25,
    PaymentAdvice_AdvancePaymentToSuppliers = 26,
    PaymentAdvice_DepositPaymentToSuppliers = 27,
    PaymentAdvice_DepositRefundToCustomers = 28,
    PaymentAdvice_PettyCashReimbursement = 29,
    PaymentAdvice_BankToBankFundTransfer = 30,
    PaymentAdvice_AdvanceRefundToCustomers = 31,
    PaymentAdvice_OneTimePayment = 32,
    GeneralLedgerReport = 33,
    PaymentDocument_Voucher = 34,
    CustomerTransactionDetail =36,
    SupplierTransactionDetail =37
}
