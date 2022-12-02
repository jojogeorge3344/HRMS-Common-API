
using Chef.Finance.Repositories;
using Chef.Finance.Integration.Models;
using AutoMapper;
using Chef.Common.Services;
using Chef.Finance.GL.Repositories;

namespace Chef.Finance.Integration;

public class IntegrationJournalService: BaseService, IAsyncService<TradingIntegrationHeader>, IIntegrationJournalService
    {
        private readonly IIntegrationJournalRepository integrationJournalRepository;
         private readonly IIntegrationDetailDimensionRepository integrationDetailDimensionRepository;
    private readonly ITradingIntegrationRepository tradingIntegrationRepository;
    private readonly IGeneralLedgerRepository generalLedgerRepository;
    public IntegrationJournalService(IIntegrationJournalRepository integrationJournalRepository, IIntegrationDetailDimensionRepository integrationDetailDimensionRepository, ITradingIntegrationRepository tradingIntegrationRepository, IGeneralLedgerRepository generalLedgerRepository)
        {
           this.integrationJournalRepository = integrationJournalRepository;
        this.integrationJournalRepository=integrationJournalRepository;
        this.integrationDetailDimensionRepository = integrationDetailDimensionRepository;
        this.tradingIntegrationRepository=tradingIntegrationRepository;
        this.generalLedgerRepository=generalLedgerRepository;
        }

    private List<GeneralLedger> generalLedgerlList = new List<GeneralLedger>();

    public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

    public async Task<IEnumerable<TradingIntegrationHeader>> GetAll(int transorginId, int transtypeId, DateTime fromDate, DateTime toDate, int status)
    {
        return await integrationJournalRepository.GetAll(transorginId, transtypeId, fromDate, toDate, status);
    }


    public Task<IEnumerable<TradingIntegrationHeader>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async  Task<IEnumerable<IntegrationDetalDimensionViewModel>> GetAllIntegrationDetailsDimensionById(int integrationId)
        {
             IEnumerable<IntegrationDetalDimensionViewModel> integrationDetalDimensionViewModels = await integrationJournalRepository.GetAllIntegrationDetailsDimensionById(integrationId);

             IEnumerable<IntegrationDetailDimension> dimensionsDetails = await integrationDetailDimensionRepository.GetDimensionDetailsbyId(integrationId);
        foreach (IntegrationDetalDimensionViewModel details in integrationDetalDimensionViewModels)
        {
            IEnumerable<IntegrationDetailDimension> dimensions = dimensionsDetails.Where(x => x.Integrationdetailid == details.Id).ToList();
            if (dimensions.Count() > 0)
            {
                List<DetailDimension> Dimension = Mapper.Map<List<DetailDimension>>(dimensions);
                details.detailDimensions = Dimension;
            }
        }

        return integrationDetalDimensionViewModels;
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

    public async Task<int> PostLedger(int[] headerIds)
    {
        try
        {
            foreach (int headerId in headerIds)
            {
                await PostingLedger(headerId);
            }

            int generalLedgerId = await generalLedgerRepository.BulkInsertAsync(generalLedgerlList);
            if (generalLedgerId > 0)
                return 1;
            else
                return 0;
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    private async Task PostingLedger(int integerationHeaderId)
    {
        int HeaderId = await tradingIntegrationRepository.UpdateStatus(integerationHeaderId);
        IEnumerable<TradingIntegrationHeaderDetailsViewModel> tradingIntegrationHeadersDetails = await tradingIntegrationRepository.GetIntegrationHeaderDetails(integerationHeaderId);
        foreach (TradingIntegrationHeaderDetailsViewModel tradingIntegration in tradingIntegrationHeadersDetails)
        {
            GeneralLedger generalLedger = Mapper.Map<GeneralLedger>(tradingIntegration);
            generalLedger.ApproveStatus = Convert.ToInt32(ApproveStatus.Approved);
            generalLedgerlList.Add(generalLedger);
        }
    }

}
