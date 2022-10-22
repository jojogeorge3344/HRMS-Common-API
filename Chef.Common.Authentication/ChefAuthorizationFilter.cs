using System.IdentityModel.Tokens.Jwt;
using Chef.Common.Core.Repositories;
using Chef.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Chef.Common.Authentication
{
    public class ChefAuthorizationFilter : IAuthorizationFilter
    {
        private readonly IRedisConnectionFactory redisConnectionFactory;
        private readonly IUserRoleService userRoleService;

        public string Feature { get; set; }
        public string NodeCode { get; set; }

        public ChefAuthorizationFilter(string feature,
            string nodeCode,
            IRedisConnectionFactory redisConnectionFactory,
            IUserRoleService userRoleService)
        {
            this.Feature = feature;
            this.NodeCode = nodeCode;
            this.redisConnectionFactory = redisConnectionFactory;
            this.userRoleService = userRoleService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var isNotAuthenticated = false;

            var token = context.HttpContext.Request.Headers["Authorization"];

            if (!token.Any())
            {
                throw new AccessViolationException("UnAuthorized Access");
            }

            token = token.ToString().Replace("Bearer ", string.Empty);
            var tokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = tokenHandler.ReadToken(token);
            var tokenS = tokenHandler.ReadToken(token) as JwtSecurityToken;

            var id = tokenS.Claims.First(claim => claim.Type == "UserId").Value;
            int userid = int.Parse(id);

            var userData = GetUserData(userid).Result;
            var tes = userData.Where(x => x.NodeName == NodeCode).Where(x => x.PermissionName == Feature);
            isNotAuthenticated = tes.Count() >= 1 ? true : false;

            if (!isNotAuthenticated)
            {
                throw new UnauthorizedAccessException("UnAuthorized Access");
            }
        }

        private async Task<IEnumerable<UserMetaData>> GetUserData(int id)
        {
            return await userRoleService.GetUserRolesByUserIdAsync(id);
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ChefAuthorizeAttribute : TypeFilterAttribute
    {
        public ChefAuthorizeAttribute(string feature, string nodeCode)
            : base(typeof(ChefAuthorizationFilter))
        {
            Arguments = new object[] { feature, nodeCode };
        }
    }
}
