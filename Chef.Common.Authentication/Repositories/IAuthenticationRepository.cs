﻿namespace Chef.Common.Authentication.Repositories;

public interface IAuthenticationRepository
{
    Task<IdentityResult> RegisterUser(RegisterDto registerModel);
    Task<IdentityResult> RegisterAdmin(RegisterDto registerModel);
    Task<AuthToken> Login(LoginDto loginModel);
    Task<IdentityResult> ChangePassword(ChangePasswordModel changePasswordModel);
    Task<UserDto> GetCurrentUser();
}
