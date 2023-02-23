
using Chef.Common.Authentication;
using Chef.Finance.Integration.Models;
namespace Chef.Finance.Integration;


[ApiController]
[Route("api/finance/[controller]")]
[AllowAnonymous]
public class ItemTransactionPostingController : ControllerBase
{
    private readonly IItemTransactionPostingService itemTransactionPostingService;
    // private readonly ITradingIntegrationService tradingIntegrationService;

    public ItemTransactionPostingController(IItemTransactionPostingService itemTransactionPostingService)
    {
        this.itemTransactionPostingService = itemTransactionPostingService;
    }

    [HttpPost("PostItems")]
    public async Task<ActionResult> PostItems(List<ItemTransactionFinanceDTO> itemTransactionFinanceDTO)
    {
        var details = await itemTransactionPostingService.PostItems(itemTransactionFinanceDTO);
        return Ok(details);
    }
    [HttpPost("DeletedByDocumentNumber")]
    public async Task<ActionResult> DeletedByDocumentNumber(string documentNumber)
    {
        int status = await itemTransactionPostingService.DeletedByDocumentNumber(documentNumber);
        return Ok(status);
    }


    //public async Task<ActionResult> InsertAsync(List<ItemTransactionFinance>  itemTransactionFinance)
    //{
    //    TradingIntegrationHeader integrationHeader = await tradingIntegrationService.InsertAsync(itemTransactionFinance);
    //    return Ok(integrationHeader);
    //}

}

