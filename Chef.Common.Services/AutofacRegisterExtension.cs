using System;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Chef.Common.Core.Logging;
using Chef.Common.Repositories;

namespace Chef.Common.Services.Extensions
{
	public static class AutofacRegisterExtension
	{
        public static void RegisterCommonServices(this ContainerBuilder builder)
        {
            //Register Services
            //TODO Properties are autowired for the timebeing.
            builder.RegisterAssemblyTypes(typeof(ICommonBranchService).Assembly)
                 .Where(t => t.IsAssignableTo<IBaseService>())
                 .AsImplementedInterfaces()
                 .InstancePerLifetimeScope()
                 .EnableInterfaceInterceptors()
                 .InterceptedBy(typeof(LoggingInterceptor))
                 .PropertiesAutowired();
        }
    }
}

