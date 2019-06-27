using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Alsahab.Setting.WebFramework.Middlewares;
using Alsahab.Setting.WebFramework.Configuration;
using Alsahab.Common;
using Alsahab.Setting.WebFramework.CustomMapping;
using Alsahab.Setting.WebFramework.Swagger;
using Alsahab.Setting.BL;

namespace Alsahab.Setting.MyAPI
{
    public class Startup
    {
        private readonly SiteSettings _siteSettings;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            AutoMapperConfiguration.InitializeAutoMapper();

            _siteSettings = Configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // اگر از اتوفک استفاده می‌کنیم باید خروجی این تابع را از 
        // void
        // تغییر دهیم و متناسب با آن در پایان تابع مقدار لازم را برگردانیم
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //با این کار تنظیمات سایت در سازنده همه پروژه‌های برنامه قابل دسترسی است
            // برای مثال JwtService را ببینید
            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));

            services.AddDbContext(Configuration);

            services.AddMinimalMVC();

            // اگر از اتوفک استفاده می‌کنیم، می‌توانیم سرویس‌ها را در آن‌جا اضافه کنیم (اختیاری است)
            // services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            // services.AddScoped<IUserRepository, UserRepository>();
            // services.AddScoped<IJwtService, JwtService>();

            // to be added
            // services.AddCustomIdentity(_siteSettings.IdentitySettings);

            // to be added
            // services.AddJwtAuthentication(_siteSettings.JwtSettings);
            // services.AddScoped(typeof(IBranchBL), typeof(BranchBL));

            services.AddCustomApiVersioning();

            services.AddSwagger();

            //for autofac
            return services.BuildAutofacServiceProvider();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCustomExceptionHandler();

            app.UseHsts(env);

            app.UseHttpsRedirection();

            app.UseSwaggerAndUI();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
