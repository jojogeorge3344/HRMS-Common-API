namespace Chef.Common.Authentication.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationRepository authenticationRepository;

    public AuthController(IAuthenticationRepository authenticationRepository)
    {
        this.authenticationRepository = authenticationRepository;
    }

    [HttpPost]
    public async Task<ActionResult<AuthToken>> Login([FromBody] LoginDto loginModel)
    {
        return Ok(await authenticationRepository.Login(loginModel));
    }

    [HttpPost]
    public async Task<ActionResult> ChangePassword(ChangePasswordModel changePasswordModel)
    {
        return Ok(await authenticationRepository.ChangePassword(changePasswordModel));
    }

    [HttpPost]
    public async Task<ActionResult<IdentityResult>> Register([FromBody] RegisterDto registerModel)
    {
        return Ok(await this.authenticationRepository.RegisterUser(registerModel));
    }

    [HttpPost]
    public async Task<ActionResult<IdentityResult>> RegisterAdmin([FromBody] RegisterDto registerModel)
    {
        return Ok(await this.authenticationRepository.RegisterAdmin(registerModel));
    }

    [HttpGet]
    //[Authorize]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        return Ok(await this.authenticationRepository.GetCurrentUser());
    }
}
