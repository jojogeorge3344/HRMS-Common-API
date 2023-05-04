using Chef.Common.Authentication;
using Chef.Common.Data.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Data.Controllers;

[ApiController]
[Route("api/common/[controller]/[action]")]
[AllowAnonymous]

public class CompanyDocumentController : ControllerBase
{
    private readonly ICompanyDocumentService companyDocumentService;
    private readonly ICompanyDocumentAttachmentService companyDocumentAttachmentService;

    public CompanyDocumentController(ICompanyDocumentService companyDocumentService, ICompanyDocumentAttachmentService companyDocumentAttachmentService)
    {
        this.companyDocumentService = companyDocumentService;
        this.companyDocumentAttachmentService = companyDocumentAttachmentService;
    }
    [HttpPost]
    public async Task<ActionResult<int>> Insert([FromForm] CompanyDocuments companyDocuments)
    {
        var files = HttpContext.Request.Form;
        CompanyDocuments documents = JsonConvert.DeserializeObject<CompanyDocuments>(files["companyDocuments"][0]);
        return Ok(await companyDocumentService.Insert(documents));
    }
    [HttpPut]
    public async Task<ActionResult<int>> Update([FromForm] CompanyDocuments companyDocuments)
    {
        var files = HttpContext.Request.Form;
        CompanyDocuments documents = JsonConvert.DeserializeObject<CompanyDocuments>(files["companyDocuments"][0]);
        return Ok(await companyDocumentService.Update(documents));
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompanyDocuments>>>GetCompanyDocumentDetails(int companyId)
    {
        IEnumerable<CompanyDocuments> details = await companyDocumentService.GetCompanyDocumentDetails(companyId);
        if(details == null)
        {
            throw new Exception("Company not found");
        }
        return Ok(details);
    }
    [HttpDelete]
    public async Task<ActionResult<int>>DeleteAttachment(int attachmentId)
    {
        int deleteId = await companyDocumentAttachmentService.DeleteAsync(attachmentId);
        if(deleteId < 1)
        {
            throw new Exception("Company not found");
        }
        return Ok(deleteId);
    }
    [HttpDelete]
    public async Task<ActionResult<int>>Delete(int id)
    {
        int deleteId = await companyDocumentService.Delete(id);
        if (deleteId < 1)
        {
            throw new Exception("Company not found");
        }
        return Ok(deleteId);
    }

}
