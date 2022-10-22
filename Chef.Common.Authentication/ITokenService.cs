namespace Chef.Common.Authentication
{
    public interface ITokenService
    {
        AccessToken GetToken(string scope);
        AccessToken GetMobileAccessToken(string scope);
    }
}
