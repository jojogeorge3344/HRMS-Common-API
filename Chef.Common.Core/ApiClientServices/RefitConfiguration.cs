using System;
using System.Linq;
using System.Net.Http;
using Chef.Common.Core.Extensions;
using Chef.Common.Models;
using Microsoft.AspNetCore.Http;

namespace Chef.Common.Core;

public class RefitConfiguration : IRefitConfiguration
	{
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly ITenantProvider tenantProvider;

    public RefitConfiguration(
        IHttpContextAccessor httpContextAccessor,
        ITenantProvider tenantProvider)
    {
        this.httpContextAccessor = httpContextAccessor;
        this.tenantProvider = tenantProvider;
    }

    public void Configure(string module, ref HttpClient client)
    {
        var tenant = tenantProvider.GetCurrent();

        Module apiClient = tenant.Modules?
            .Where(x => x.Name == module).FirstOrDefault()
            ?? throw new Exception(string.Format("Tenant/ApiClients name - {0} not configured properly.", module));

        client.BaseAddress = new Uri(apiClient.Host);

        //set auth token if available
        var token = TokenDecryptorExtension.GetToken(this.httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString());
        if (token != null)
        {
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        }
    }
}

