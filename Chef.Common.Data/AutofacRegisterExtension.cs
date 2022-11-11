using Autofac;
using Autofac.Extras.DynamicProxy;
using Chef.Common.Core.Logging;
using Chef.Common.Data.Repositories;
using Chef.Common.Data.Services;
using Chef.Common.Repositories;
using Chef.Common.Services;

namespace Chef.Common.Data;

public static class AutofacRegisterExtension
{
    public static void RegisterDataServices(this ContainerBuilder builder)
    {
        //register console connection
        builder.RegisterType<ConsoleConnectionFactory>().As<IConsoleConnectionFactory>().InstancePerLifetimeScope();

        //Register Services
        //TODO Properties are autowired for the timebeing.
        builder.RegisterAssemblyTypes(typeof(IMasterDataService).Assembly)
            .Where(t => t.IsAssignableTo<IBaseService>())
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(LoggingInterceptor))
            .PropertiesAutowired();

        //Register Repositories
        //Repos will have the Logging Interceptor
        //TODO Properties are autowired for the timebeing.
        //We can avoid the property injection.
        builder.RegisterAssemblyTypes(typeof(IMasterDataRepository).Assembly)
            .Where(t => t.IsAssignableTo<IRepository>())
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(LoggingInterceptor))
            .PropertiesAutowired();
    }
}
