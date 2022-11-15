using Autofac;
using Chef.Common.ClientServices;
using Chef.Common.Core.Logging;
using Chef.Common.Repositories;
using Chef.Common.Services;

namespace Chef.Common.Core.Extensions
{
    public static class AutofacRegisterExtensions
	{
        public static void RegisterConsoleDBComponents(this ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleConnectionFactory>().As<IConsoleConnectionFactory>().InstancePerLifetimeScope();
            builder.RegisterType<ConsoleSimpleUnitOfWork>().As<IConsoleSimpleUnitOfWork>().InstancePerLifetimeScope();
        }

        public static void RegisterTenantDBComponents(this ContainerBuilder builder)
        {
            builder.RegisterType<TenantConnectionFactory>().As<ITenantConnectionFactory>().InstancePerLifetimeScope();
            builder.RegisterType<TenantConnectionFactory>().As<ITenantConnectionFactory>().InstancePerLifetimeScope();
            builder.RegisterType<TenantSimpleUnitOfWork>().As<ITenantSimpleUnitOfWork>().InstancePerLifetimeScope();
        }

        public static void RegisterFrameworkComponents(this ContainerBuilder builder)
        {
            //Register Interceptor
            builder.RegisterType<LoggingInterceptor>();

            //register generic services
            builder.RegisterType<ServiceSession>().As<IServiceSession>().InstancePerLifetimeScope();

            //register repository factory
            builder.RegisterType<RepositoryFactory>().As<IRepositoryFactory>().InstancePerLifetimeScope();

            //Register Redis
            builder.RegisterType<RedisConnectionFactory>().As<IRedisConnectionFactory>().InstancePerLifetimeScope();

            //transient email factory
            builder.RegisterType<EmailSendFactory>().As<IEmailSendFactory>().InstancePerDependency();

            //app configuration
            builder.RegisterType<TenantProvider>().As<ITenantProvider>().InstancePerLifetimeScope();
        }

        public static void RegisterApiServiceComponents(this ContainerBuilder builder)
        {
            builder.RegisterType<ApiClientServiceFactory>().As<IApiClientServiceFactory>().InstancePerLifetimeScope();
            builder.RegisterType<RefitConfiguration>().As<IRefitConfiguration>().InstancePerLifetimeScope();
        }
    }
}

