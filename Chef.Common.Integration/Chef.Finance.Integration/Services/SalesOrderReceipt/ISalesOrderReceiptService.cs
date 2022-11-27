using Chef.Finance.Integration.Models;

namespace Chef.Finance.Integration;

public interface ISalesOrderReceiptService : IAsyncService<SalesOrderReceiptDto>
{
    Task<string> PostAsync(SalesOrderReceiptDto salesReturnCreditDto);
}
