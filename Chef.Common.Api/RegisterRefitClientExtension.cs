using Chef.Common.Core;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Chef.Common.Api
{
    public static class RegisterRefitClientExtension
	{
        public static void AddRefitClientForConsole(this IServiceCollection services)
        {
            services.AddRefitClient<IConsoleApi>().ConfigureHttpClient(ConfigureHttpClientForConsole);
        }

        public static void AddRefitClientForFinance(this IServiceCollection services)
        {
            services.AddRefitClient<IFinanceApi>().ConfigureHttpClient(ConfigureHttpClientForFinance);
        }

        public static void AddRefitClientForApproval(this IServiceCollection services)
        {
            services.AddRefitClient<IApprovalApi>().ConfigureHttpClient(ConfigureHttpClientForApproval);
        }

        public static void AddRefitClientForDms(this IServiceCollection services)
        {
            services.AddRefitClient<IDmsApi>().ConfigureHttpClient(ConfigureHttpClientForDms);
        }

        private static void ConfigureHttpClientForConsole(IServiceProvider sp, HttpClient client)
        {
            sp.GetRequiredService<IRefitConfiguration>().Configure("Console", ref client);
        }

        private static void ConfigureHttpClientForFinance(IServiceProvider sp, HttpClient client)
        {
            sp.GetRequiredService<IRefitConfiguration>().Configure("Finance", ref client);
        }

        private static void ConfigureHttpClientForApproval(IServiceProvider sp, HttpClient client)
        {
            sp.GetRequiredService<IRefitConfiguration>().Configure("Approval", ref client);
        }

        private static void ConfigureHttpClientForDms(IServiceProvider sp, HttpClient client)
        {
            sp.GetRequiredService<IRefitConfiguration>().Configure("DMS", ref client);
        }

        private static void ConfigureHttpClientForAdmin(IServiceProvider sp, HttpClient client)
        {
            sp.GetRequiredService<IRefitConfiguration>().Configure("Admin", ref client);
        }
    }
}

