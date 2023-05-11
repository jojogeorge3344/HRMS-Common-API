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
    [HttpGet("GetPaygroup")]
    public async Task<ActionResult<IEnumerable<PayGroup>>>GetPaygroup()
    {
        IEnumerable<PayGroup> payGroup = await hRMSMasterService.GetPaygroup();
        return Ok(payGroup);
    }
    [HttpGet("GetPayRollComponent")]
    public async Task<ActionResult<IEnumerable<PayRollComponentViewModel>>> GetPayRollComponent()
    {
        IEnumerable<PayRollComponentViewModel> payRollComponents = await hRMSMasterService.GetPayRollComponent();
        return Ok(payRollComponents);
    }


}
