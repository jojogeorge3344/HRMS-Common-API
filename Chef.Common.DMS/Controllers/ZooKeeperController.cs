namespace Chef.Common.DMS.Controllers;

[Authorize]
[ApiController]
[Route("api/dms/[controller]/[action]")]
public class ZooKeeperController : ControllerBase
{
    private readonly IZooKeeperService zooKeeperService;

    public ZooKeeperController(IZooKeeperService zooKeeperService)
    {
        this.zooKeeperService = zooKeeperService;
    }

    [HttpGet]
    [AllowAnonymous]
    public ActionResult CreateSchema()
    {
        zooKeeperService.CreateSchema();

        return Ok();
    }
}
