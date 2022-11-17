using Chef.Common.Authentication;
using Chef.Common.Authentication.Models;
using Chef.Common.Exceptions;
using Chef.Common.Models;

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
        var branches = await commonDataService.GetBranches();

        if (branches == null)
        {
            throw new CompanyNotFoundException("The branches does not exist.");
        }

        return Ok(branches);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserBranchDto>>> GetMyBranches()
    {
        var branches = await commonDataService.GetMyBranches();

        if (branches == null)
        {
            throw new CompanyNotFoundException("The branches does not exist.");
        }

        return Ok(branches);
    }
}

