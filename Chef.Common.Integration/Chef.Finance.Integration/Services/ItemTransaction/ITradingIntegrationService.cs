
using Chef.Finance.Integration.Models;
namespace Chef.Finance.Integration;

public interface ITradingIntegrationService: IAsyncService<TradingIntegrationHeader>
{
    Task<TradingIntegrationHeader> InsertAsync(List<ItemTransactionFinance> itemTransactionFinances);
}
