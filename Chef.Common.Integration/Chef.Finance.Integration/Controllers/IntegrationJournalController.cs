
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

        [HttpGet("GetAll/{transorginId}/{transtypeId}/{fromDate}/{toDate}")]
        public async Task<ActionResult<IAsyncEnumerable<TradingIntegrationHeader>>> GetAll(int transorginId, int transtypeId, DateTime fromDate, DateTime toDate)
        {
            return Ok(await integrationJournalService.GetAll( transorginId,  transtypeId,  fromDate,  toDate));
        }

        [HttpGet("GetAllIntegrationDetailsById/{integrationId}")]
        public async Task<ActionResult<IAsyncEnumerable<IntegrationDetails>>> GetAllIntegrationDetailsById(int integrationId)
        {
            return Ok(await integrationJournalService.GetAllIntegrationDetailsById(integrationId));
        }
    }

