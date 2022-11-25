using Chef.Finance.Integration.Models;
using Chef.Finance.Integration.Services;

namespace Chef.Finance.Integration.Controllers;

[Route("api/finance/[controller]/[action]")]
[ApiController]
public class SalesOrderInvoiceController : ControllerBase
{
    private readonly ISalesOrderInvoiceService salesOrderInvoiceService;
    public SalesOrderInvoiceController(ISalesOrderInvoiceService salesOrderInvoiceService)
    {
        this.salesOrderInvoiceService = salesOrderInvoiceService;
    }

    [HttpPost]
    public async Task<ActionResult<SalesInvoice>> Insert(SalesInvoiceDto salesInvoice)
    {
        return Ok(await salesOrderInvoiceService.InsertAsync(salesInvoice));
    }
}
