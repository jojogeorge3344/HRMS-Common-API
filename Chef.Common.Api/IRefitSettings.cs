using System;
using Chef.Common.Models;

namespace Chef.Common.Api
{
	public interface IRefitSettings
	{
		void Configure(string module, ref HttpClient client);
	}
}

