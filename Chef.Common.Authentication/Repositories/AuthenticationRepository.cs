using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Chef.Common.Authentication.Models;
using Chef.Common.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Chef.Common.Authentication.Repositories;

public class AuthenticationRepository : IAuthenticationRepository
{
    private readonly IConfiguration configuration;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;

    public AuthenticationRepository(
        IConfiguration configuration,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        this.configuration = configuration;
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    public async Task<IdentityResult> RegisterAdmin(RegisterModel registerModel)
    {
        var userExists = await userManager.FindByNameAsync(registerModel.Email);
        if (userExists != null)
        {
            throw new DuplicateUserException("User name already exists!");
        }

        ApplicationUser user = new ApplicationUser()
        {
            FirstName = registerModel.FirstName,
            LastName = registerModel.LastName,
            Email = registerModel.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = registerModel.Email
        };

        var result = await userManager.CreateAsync(user, registerModel.Password);

        if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
            await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        if (!await roleManager.RoleExistsAsync(UserRoles.User))
            await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

        if (await roleManager.RoleExistsAsync(UserRoles.Admin))
        {
            await userManager.AddToRoleAsync(user, UserRoles.Admin);
        }
        return result;
    }

    public async Task<IdentityResult> RegisterUser(RegisterModel registerModel)
    {
        var userExists = await userManager.FindByNameAsync(registerModel.Email);
        if (userExists != null)
        {
            throw new DuplicateUserException("User name already exists!");
        }

        ApplicationUser user = new ApplicationUser()
        {
            FirstName = registerModel.FirstName,
            LastName = registerModel.LastName,
            Email = registerModel.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = registerModel.Email
        };

        return await userManager.CreateAsync(user, registerModel.Password);
    }

    public async Task<string> Login(LoginModel loginModel)
    {
        var user = await userManager.FindByNameAsync(loginModel.Email);
        if (user != null && await userManager.CheckPasswordAsync(user, loginModel.Password))
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        throw new UserNotFoundException("Either the username or password is invalid");
    }

    public async Task<IdentityResult> ChangePassword(ChangePasswordModel changePasswordModel)
    {
        var user = await userManager.FindByEmailAsync(changePasswordModel.Email);

        if (user != null && await userManager.CheckPasswordAsync(user, changePasswordModel.CurrentPassword))
        {

            IdentityResult result;
            foreach (var passwordValidator in userManager.PasswordValidators)
            {
                result = await passwordValidator.ValidateAsync(userManager, user, changePasswordModel.NewPassword);

                if (!result.Succeeded)
                {
                    return result;
                }
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            return await userManager.ResetPasswordAsync(user, token, changePasswordModel.NewPassword);
        }

        throw new UserNotFoundException("Either the username or password is invalid");
    }

    private async Task<List<Claim>> GetClaims(ApplicationUser? user)
    {
        var userRoles = await userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        return authClaims;
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(configuration["jwtConfig:Secret"]);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var jwtSettings = configuration.GetSection("JwtConfig");
        var tokenOptions = new JwtSecurityToken
        (
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expiresIn"])),
            signingCredentials: signingCredentials
        );
        return tokenOptions;
    }
}

