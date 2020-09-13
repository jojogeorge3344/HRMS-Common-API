using Chef.Common.ClientServices;
using Chef.Common.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Chef.Common.Test
{
    public class ApiClientServiceFactory : IApiClientServiceFactory
    {
        readonly IConfiguration configuration;
        readonly ConcurrentDictionary<string, HttpClient> httpClients;

        public ApiClientServiceFactory(IConfiguration configuration)
        {
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
            var unittesttenantname = configuration.GetValue<string>("UnitTestTenantName");
            var tenant = configuration.GetSection("Tenants").Get<List<Tenant>>()
                .Where(t => t.Name.ToLower().Equals(unittesttenantname.ToLower())).FirstOrDefault();

            var apiclient = tenant.ApiClients
                .Where(x => x.Name == name).FirstOrDefault();

            client = new HttpClient
            {
                BaseAddress = new Uri(apiclient.BaseAddress),
            };

            this.httpClients.TryAdd(name, client);

            return client;
        }
    }
}
