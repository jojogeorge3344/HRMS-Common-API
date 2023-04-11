namespace Chef.Common.Authentication.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;

    public AuthController(IAuthService authService)
    {
        this.authService = authService;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<AuthToken>> Login([FromBody]LoginDto loginModel)
    {
        return Ok(await authService.Login(loginModel));
    }

    [HttpPost]
    public async Task<ActionResult> ChangePassword(ChangePasswordModel changePasswordModel)
    {
        return Ok(await authService.ChangePassword(changePasswordModel));
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<IdentityResult>> Register([FromBody]RegisterDto registerModel)
    {
        return Ok(await this.authService.RegisterUser(registerModel));
    }

    [HttpPost]
    public async Task<ActionResult<IdentityResult>> RegisterAdmin([FromBody]RegisterDto registerModel)
    {
        return Ok(await this.authService.RegisterAdmin(registerModel));
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        return Ok(await this.authService.GetCurrentUser());
    }
}
