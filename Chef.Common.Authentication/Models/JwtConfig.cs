namespace Chef.Common.Authentication.Models;

public class JwtConfigOptions
{
    public const string JwtConfig = "JwtConfig";

    public string Secret { get; set; }
    public int ExpiresIn { get; set; }
}

