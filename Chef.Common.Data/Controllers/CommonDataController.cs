using Chef.Common.Authentication;
using Chef.Common.Authentication.Models;

namespace Chef.Common.Data.Services;

[Authorize]
[ApiController]
[Route("api/console/[controller]/[action]")]
public class CommonDataController : ControllerBase
{
    private readonly ICommonDataService commonDataService;

    public CommonDataController(ICommonDataService commonDataService)
    {
        this.commonDataService = commonDataService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Branch>>> GetBranches()
    {
        return Ok(await commonDataService.GetBranches());
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserBranchDto>>> GetMyBranches()
    {
        return Ok(await commonDataService.GetMyBranches());
    }
}

