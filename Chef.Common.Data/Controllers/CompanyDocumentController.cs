﻿using Chef.Common.Authentication;
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
    public CompanyDocumentController(ICompanyDocumentService companyDocumentService)
    {
        this.companyDocumentService = companyDocumentService;
    }
    [HttpPost]
    public async Task<ActionResult<int>> Insert([FromForm] ComapnyDocuments comapnyDocuments)
    {
        var files = HttpContext.Request.Form;
        ComapnyDocuments documents = JsonConvert.DeserializeObject<ComapnyDocuments>(files["comapnyDocuments"][0]);
        return Ok(await companyDocumentService.Insert(documents));
    }

}
