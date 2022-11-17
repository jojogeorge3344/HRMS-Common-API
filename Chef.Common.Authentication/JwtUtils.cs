using Microsoft.Extensions.Options;

namespace Chef.Common.Authentication;

public interface IJwtUtils
{
    public Task<AuthToken> GenerateJwtToken(ApplicationUser user);
    public string ValidateJwtToken(string token);
}

public class JwtUtils : IJwtUtils
{
    private readonly JwtConfigOptions jwtConfig;
    private readonly UserManager<ApplicationUser> userManager;

    public byte[] Key => Encoding.UTF8.GetBytes(jwtConfig.Secret);

    public JwtUtils(
        IOptions<JwtConfigOptions> jwtConfig,
        UserManager<ApplicationUser> userManager)
    {
        this.jwtConfig = jwtConfig.Value;
        this.userManager = userManager;
    }

    public async Task<AuthToken> GenerateJwtToken(ApplicationUser user)
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims(user);
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

        return new AuthToken()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions),
            Username = user.UserName
        };
    }

    public string ValidateJwtToken(string token)
    {
        if (token == null)
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var username = jwtToken.Claims.First(x => x.Type == ClaimTypes.Name).Value;

            // return user id from JWT token if validation successful
            return username;
        }
        catch
        {
            // return null if validation fails
            return null;
        }
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
        var secret = new SymmetricSecurityKey(Key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var tokenOptions = new JwtSecurityToken
        (
            claims: claims,
            expires: DateTime.Now.AddDays(jwtConfig.ExpiresIn),
            signingCredentials: signingCredentials
        );

        return tokenOptions;
    }
}
