namespace Chef.Common.Authentication.Repositories;

public class AuthService : IAuthService
{
    private readonly IAuthRepository authRepository;

    public AuthService(IAuthRepository authRepository)
    {
        this.authRepository = authRepository;
    }

    public async Task<IdentityResult> RegisterAdmin(RegisterDto registerModel)
    {
        return await authRepository.RegisterAdmin(registerModel);
    }

    public async Task<IdentityResult> RegisterUser(RegisterDto registerModel)
    {
        return await authRepository.RegisterUser(registerModel);
    }

    public async Task<AuthToken> Login(LoginDto loginModel)
    {
        return await authRepository.Login(loginModel);
    }

    private bool HasPermissions(ApplicationUser user)
    {
        //TODO
        //Check user has permission to login to this module.
        return true;
    }

    public async Task<IdentityResult> ChangePassword(ChangePasswordModel changePasswordModel)
    {
        return await authRepository.ChangePassword(changePasswordModel);
    }

    public async Task<UserDto> GetCurrentUser()
    {
        return await authRepository.GetCurrentUser();
    }

    public async Task<ApplicationUser> GetAuthUser()
    {
        return await authRepository.GetAuthUser();
    }

    public async Task<ApplicationUser> GetUser(string userName)
    {
        return await authRepository.GetUser(userName);
    }
}

