
using Chef.Finance.Integration.Models;
namespace Chef.Finance.Integration;
public  interface IIntegrationJournalRepository : IGenericRepository<TradingIntegrationHeader>
{
    Task<IEnumerable<TradingIntegrationHeader>> GetAll(int transorginId, int transtypeId, DateTime fromDate, DateTime toDate, int status);

    Task<IEnumerable<IntegrationDetalDimensionViewModel>> GetAllIntegrationDetailsDimensionById(int integrationId);

    Task<int> GetintegrationheaderId(string documentNumber);

    Task<int> Deleteintegrationheader(int tradingintegrationheaderId);
}
