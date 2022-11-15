namespace Chef.Common.Authentication.Repositories;

public interface IAuthRepository
{
    Task<IdentityResult> RegisterUser(RegisterDto registerModel);
    Task<IdentityResult> RegisterAdmin(RegisterDto registerModel);
    Task<AuthToken> Login(LoginDto loginModel);
    Task<IdentityResult> ChangePassword(ChangePasswordModel changePasswordModel);
    Task<UserDto> GetCurrentUser();
    Task<ApplicationUser> GetAuthUser();
}
