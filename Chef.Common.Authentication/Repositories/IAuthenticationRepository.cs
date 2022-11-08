using Chef.Common.Authentication.Models;
using Microsoft.AspNetCore.Identity;

namespace Chef.Common.Authentication.Repositories;

public interface IAuthenticationRepository
{
    Task<IdentityResult> RegisterUser(RegisterModel registerModel);
    Task<IdentityResult> RegisterAdmin(RegisterModel registerModel);
    Task<string> Login(LoginModel loginModel);
    Task<IdentityResult> ChangePassword(ChangePasswordModel changePasswordModel);
}
