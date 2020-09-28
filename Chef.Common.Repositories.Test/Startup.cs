using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using Autofac.Extensions.DependencyInjection;
using Chef.Common.ClientServices;
using Chef.Common.Services;
using Chef.Common.Test;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Chef.Common.Repositories.Test
{
    public class Startup
    {

        public void ConfigureHost(IHostBuilder hostBuilder) =>
            hostBuilder
                .ConfigureHostConfiguration(builder => { })
        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterModule<InjectPropertiesByDefaultModule>();

                //builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();

            })
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.AddJsonFile("appsettings.json", optional: false);
                });
        public void ConfigureServices(IServiceCollection services, HostBuilderContext context)
        {
            services.AddSingleton<IConfiguration>(context.Configuration);
            services.AddSingleton<ITestConfiguration, TestConfiguration>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IApiClientServiceFactory, Common.Test.ApiClientServiceFactory>();
            services.AddScoped<IConnectionFactory, Common.Test.ConnectionFactory>();
            services.AddScoped<IDatabaseSession, DatabaseSession>();
            services.AddScoped<IRepositoryFactory, RepositoryFactory>();
            services.AddScoped<IQueryBuilderFactory, QueryBuilderFactory>();
            services.AddScoped<IServiceFactory, ServiceFactory>();

            //services.Scan(scan => scan.FromAssemblyOf<Chef.Trading.Repositories.IAssemblyMarker>()
            //    .AddClasses()
            //    .AsImplementedInterfaces()
            //    .WithScopedLifetime());

            //services.Scan(scan => scan.FromAssemblyOf<Chef.Trading.Services.IAssemblyMarker>()
            //    .AddClasses()
            //    .AsImplementedInterfaces()
            //    .WithScopedLifetime());


            //var mapperConfig = new MapperConfiguration(mc =>
            //{
            //    mc.AddProfile(new Chef.Trading.Dtos.MappingProfile());
            //});

            //IMapper mapper = mapperConfig.CreateMapper();
            //services.AddSingleton(mapper);
        }

    }
    class InjectPropertiesByDefaultModule : Autofac.Module
    {
        protected override void AttachToComponentRegistration(IComponentRegistryBuilder componentRegistry, IComponentRegistration registration)
        {
            registration.Activating += (s, e) =>
            {
                e.Context.InjectProperties(e.Instance);
            };
            base.AttachToComponentRegistration(componentRegistry, registration);
        }
    }
}
