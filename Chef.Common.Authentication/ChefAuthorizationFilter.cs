namespace Chef.Common.Authentication
{
    public class ChefAuthorizationFilter : IAuthorizationFilter
    {
        public ChefAuthorizationFilter()
        {
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if(token == null)
            {
                context.Result = new UnauthorizedResult();
            }
            else
            {
                if (!ValidateToken(context.HttpContext.Request.Headers["Authorization"]))
                {
                    context.Result = new UnauthorizedResult();
                }
            }
        }

        private bool ValidateToken(StringValues stringValues)
        {
            return true;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ChefAuthorizeAttribute : TypeFilterAttribute
    {
        public ChefAuthorizeAttribute()
            : base(typeof(ChefAuthorizationFilter))
        {
        }
    }
}
