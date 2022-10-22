using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Chef.Common.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Chef.Common.Authentication
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<IdentityServerSettings> identityServerSettings;
        private readonly IConfiguration configuration;
        private readonly IRedisConnectionFactory redisConnectionFactory;
        private readonly IHttpContextAccessor httpContextAccessor;

        public TokenService(
            IOptions<IdentityServerSettings> identityServerSettings,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor,
            IRedisConnectionFactory redisConnectionFactory)
        {
            this.identityServerSettings = identityServerSettings;
            this.configuration = configuration;
            this.redisConnectionFactory = redisConnectionFactory;
            this.httpContextAccessor = httpContextAccessor;
        }

        public AccessToken GetToken(string scope)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri(configuration["IdentityServer:Host"] ?? throw new Exception("Identity server is not configured"));

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", configuration["IdentityServer:GrantType"] ?? string.Empty),
                    new KeyValuePair<string, string>("code", scope),
                    new KeyValuePair<string, string>("redirect_uri", configuration["IdentityServer:RedirectUri"] ?? string.Empty),
                    new KeyValuePair<string, string>("client_id", configuration["IdentityServer:ClientId"] ?? string.Empty),
                    new KeyValuePair<string, string>("client_secret", configuration["IdentityServer:ClientSecret"] ?? string.Empty)
                });

                var httpResponseMessage = client.PostAsync("connect/token", content).Result;
                var accessToken = httpResponseMessage.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<AccessToken>(accessToken) ?? throw new Exception("Access token is null.");
            }
        }

        public AccessToken GetMobileAccessToken(string scope)
        {
            AccessToken obj = new AccessToken();
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri(configuration["IdentityServerMobile:Host"] ?? throw new Exception("Identity server is not configured"));
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", configuration["IdentityServerMobile:GrantType"] ?? string.Empty),
                    new KeyValuePair<string, string>("code", scope),
                    new KeyValuePair<string, string>("redirect_uri", configuration["IdentityServerMobile:RedirectUri"] ?? string.Empty),
                    new KeyValuePair<string, string>("client_id", configuration["IdentityServerMobile:ClientId"] ?? string.Empty),
                    new KeyValuePair<string, string>("client_secret", configuration["IdentityServerMobile:ClientSecret"] ?? string.Empty)
                });

                var httpResponseMessage = client.PostAsync("connect/token", content).Result;
                var accessToken = httpResponseMessage.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<AccessToken>(accessToken) ?? throw new Exception("Access token is null.");
            }
        }

        [return: MaybeNull]
        private bool ValidateToken(string token)
        {
            //Grab certificate for verifying JWT signature
            //IdentityServer4 also has a default certificate you can might reference.
            //In prod, we'd get this from the certificate store or similar

            var accessToken = JsonConvert.DeserializeObject<AccessToken>(token)?.access_token;
            return IsValidToken(accessToken ?? throw new Exception("Token is null."));
        }

        private bool IsValidToken(string jwt)
        {
            var _handler = new JwtSecurityTokenHandler();
            var configurationDetail = "/.well-known/openid-configuration";
            string stsDiscoveryEndpoint = "https://192.168.100.54:9030" + configurationDetail;
            ConfigurationManager<OpenIdConnectConfiguration> configManager =
                new ConfigurationManager<OpenIdConnectConfiguration>(stsDiscoveryEndpoint, new OpenIdConnectConfigurationRetriever());
            OpenIdConnectConfiguration config = configManager.GetConfigurationAsync().Result;

            var signKey = config.SigningKeys;
            var keyid = signKey.Select(x => x.KeyId);
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(keyid.ToString() ?? throw new Exception("Key is null.")));

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                IssuerSigningKey = key,
                ValidAudience = "weatherapi",
                ValidIssuer = configuration["IdentityServer:Host"],
                IssuerSigningKeys = signKey,
            };

            try
            {
                var validatedToken = (SecurityToken)new JwtSecurityToken();
                _handler.ValidateToken(jwt, validationParameters, out validatedToken);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}