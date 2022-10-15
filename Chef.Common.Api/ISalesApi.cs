using Refit;

namespace Chef.Common.Api
{
    public interface ISalesApi
    {
        [Put("/SalesOrder/UpdateStatus/{salesOrderId}/{status}/{remark}")]
        Task<int> UpdateSalesOrderStatus(int salesOrderId, int status, string remark);

        [Put("/SalesInquiry/UpdateStatus/{salesInquiryId}/{status}/{remark}")]
        Task<int> UpdateSalesInquiryStatus(int salesInquiryId, int status, string remark);

        [Put("/SalesQuotation/UpdateStatus/{salesQuotId}/{status}/{remark}")]
        Task<int> UpdateSalesQuotationStatus(int salesQuotId, int status, string remark);
    }
}
