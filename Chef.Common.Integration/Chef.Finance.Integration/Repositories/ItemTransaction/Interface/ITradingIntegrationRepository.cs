
using Chef.Finance.Integration.Models;
namespace Chef.Finance.Integration;
public interface ITradingIntegrationRepository : IGenericRepository<TradingIntegrationHeader>
{
    Task<IEnumerable<TradingIntegrationHeaderDetailsViewModel>> GetIntegrationHeaderDetails(int integerationHeaderId);

    Task<IEnumerable<ItemTransactionFinanceDetailsDto>> GetItemtransactionFinanceDetails(int headerId);

    Task<int> UpdateStatus(int HeaderId);
}
