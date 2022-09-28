using System;
using Microsoft.Extensions.DependencyInjection;

namespace Chef.Common.Core.Extensions
{
	public static class SwaggerExtension
	{
        public static void ConfigureSwagger(
            this IServiceCollection services,
            string version,
            string title,
            string description,
            string terms)
        {
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "ToDo API";
                    document.Info.Description = "A simple ASP.NET Core web API";
                    document.Info.TermsOfService = "None";
                };
            });
        }
    }
}

