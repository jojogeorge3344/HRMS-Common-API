
using Chef.Finance.Integration.Models;
namespace Chef.Finance.Integration;
public  interface IIntegrationJournalRepository : IGenericRepository<TradingIntegrationHeader>
{
    Task<IEnumerable<TradingIntegrationHeader>> GetAll(int transorginId, int transtypeId, DateTime fromDate, DateTime toDate);

    Task<IEnumerable<IntegrationDetails>> GetAllIntegrationDetailsById(int integrationId);
}
