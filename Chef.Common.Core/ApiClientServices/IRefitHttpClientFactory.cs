using System;

namespace Chef.Common.Core
{
	public interface IRefitHttpClientFactory<T>
	{
        T CreateClient(string baseAddressKey);
    }
}

