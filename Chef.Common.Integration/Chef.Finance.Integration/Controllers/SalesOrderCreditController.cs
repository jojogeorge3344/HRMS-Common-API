using Chef.Finance.Integration.Models;

namespace Chef.Finance.Integration.Controllers;

[Route("api/finance/[controller]/[action]")]
[ApiController]
public class SalesOrderCreditNoteController : ControllerBase
{
    private readonly ISalesOrderCreditNoteService SalesOrderCreditNoteService;
    public SalesOrderCreditNoteController(ISalesOrderCreditNoteService SalesOrderCreditNoteService)
    {
        this.SalesOrderCreditNoteService = SalesOrderCreditNoteService;
    }

    [HttpPost]
    public async Task<ActionResult<string>> Post(SalesReturnCreditDto salesReturnCreditDto)
    {
        return Ok(await SalesOrderCreditNoteService.PostAsync(salesReturnCreditDto));
    }
}
