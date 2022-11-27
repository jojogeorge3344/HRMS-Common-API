﻿
using Chef.Finance.Integration.Models;

namespace Chef.Finance.Integration.Controllers;

[Route("api/finance/[controller]/[action]")]
[ApiController]
public class SalesOrderReceiptController : ControllerBase
{
    private readonly ISalesOrderReceiptService salesOrderReceiptService;
    public SalesOrderReceiptController(ISalesOrderReceiptService salesOrderReceiptService)
    {
        this.salesOrderReceiptService = salesOrderReceiptService;
    }

    [HttpPost]
    public async Task<ActionResult<string>> Post(SalesOrderReceiptDto salesOrderReceiptDto)
    {
        return Ok(await salesOrderReceiptService.PostAsync(salesOrderReceiptDto));
    }
}
