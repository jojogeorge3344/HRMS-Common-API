using Chef.Common.Authentication;
using Chef.Finance.Integration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Finance.Integration.Controllers;

[ApiController]
[Route("api/finance/[controller]")]
[AllowAnonymous]
public  class DocumentUpdateController:ControllerBase
{
    private readonly IDocumentUpdateService documentUpdateService;
   public DocumentUpdateController(IDocumentUpdateService documentUpdateService)
    {
        this.documentUpdateService = documentUpdateService;
    }
    [HttpPost("CancelDocumentDetails")]
    public async Task<ActionResult> CancelDocument(ARCancelDto aRCancelDto)
    {
        IntegrationResponseDto status = await documentUpdateService.CancelDocument(aRCancelDto);
        return Ok(status);
    }
}
