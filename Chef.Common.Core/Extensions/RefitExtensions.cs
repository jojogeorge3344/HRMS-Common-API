using System;
using Chef.Common.ClientServices;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace Chef.Common.Core
{
	public class RefitExtensions
	{
        public void ConfigureHttpClientForConsole(IServiceProvider sp, HttpClient client)
        {
            var config = sp.GetRequiredService<IApiClientServiceFactory>().CreateClient("Console");
            client.BaseAddress = config.BaseAddress;
        }
    }
}

