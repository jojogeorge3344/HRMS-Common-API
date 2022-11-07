using System.Net.Http;

namespace Chef.Common.Core;

public interface IRefitConfiguration
{
    void Configure(string module, ref HttpClient client);
}

