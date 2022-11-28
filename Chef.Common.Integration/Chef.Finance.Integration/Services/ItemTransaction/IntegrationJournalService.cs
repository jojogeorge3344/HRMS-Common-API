
using Chef.Finance.Repositories;
using Chef.Finance.Integration.Models;
namespace Chef.Finance.Integration;

public class IntegrationJournalService: IAsyncService<TradingIntegrationHeader>, IIntegrationJournalService
    {
        private readonly IIntegrationJournalRepository integrationJournalRepository;
        public IntegrationJournalService(IIntegrationJournalRepository integrationJournalRepository)
        {
           this.integrationJournalRepository = integrationJournalRepository;
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TradingIntegrationHeader>> GetAll(int transorginId, int transtypeId, DateTime fromDate, DateTime toDate)
        {
            return await integrationJournalRepository.GetAll(transorginId,transtypeId,fromDate,toDate);
        }

        public  Task<IEnumerable<TradingIntegrationHeader>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async  Task<IEnumerable<IntegrationDetails>> GetAllIntegrationDetailsById(int integrationId)
        {
            return await integrationJournalRepository.GetAllIntegrationDetailsById(integrationId);
        }

        public Task<TradingIntegrationHeader> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TradingIntegrationHeader> InsertAsync(TradingIntegrationHeader obj)
        {
            throw new NotImplementedException();
        }
        public Task<int> UpdateAsync(TradingIntegrationHeader obj)
        {
            throw new NotImplementedException();
        }

        Task<int> IAsyncService<TradingIntegrationHeader>.InsertAsync(TradingIntegrationHeader obj)
        {
            throw new NotImplementedException();
        }
    
}
