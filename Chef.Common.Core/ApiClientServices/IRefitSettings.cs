using System;
using System.Net.Http;
using Chef.Common.Models;

namespace Chef.Common.Core
{
	public interface IRefitSettings
	{
		void Configure(string module, ref HttpClient client);
	}
}

