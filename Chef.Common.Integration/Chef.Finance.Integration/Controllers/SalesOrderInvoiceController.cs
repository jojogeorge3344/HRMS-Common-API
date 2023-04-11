using Chef.Common.Authentication;
using Chef.Finance.Integration.Models;

namespace Chef.Finance.Integration.Controllers;

[Route("api/finance/[controller]/[action]")]
[ApiController]
[AllowAnonymous]
public class SalesOrderInvoiceController : ControllerBase
{
    private readonly ISalesOrderInvoiceService salesOrderInvoiceService;
    public SalesOrderInvoiceController(ISalesOrderInvoiceService salesOrderInvoiceService)
    {
        this.salesOrderInvoiceService = salesOrderInvoiceService;
    }

    [HttpPost]
    public async Task<ActionResult<SalesInvoiceResponse>> Insert(SalesInvoiceDto salesInvoice)
    {
        return Ok(await salesOrderInvoiceService.Insert(salesInvoice));
    }

    [HttpPost]
    public async Task<ActionResult<SalesInvoiceResponse>> ViewSalesInvoice(SalesInvoiceDto salesInvoice)
    {
        return Ok(await salesOrderInvoiceService.ViewSalesInvoice(salesInvoice));
    }
}
