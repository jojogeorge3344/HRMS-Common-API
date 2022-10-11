using Chef.Common.Exceptions;
using Chef.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Chef.Common.ClientServices
{
    public class ApiClientServiceFactory : IApiClientServiceFactory
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;
        private readonly ConcurrentDictionary<string, HttpClient> httpClients;

        public ApiClientServiceFactory(
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            this.httpContextAccessor = httpContextAccessor;
            httpClients = new ConcurrentDictionary<string, HttpClient>();
            this.configuration = configuration;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            foreach (KeyValuePair<string, HttpClient> httpClient in httpClients)
            {
                httpClient.Value.Dispose();
            }
        }

        public HttpClient CreateClient(string name)
        {
            if (httpClients.TryGetValue(name, out HttpClient client))
            {
                return client;
            }

            string hostname = httpContextAccessor.HttpContext.Request.Host.Value.ToLower();

            Tenant tenant = configuration.GetSection("Tenants").Get<List<Tenant>>()
               .Where(x => x.Host == hostname).FirstOrDefault();

            if (tenant == null)
            {
                throw new TenantNotFoundException(hostname + " not configured properly.");
            }

            ApiClient apiclient = tenant.ApiClients
                .Where(x => x.Name == name).FirstOrDefault();

            if (apiclient == null)
            {
                throw new Exception(string.Format("Tenant/ApiClients name - {0} not configured properly.", name));
            }

            HttpClientHandler handler = new()
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, sslErrors) => true
            };

            client = new HttpClient(handler)
            {
                BaseAddress = new Uri(apiclient.BaseAddress),
            };

            httpClients.TryAdd(name, client);

            return client;
        }
    }
}
