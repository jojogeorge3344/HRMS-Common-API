using System.Collections;

using Chef.Finance.Integration.Models;
namespace Chef.Finance.Integration;
public interface IItemTransactionPostingService: IAsyncService<TradingIntegrationHeader>
{
    Task<IEnumerable<ItemTransactionFinanceDetailsDto>> PostItems(List<ItemTransactionFinanceDTO> itemTransactionFinanceDTO);
}
