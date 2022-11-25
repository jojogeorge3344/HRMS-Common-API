namespace Chef.Finance.Integration;

public static class AutofacRegisterExtension
{
    public static void RegisterFinanceIntegration(this ContainerBuilder builder)
    {
        //Register Services
        //TODO Properties are autowired for the timebeing.
        builder.RegisterAssemblyTypes(typeof(AutofacRegisterExtension).Assembly)
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
        builder.RegisterAssemblyTypes(typeof(AutofacRegisterExtension).Assembly)
            .Where(t => t.IsAssignableTo<IRepository>())
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(LoggingInterceptor))
            .PropertiesAutowired();
    }
}

