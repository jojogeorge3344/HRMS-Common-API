using Chef.Finance.Integration.Models;

namespace Chef.Finance.Integration;

public interface ISalesOrderInvoiceService : IAsyncService<SalesInvoiceDto>
{
   new Task<string> InsertAsync(SalesInvoiceDto salesInvoiceDto);
}
