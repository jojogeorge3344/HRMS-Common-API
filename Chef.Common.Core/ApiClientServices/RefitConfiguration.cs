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
    public class RefitConfiguration : IRefitConfiguration
	{
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IAppConfiguration appConfiguration;

        public RefitConfiguration(
            IHttpContextAccessor httpContextAccessor,
            IAppConfiguration appConfiguration)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.appConfiguration = appConfiguration;
        }

        public void Configure(string module, ref HttpClient client)
        {
            string hostname = httpContextAccessor.HttpContext?.Request.Host.Value.ToLower()
                ?? throw new HttpRequestException("Http context accessor is not initialized.");

            TenantDto tenant = appConfiguration.GetTenant(hostname);

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
}

