using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using Chef.Common.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Chef.Common.Authentication;

public class ChefAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private IAuthenticationRepository authenticationRepository;

    public ChefAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IAuthenticationRepository authenticationRepository)
        : base(options, logger, encoder, clock)
    {
        this.authenticationRepository = authenticationRepository;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
            return AuthenticateResult.Fail("Missing Authorization Header");

        ApplicationUser user;

        try
        {
            user = await authenticationRepository.GetAuthUser();
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
        }
        catch
        {
            return AuthenticateResult.Fail("Invalid Authorization Header");
        }

        var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
            };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}
