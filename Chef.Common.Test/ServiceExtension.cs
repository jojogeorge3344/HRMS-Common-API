using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Chef.Common.Test
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddRestServiceClient(this IServiceCollection services, Type type, Action<IServiceProvider, HttpClient> clientFactory)
        {
            services.AddHttpClient(type.FullName, clientFactory)
                .Services.AddTransient(type, services =>
                {
                    var httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
                    var httpClient = httpClientFactory.CreateClient(type.FullName);
                    return Refit.RestService.For(type, httpClient);
                });

            return services;
        }

        public static IServiceCollection AddRestServiceClient(this IServiceCollection services, Type type, Action<HttpClient> clientFactory)
        {
            return AddRestServiceClient(services, type, (serviceProvider, client) => clientFactory(client));
        }
    }
}
