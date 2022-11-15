namespace Chef.Common.Authentication;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IAuthService authService, IJwtUtils jwtUtils)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        var userName = jwtUtils.ValidateJwtToken(token);
        if (userName != null)
        {
            // attach user to context on successful jwt validation
            context.Items["User"] = authService.GetUser(userName);
        }

        await _next(context);
    }
}
