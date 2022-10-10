using System;
using Refit;
using System.Net.Http;

namespace Chef.Common.Core
{
    public class RefitHttpClientFactory<T> : IRefitHttpClientFactory<T>
    {
        private readonly IHttpClientFactory _clientFactory;

        public RefitHttpClientFactory(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public T CreateClient(string baseAddressKey)
        {
            var client = _clientFactory.CreateClient(baseAddressKey);

            return RestService.For<T>(client);
        }
    }
}