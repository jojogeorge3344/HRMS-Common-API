using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;

namespace Chef.Common.Authentication.Extensions;

public static class AutofacRegisterExtension
{
    public static void RegisterAuthentication(this ContainerBuilder builder)
    {
        builder.RegisterType<AuthenticationRepository>().As<IAuthenticationRepository>().InstancePerLifetimeScope();

        //Configure automapper
        builder.RegisterAutoMapper(typeof(MappingProfile).Assembly);
    }
}

