using AutoMapper;

namespace Chef.Common.Authentication.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly IConfiguration configuration;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly IJwtUtils jwtUtils;
    private readonly IMapper mapper;

    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;

    public AuthRepository(
        IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor,
        IJwtUtils jwtUtils,
        IMapper mapper,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        this.configuration = configuration;
        this.httpContextAccessor = httpContextAccessor;
        this.jwtUtils = jwtUtils;
        this.mapper = mapper;

        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    public async Task<IdentityResult> RegisterAdmin(RegisterDto registerModel)
    {
        var userExists = await userManager.FindByNameAsync(registerModel.Username);
        if (userExists != null)
        {
            throw new DuplicateUserException("User name already exists!");
        }

        var user = mapper.Map<ApplicationUser>(registerModel);
        user.SecurityStamp = Guid.NewGuid().ToString();

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

    public async Task<IdentityResult> RegisterUser(RegisterDto registerModel)
    {
        var userExists = await userManager.FindByNameAsync(registerModel.Username);
        if (userExists != null)
        {
            throw new DuplicateUserException("User name already exists!");
        }

        var user = mapper.Map<ApplicationUser>(registerModel);
        user.SecurityStamp = Guid.NewGuid().ToString();
        
        return await userManager.CreateAsync(user, registerModel.Password);
    }

    public async Task<AuthToken> Login(LoginDto loginModel)
    {
        var user = await userManager.FindByNameAsync(loginModel.Username);
        if (user != null && await userManager.CheckPasswordAsync(user, loginModel.Password))
        {
            if(!HasPermissions(user))
            {
                throw new UnauthorizedAccessException("User do not have permission to login.");
            }

            return await jwtUtils.GenerateJwtToken(user);
        }

        throw new UserNotFoundException("Either the username or password is invalid.");
    }

    private bool HasPermissions(ApplicationUser user)
    {
        //TODO
        //Check user has permission to login to this module.
        return true;
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

    public async Task<UserDto> GetCurrentUser()
    {
        var userName = httpContextAccessor.HttpContext.Items["User"].ToString();
        var user = await userManager.FindByNameAsync(userName);
        return mapper.Map<UserDto>(user);
    }

    public async Task<ApplicationUser> GetAuthUser()
    {
        return await userManager.GetUserAsync(httpContextAccessor.HttpContext.User);
    }

    public async Task<ApplicationUser> GetUser(string userName)
    {
        return await userManager.FindByNameAsync(userName);
    }
}

