
using Chef.Finance.Integration.Models;
namespace Chef.Finance.Integration;
public interface IIntegrationJournalService: IAsyncService<TradingIntegrationHeader>
{
    Task<IEnumerable<TradingIntegrationHeader>> GetAll(int transorginId, int transtypeId, DateTime fromDate, DateTime toDate, int status);

    Task<IEnumerable<IntegrationDetalDimensionViewModel>> GetAllIntegrationDetailsDimensionById(int integrationId);

    Task<int> PostLedger(int[] headerId);
}



