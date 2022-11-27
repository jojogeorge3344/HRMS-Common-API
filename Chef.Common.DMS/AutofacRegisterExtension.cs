namespace Chef.Common.DMS;

public static class AutofacRegisterExtension
{
    public static void RegisterDMS(this ContainerBuilder builder)
    {
        //Register dms System Services
        //TODO Properties are autowired for the timebeing.
        builder.RegisterAssemblyTypes(typeof(ZooKeeperService).Assembly)
             .Where(t => t.IsAssignableTo<IBaseService>())
             .AsImplementedInterfaces()
             .InstancePerLifetimeScope()
             .EnableInterfaceInterceptors()
             .InterceptedBy(typeof(LoggingInterceptor))
             .PropertiesAutowired();

        //Register Approval System Repositories
        //Repos will have the Logging Interceptor
        //TODO Properties are autowired for the timebeing.
        //We can avoid the property injection.
        builder.RegisterAssemblyTypes(typeof(ZooKeeperRepository).Assembly)
            .Where(t => t.IsAssignableTo<IRepository>())
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(LoggingInterceptor))
        .PropertiesAutowired();
    }

    public static void ConfigureDMS(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<StorageOptions>(configuration.GetSection(StorageOptions.FileStorage));
    }
}