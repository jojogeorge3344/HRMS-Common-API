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
                    document.Info.Version = version;
                    document.Info.Title = title;
                    document.Info.Description = description;
                    document.Info.TermsOfService = terms;
                };
            });
        }
    }
}

