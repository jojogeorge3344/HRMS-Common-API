using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Chef.Common.Core.Extensions
{
    public static class TokenDecryptorExtension
    {
        public static JwtSecurityToken Call(string token)
        {
            return ReadToken(token);
        }

        public static string GetEmail(string token)
        {
            var tokenData = ReadToken(token);
            return tokenData.Claims.First(claim => claim.Type == "EmailId").Value.ToString();
        }

        public static JwtSecurityToken ReadToken(string token)
        {
            token = token.ToString().Replace("Bearer ", string.Empty);
            var tokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = tokenHandler.ReadToken(token);

            return tokenHandler.ReadToken(token) as JwtSecurityToken;
        }

        public static string GetToken(string token)
        {
            return token.ToString().Replace("Bearer ", string.Empty);
        }
    }
}
