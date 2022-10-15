using Refit;

namespace Chef.Common.Api
{
    public interface ITradingApi
    {
        [Put("/PurchaseOrder/UpdateStatus/{purchaseOrderId}/{status}/{remark}")]
        Task<int> UpdatePurchaseOrderStatus(int purchaseOrderId, int status,string remark);

        [Put("/PurchaseRequest/UpdateStatus/{purchaseRequestId}/{status}/{remark}")]
        Task<int> UpdatePurchaseRequestStatus(int purchaseRequestId, int status,string remark);

        [Put("/PurchaseReturn/UpdateStatus/{purchaseReturnId}/{status}/{remark}")]
        Task<int> UpdatePurchaseReturnStatus(int purchaseReturnId, int status,string remark);

        [Put("/PurchaseContract/UpdateStatus/{purchaseContractId}/{status}/{remark}")]
        Task<int> UpdatePurchaseContractStatus(int purchaseContractId, int status,string remark);

        [Put("/SupplierQuote/UpdateStatus/{compareQuoteId}/{status}/{remark}")]
        Task<int> UpdateCompareQuoteStatus(int compareQuoteId, int status,string remark);
    }
}
