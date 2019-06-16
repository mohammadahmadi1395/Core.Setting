using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using His.Reception.Infrastructure;
using System.Text;
using System.Threading.Tasks;
using His.Reception.Application.Interface;
using His.Reception.Application.Interface.Base;
using His.Reception.Application.Service;
using His.Reception.Application.Service.Base;
using His.Reception.DAL.Context;
using His.Reception.DTO.User;
using His.Reception.Infrastructure.Caching;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using FluentValidation.AspNetCore;
using FluentValidation;
using His.Reception.Application.Validation;

namespace His.Reception.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }).AddFluentValidation();

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            // services.AddTransient < IValidator<Person>, PersonValida
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            RegisterService(services);

            //services.AddSwaggerGen(c => {
            //    c.SwaggerDoc("v1", new Info { Title = "You api title", Version = "v1" });
            //    c.AddSecurityDefinition("Bearer",
            //        new ApiKeyScheme
            //        {
            //            In = "header",
            //            Description = "Please enter into field the word 'Bearer' following by space and JWT",
            //            Name = "Authorization",
            //            Type = "apiKey"
            //        });
            //    c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
            //    { "Bearer", Enumerable.Empty<string>() },{ "SectionId", Enumerable.Empty<string>() },
            //});

            //});

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "You api title", Version = "v1" });

                //c.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                //{
                //    Description = "Authorization header using the Bearer scheme",
                //    Name = "Authorization",
                //    In = "header"
                //});

                //c.DocumentFilter<SwaggerSecurityRequirementsDocumentFilter>();
                //c.DocumentFilter<SwaggerSecurityRequirementsDocumentFilter>();
                //  c.OperationFilter<SwaggerSecurityRequirementsDocumentFilter>();
                c.OperationFilter<AddRequiredHeaderParameter>();
            });

            ConfigureBearerTokenAuthentication(services);

            services.Configure<JwtConfigDto>(Configuration.GetSection("jwt"));

            services.AddDistributedRedisCache(options =>
            {
                options.InstanceName = "Recption";
                options.Configuration = "localhost:6379";
            });
            
            services.AddLocalization(options => options.ResourcesPath = "Resources");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("MyPolicy");

            #region lang

            app.UseStaticFiles();

            var supportedCultures = new List<CultureInfo>
                                {
                                    new CultureInfo("en-us"),
                                    new CultureInfo("fa-IR"),
                                    new CultureInfo("ar-SA"),
                                    new CultureInfo("sp"),
                                };

            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("fr"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
            };

            app.UseRequestLocalization(options);

            #endregion
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void ConfigureBearerTokenAuthentication(IServiceCollection services)
        {           
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["jwt:JwtIssuer"],
                        ValidAudience = Configuration["jwt:JwtIssuer"],
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwt:JwtKey"])),
                        RequireExpirationTime = true
                    };
                });
        }

        public void RegisterService(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, ApplicationDbContext>();
            services.AddScoped<IRedisCacheService, RedisCacheService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IUserManagerService, UserManagerService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IReceptionsService, ReceptionsService>();

            #region Service Base

            services.AddScoped<IBloodGroupService, BloodGroupService>();
            services.AddScoped<IBirthPlaceService, BirthPlaceService>();
            services.AddScoped<IEducationService, EducationService>();
            services.AddScoped<IGeneralStatusService, GeneralStatusService>();
            services.AddScoped<IIllnessService, IllnessService>();
            services.AddScoped<IIssuePlaceService, IssuePlaceService>();
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<IMaritalStatusService, MaritalStatusService>();
            services.AddScoped<IReceptionTypeService, ReceptionTypeService>();
            services.AddScoped<ISectionService, SectionService>();
            services.AddScoped<IRhService, RhService>();
            services.AddScoped<IRegionalService, RegionalService>();
            services.AddScoped<ISexService, SexService>();
            services.AddScoped<IRefferReasonService, RefferReasonService>();
            services.AddScoped<ISpecialIllnessService, SpecialIllnessService>();
            services.AddScoped<IAllergyService, AllergyService>();
            services.AddScoped<IRefferFromService, RefferFromService>();
            services.AddScoped<IPresenterService, PresenterService>();

            #endregion
        }

        public class SwaggerSecurityRequirementsDocumentFilter : IDocumentFilter
        {
            public void Apply(SwaggerDocument document, DocumentFilterContext context)
            {
                document.Security = new List<IDictionary<string, IEnumerable<string>>>()
            {
                new Dictionary<string, IEnumerable<string>>()
                {
                    { "Bearer", new string[]{ } },
                    { "Basic", new string[]{ } },
                }
            };

               // document.Parameters.Add()
            }
        }

        public class AddRequiredHeaderParameter : IOperationFilter
        {
            public void Apply(Operation operation, OperationFilterContext context)
            {
                if (operation.Parameters == null)
                    operation.Parameters = new List<IParameter>();

                operation.Parameters.Add(new NonBodyParameter
                {
                    Name = "Authorization",
                    In = "header",
                    Type = "string",
                    Required = false
                });

                operation.Parameters.Add(new NonBodyParameter
                {
                    Name = "SectionId",
                    In = "header",
                    Type = "string",
                    Required = false
                });

                operation.Parameters.Add(new NonBodyParameter
                {
                    Name = "Accept-Language",
                    In = "header",
                    Type = "string",
                    Required = false
                });
            }
        }

    }
}
