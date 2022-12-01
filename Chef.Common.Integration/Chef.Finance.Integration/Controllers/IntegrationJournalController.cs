
using Chef.Finance.Integration.Models;
namespace Chef.Finance.Integration;


[ApiController]
    [Route("api/finance/[controller]")]
    public class IntegrationJournalController : ControllerBase
    {
        private IIntegrationJournalService integrationJournalService;

        public IntegrationJournalController(IIntegrationJournalService integrationJournalService)
        {
            this.integrationJournalService = integrationJournalService;
        }

    [HttpGet("GetAll/{transorginId}/{transtypeId}/{fromDate}/{toDate}/{status}")]
    public async Task<ActionResult<IAsyncEnumerable<TradingIntegrationHeader>>> GetAll(int transorginId, int transtypeId, DateTime fromDate, DateTime toDate, int status)
    {
        IEnumerable<TradingIntegrationHeader> tradingIntegrationHeader = await integrationJournalService.GetAll(transorginId, transtypeId, fromDate, toDate, status);
        return Ok(tradingIntegrationHeader);
    }

    [HttpGet("GetAllIntegrationDetailsDimensionById/{integrationId}")]
        public async Task<ActionResult<IAsyncEnumerable<IntegrationDetalDimensionViewModel>>> GetAllIntegrationDetailsDimensionById(int integrationId)
        {
            return Ok(await integrationJournalService.GetAllIntegrationDetailsDimensionById(integrationId));
        }
    [HttpPost("PostLedger")]
    public async Task<ActionResult<int>> PostLedger(int[] headerId)
    {
        int integrationDetails = await integrationJournalService.PostLedger(headerId);
        return Ok(integrationDetails);
    }
}

