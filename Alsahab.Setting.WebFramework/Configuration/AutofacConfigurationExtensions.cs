using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Alsahab.Setting.Common;
using Alsahab.Setting.Data;
using Alsahab.Setting.Entities;
using Alsahab.Setting.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using Alsahab.Setting.Data.Contracts;
using Alsahab.Setting.Data.Repositories;
using Alsahab.Setting.BL;
using Alsahab.Setting.BL.Services;
using Alsahab.Setting.DTO;
// using Alyatim.Member.SC;
// using Alyatim.Member.DTO;
// using UserManagement.SC;
// using UserManagement.DTO;

namespace Alsahab.Setting.WebFramework.Configuration
{
    public static class AutofacConfigurationExtensions
    {
        public static void AddServices(this ContainerBuilder builder)
        {
            //RegisterType , Contract , Lifetime
            // services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            // services.AddScoped<IUserRepository, UserRepository>();
            // services.AddScoped<IJwtService, JwtService>();

            // builder.RegisterType<JwtService>().As<IJwtService>().InstancePerLifetimeScope();
            // builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            
            // builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(BaseDL<,,>)).As(typeof(IBaseDL<,,>)).InstancePerLifetimeScope();

            //قابلیت‌هایی که اتوفک دارد، اما 
            // IOC
            // اتوماتیک دات نت کور ندارد:
            // PropertyInjection
            // AssemblyScanning + Auto/Conventional Registration
            // Interception

            // Assembly Scanning + Auto Registration
            var commonAssembly = typeof(SiteSettings).Assembly;
            var DataAssembly = typeof(ApplicationDbContext).Assembly;
            var EntityAssembly = typeof(IEntity).Assembly;
            // To be added
            // var serviceAssembly = typeof(JwtService).Assembly;
            var serviceAssembly = typeof(BaseBL<,,>).Assembly;
            var dtoAssembly = typeof(BaseDTO).Assembly;

            builder.RegisterAssemblyTypes(commonAssembly, DataAssembly, EntityAssembly, serviceAssembly, dtoAssembly)
                .AssignableTo<IScopedDependency>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(commonAssembly, DataAssembly, EntityAssembly, serviceAssembly, dtoAssembly)
                .AssignableTo<ITransientDependency>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(commonAssembly, DataAssembly, EntityAssembly, serviceAssembly, dtoAssembly)
                .AssignableTo<ISingletonDependency>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
        public static IServiceProvider BuildAutofacServiceProvider(this IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();
            //همه سرویس‌هایی که از قبل وجود داشته‌اند را به 
            // containerBuilder
            // اضافه می‌کند
            containerBuilder.Populate(services);
            // می‌توان سرویس‌های بیشتری را هم در این قسمت به 
            // ContainerBuilder
            // اضافه کرد
            //Register Services to Autofac ContainerBuilder
            containerBuilder.AddServices();

            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }
    }
}