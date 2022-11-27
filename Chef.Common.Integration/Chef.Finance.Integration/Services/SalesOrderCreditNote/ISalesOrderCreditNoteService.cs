
using Chef.Finance.Integration.Models;

namespace Chef.Finance.Integration;

public interface ISalesOrderCreditNoteService : IAsyncService<SalesReturnCreditDto>
{
    Task<string> PostAsync(SalesReturnCreditDto salesReturnCreditDto);
}
