using System;
using Microsoft.AspNetCore.Http;

namespace Chef.Common.Core;

public static class HttpHelper
{
    private static IHttpContextAccessor _accessor;

    public static void Configure(IHttpContextAccessor httpContextAccessor)
    {
        _accessor = httpContextAccessor;
    }

    public static HttpContext HttpContext => _accessor.HttpContext;

    public static string Username
    {
        get
        {
            if (_accessor is null)
                return "System";
            return _accessor.HttpContext.Items["User"] != null ? ((Microsoft.AspNetCore.Identity.IdentityUser<string>)_accessor.HttpContext.Items["User"]).UserName : "System";
        }
    }

    public static string BranchId
    {
        get
        {
            if (_accessor.HttpContext != null)
            {
                var branchId = _accessor.HttpContext.Request.Headers["BranchId"];
                if (!string.IsNullOrEmpty(branchId))
                {
                    return branchId;
                }
            }

            throw new UnauthorizedAccessException();
        }
    }
}