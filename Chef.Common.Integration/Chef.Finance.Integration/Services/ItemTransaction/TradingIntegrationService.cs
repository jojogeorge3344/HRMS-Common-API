
using Chef.Finance.Integration.Models;

namespace Chef.Finance.Integration;
public class TradingIntegrationService : AsyncService<TradingIntegrationHeader>, ITradingIntegrationService
    {
        private readonly ITenantSimpleUnitOfWork tenantSimpleUnitOfWork;
        private readonly ITradingIntegrationRepository tradingIntegrationRepository;

        public TradingIntegrationService(ITradingIntegrationRepository tradingIntegrationTransactionRepository,ITenantSimpleUnitOfWork tenantSimpleUnitOfWork)
        {
            this.tenantSimpleUnitOfWork = tenantSimpleUnitOfWork;
            this.tradingIntegrationRepository = tradingIntegrationTransactionRepository;
        }
        public  async Task<TradingIntegrationHeader> InsertAsync(List<ItemTransactionFinance> itemTransactionFinances)
        {
            TradingIntegrationHeader tradingIntegrationHeader = new TradingIntegrationHeader();
            tenantSimpleUnitOfWork.BeginTransaction();
            try
            {
                //tradingIntegrationHeader = await tradingIntegrationRepository.InsertAsync(itemTransactionFinances);
                tenantSimpleUnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                tenantSimpleUnitOfWork.Rollback();
                throw;
            }
            return tradingIntegrationHeader;
        }
    }

