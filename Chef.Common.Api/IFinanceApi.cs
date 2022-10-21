using Chef.Common.Models;
using Refit;

namespace Chef.Common.Api
{
    public interface IFinanceApi
    {
        [Get("/Branch/CheckForBranch/{id}")]
        Task<int> IsBranchUsed(int id);

        //TODO return only int in finance
        [Post("/JournalBook/InsertJournalBooksForBranch")]
        Task<Branch> CreateJournalBooksForBranchAsync([Body] Branch branch);


        [Put("/Purchaseinvoice/UpdateStatus/{Invoiceid}/{status}/{remark}")]
        Task<int> UpdateInvoiceRegisterStatus(int Invoiceid, int status, string remark);

        [Put("/SupplierCreditNote/UpdateStatus/{creditnoteid}/{status}/{remark}")]
        Task<int> UpdateCreditNoteStatus(int creditnoteid, int status, string remark);

        [Put("/AccountsPayable/UpdateSupplierTransactionStatus/{id}/{status}/{remark}")]
        Task<int> UpdateSupplierTransactionStatus(int id, int status, string remark);

        [Put("/CustomerCreditNote/UpdateStatus/{creditnoteid}/{status}/{remark}")]
        Task<int> UpdateCustomerCreditNoteStatus(int creditnoteid, int status, string remark);

        [Put("/SalesInvoice/UpdateSalesInvoiceStatus/{id}/{status}/{remark}")]
        Task<int> UpdateSalesInvoiceStatus(int id, int status, string remark);

        [Put("/ManualJournalEntry/UpdateManualJournalEntryStatus/{id}/{status}/{remark}")]
        Task<int> UpdateManualJournalEntryStatus(int id, int status, string remark);

        [Put("/ReceiptRegister/UpdateReceiptRegisterStatus/{id}/{status}/{remark}")]
        Task<int> UpdateReceiptRegisterStatus(int id, int status, string remark);

        [Put("/PaymentAdvice/UpdatePaymentAdviceStatus/{id}/{status}/{remark}")]
        Task<int> UpdatePaymentAdviceStatus(int id, int status, string remark);



        [Post("/Purchaseinvoice/UpdateStatus")]
        Task<int> PurchaseInvoiceUpdateStatus([Body] ApproveRejectDocViewModel approveRejectDocViewModel);

        [Post("/CustomerCreditNote/UpdateStatus")]
        Task<int> CustomerCreditNodeUpdateStatus([Body] ApproveRejectDocViewModel approveRejectDocViewModel);

        [Post("/SupplierTransaction/UpdateSupplierTransactionStatus")]
        Task<int> SupplierTransactionUpdateStatus([Body] ApproveRejectDocViewModel approveRejectDocViewModel);

        [Post("/SalesInvoice/UpdateSalesInvoiceStatus")]
        Task<int> SalesInvoiceUpdateStatus([Body] ApproveRejectDocViewModel approveRejectDocViewModel);

        [Post("/SupplierCreditNote/UpdateStatus")]
        Task<int> SupplierCreditNoteUpdateStatus([Body] ApproveRejectDocViewModel approveRejectDocViewModel);

        [Post("/ManualJournalEntry/UpdateManualJournalEntryStatus")]
        Task<int> ManualJournalEntryUpdateStatus([Body] ApproveRejectDocViewModel approveRejectDocViewModel);

        [Post("/ReceiptRegister/UpdateReceiptRegisterEntryStatus")]
        Task<int> ReceiptRegisterUpdateStatus([Body] ApproveRejectDocViewModel approveRejectDocViewModel);

        [Post("/PaymentAdvice/UpdatePaymentAdviceStatus")]
        Task<int> PaymentAdviceUpdateStatus([Body] ApproveRejectDocViewModel approveRejectDocViewModel);

        [Post("/CustomerAllocationTransaction/UpdateARAllocationTransaction")]
        Task<int> CustomerAllocationUpdateStatus([Body] ApproveRejectDocViewModel approveRejectDocViewModel);

        [Post("/SupplierAllocationTransaction/UpdateAPAllocationTransaction")]
        Task<int> SupplieAllocationUpdateStatus([Body] ApproveRejectDocViewModel approveRejectDocViewModel);
    }
}
