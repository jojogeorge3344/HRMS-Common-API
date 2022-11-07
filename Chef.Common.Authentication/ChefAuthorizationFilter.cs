using System.IdentityModel.Tokens.Jwt;
using Chef.Common.Core.Repositories;
using Chef.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Chef.Common.Authentication
{
    public class ChefAuthorizationFilter : IAuthorizationFilter
    {
        public string Feature { get; set; }
        public string NodeCode { get; set; }

        public ChefAuthorizationFilter(
            string feature,
            string nodeCode)
        {
            this.Feature = feature;
            this.NodeCode = nodeCode;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            
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
