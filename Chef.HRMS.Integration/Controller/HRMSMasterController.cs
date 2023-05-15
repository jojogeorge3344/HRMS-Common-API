using Chef.Common.Models;
using Chef.HRMS.Integration.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.HRMS.Integration;


[ApiController]
[Route("api/finance/[controller]")]
[AllowAnonymous]
public class HRMSMasterController : ControllerBase
{
    private readonly IHRMSMasterService hRMSMasterService;
    public HRMSMasterController(IHRMSMasterService hRMSMasterService)
    {
        this.hRMSMasterService = hRMSMasterService;
    }
    [HttpGet("GetPaygroup/{Id}")]
    public async Task<ActionResult<IEnumerable<PayGroup>>>GetPaygroup(int Id)
    {
        IEnumerable<PayGroup> payGroup = await hRMSMasterService.GetPaygroup(Id);
        return Ok(payGroup);
    }
    [HttpGet("GetPayRollComponent")]
    public async Task<ActionResult<IEnumerable<HRMSPayGroupPayRollComoponentDetails>>> GetPayRollComponent()
    {
        IEnumerable<HRMSPayGroupPayRollComoponentDetails> payRollComponents = await hRMSMasterService.GetPayRollComponent();
        return Ok(payRollComponents);
    }


}
