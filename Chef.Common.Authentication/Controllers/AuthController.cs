using Chef.Common.Authentication.Models;
using Chef.Common.Authentication.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult> Login([FromBody] LoginModel loginModel)
    {
        return Ok(await authenticationRepository.Login(loginModel));
    }

    [HttpPost]
    public async Task<ActionResult> ChangePassword(ChangePasswordModel changePasswordModel)
    {
        return Ok(await authenticationRepository.ChangePassword(changePasswordModel));
    }

    [HttpPost]
    public async Task<ActionResult<IdentityResult>> Register([FromBody] RegisterModel registerModel)
    {
        return Ok(await this.authenticationRepository.RegisterUser(registerModel));
    }

    [HttpPost]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel registerModel)
    {
        return Ok(await this.authenticationRepository.RegisterAdmin(registerModel));
    }
}
