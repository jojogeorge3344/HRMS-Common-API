
using Chef.Finance.Integration.Models;

namespace Chef.Finance.Integration;

public interface ISalesOrderCreditNoteService : IAsyncService<SalesReturnCreditDto>
{
    Task<SalesReturnCreditResponse> PostAsync(SalesReturnCreditDto salesReturnCreditDto);
}
