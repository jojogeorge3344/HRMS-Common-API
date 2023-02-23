using Chef.Common.Authentication;
using Chef.Common.Authentication.Models;
using Chef.Common.Exceptions;
using Chef.Common.Models;

namespace Chef.Common.Data.Controller;

//[Authorize]
[ApiController]
[Route("api/common/[controller]/[action]")]
[AllowAnonymous]
public class CommonDataController : ControllerBase
{
    private readonly ICommonDataService commonDataService;

    public CommonDataController(ICommonDataService commonDataService)
    {
        this.commonDataService = commonDataService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BranchViewModel>>> GetBranches()
    {
        var branches = await commonDataService.GetBranches();

        if (branches == null)
        {
            throw new BranchNotFoundException("The branches does not exist.");
        }

        return Ok(branches);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserBranchDto>>> GetMyBranches()
    {
        var branches = await commonDataService.GetMyBranches();

        if (branches == null)
        {
            throw new BranchNotFoundException("The branches does not exist.");
        }

        return Ok(branches);
    }
    [HttpGet]
    public async Task<ActionResult<ReasonCodeMaster>> GetAllReasonCode()
    {
        var reasonCodemaster = await commonDataService.GetAllReasonCode();
        if (reasonCodemaster == null)
        {
            return NotFound("The reason code control does not exist.");
        }

        return Ok(reasonCodemaster);
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Company>>> GetMyCompany()
    {
        var Company = await commonDataService.GetMyCompany();

        if (Company == null)
        {
            throw new BranchNotFoundException("The branches does not exist.");
        }

        return Ok(Company);
    }

    [HttpPut]
    public async Task<ActionResult<int>> UpdateCompanyLogo(Company company)
    {
        return Ok(await commonDataService.UpdateCompanyLogo(company));
    }

}

