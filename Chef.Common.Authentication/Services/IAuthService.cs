﻿namespace Chef.Common.Authentication.Repositories;

public interface IAuthService : IBaseService
{
    Task<IdentityResult> RegisterUser(RegisterDto registerModel);
    Task<IdentityResult> RegisterAdmin(RegisterDto registerModel);
    Task<AuthToken> Login(LoginDto loginModel);
    Task<IdentityResult> ChangePassword(ChangePasswordModel changePasswordModel);

    Task<ApplicationUser> GetUser(string userName);

    Task<UserDto> GetCurrentUser();
    Task<ApplicationUser> GetAuthUser();
}
