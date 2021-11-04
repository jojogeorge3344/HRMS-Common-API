using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Chef.Common.Exceptions;
using Chef.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Chef.Common.ClientServices
{
    public class ApiClientServiceFactory : IApiClientServiceFactory
    {
        readonly IHttpContextAccessor httpContextAccessor;
        readonly IConfiguration configuration;
        readonly ConcurrentDictionary<string, HttpClient> httpClients;

        public ApiClientServiceFactory(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.httpClients = new ConcurrentDictionary<string, HttpClient>();
            this.configuration = configuration;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            foreach (var httpClient in this.httpClients)
            {
                httpClient.Value.Dispose();
            }
        }

        public HttpClient CreateClient(string name)
        {
            if (this.httpClients.TryGetValue(name, out var client))
                return client;

            var hostname = httpContextAccessor.HttpContext.Request.Host.Value.ToLower();

            var tenant = configuration.GetSection("Tenants").Get<List<Tenant>>()
               .Where(x => x.Host == hostname).FirstOrDefault();

            if (tenant == null)
                throw new TenantNotFoundException(hostname + " not configured properly.");

            var apiclient = tenant.ApiClients
                .Where(x => x.Name == name).FirstOrDefault();

            if (apiclient == null)
                throw new Exception(string.Format("Tenant/ApiClients name - {0} not configured properly.", name));

            client = new HttpClient
            {
                BaseAddress = new Uri(apiclient.BaseAddress),
            };

            this.httpClients.TryAdd(name, client);

            return client;
        }
    }
}
