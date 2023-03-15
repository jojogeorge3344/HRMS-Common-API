using Chef.Finance.Integration.Models;

namespace Chef.Finance.Integration;

public interface ISalesOrderInvoiceService : IAsyncService<SalesInvoiceDto>
{
   new Task<SalesInvoiceResponse> Insert(SalesInvoiceDto salesInvoiceDto);

    new Task<SalesInvoiceResponse> ViewSalesInvoice(SalesInvoiceDto salesInvoiceDto);
}
