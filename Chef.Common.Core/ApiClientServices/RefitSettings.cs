using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Chef.Common.Core.Extensions;
using Chef.Common.Exceptions;
using Chef.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Chef.Common.Core
{
    public class RefitSettings : IRefitSettings
	{
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;

        public RefitSettings(
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
        }

        public void Configure(string module, ref HttpClient client)
        {
            string hostname = httpContextAccessor.HttpContext?.Request.Host.Value.ToLower()
                ?? throw new HttpRequestException("Http context accessor is not initialized.");

            Tenant tenant = configuration.GetSection("Tenants").Get<List<Tenant>>()?
               .Where(x => x.Host == hostname).FirstOrDefault()
               ?? throw new TenantNotFoundException(hostname + " not configured properly.");

            ApiClient apiClient = tenant.ApiClients?
                .Where(x => x.Name == module).FirstOrDefault()
                ?? throw new Exception(string.Format("Tenant/ApiClients name - {0} not configured properly.", module));

            client.BaseAddress = new Uri(apiClient.BaseAddress);

            //set auth token if available
            var token = TokenDecryptorExtension.GetToken(this.httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString());
            if (token != null)
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            }
        }
    }
}

