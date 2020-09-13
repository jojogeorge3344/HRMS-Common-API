using System;
using System.Net.Http;

namespace Chef.Common.ClientServices
{
    public interface IApiClientServiceFactory : IHttpClientFactory, IDisposable
    {
    }
}
