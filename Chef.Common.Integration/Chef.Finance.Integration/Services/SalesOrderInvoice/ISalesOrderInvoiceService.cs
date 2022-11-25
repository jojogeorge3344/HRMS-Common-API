using Chef.Finance.Integration.Models;

namespace Chef.Finance.Integration.Services;

public interface ISalesOrderInvoiceService : IAsyncService<SalesInvoiceDto>
{
    Task<string> InsertAsync(SalesInvoiceDto salesInvoiceDto);
}
