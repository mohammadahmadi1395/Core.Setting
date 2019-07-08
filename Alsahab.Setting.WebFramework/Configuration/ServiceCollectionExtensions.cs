using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Alsahab.Setting.DL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using FluentValidation.AspNetCore;
using Alsahab.Setting.WebFramework.Api;

namespace Alsahab.Setting.WebFramework.Configuration
{
    public static class ServiceCollectionExtensions
    {

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                // options.UseSqlServer("Data Source=.;Initial Catalog=MyApiDb;IntegratedSecurity=true");
                options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
            });
        }

        public static void AddMinimalMVC(this IServiceCollection services)
        {
            // در این جا به جای آن که همه سرویس‌های ام وی سی را اضافه کنیم، فقط سرویس‌های مهم و پرکاربرد را اضافه می‌کنیم
            //https://github.com/aspnet/Mvc/blob/release/2.2/src/Microsoft.AspNetCore.Mvc/MvcServiceCollectionExtensions.cs

            services.AddMvcCore(options =>
            {
                //خط کد زیر باعث می‌شود که همه اکشن ها حتما باید مجوز دسترسی داشته باشند و نیازی به 
                // [Authorize]
                // بالای هر اکشن نداشته باشیم
                options.Filters.Add(new AuthorizeFilter());
            })
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<EmptyResult>())
            .AddApiExplorer()
            .AddAuthorization()
            .AddFormatterMappings()
            .AddDataAnnotations()
            .AddJsonFormatters(/*options=>
            {
                options.Formatting = Newtonsoft.Json.Formatting.Indented;
                options.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            }*/
            )
            .AddCors()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

        }

        // To be added
        // public static void AddJwtAuthentication(this IServiceCollection services, JwtSettings settings)
        // {
        //     services.AddAuthentication(options =>
        //     {
        //         options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //         options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        //         options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //     }).AddJwtBearer(options =>
        //     {
        //         //مقدار این کلید باید با مقدار کلیدی که در سرویس 
        //         // JwtService
        //         // وارد شده است، یکسان باشد
        //         var secretKey = Encoding.UTF8.GetBytes(settings.SecretKey);
        //         var encryptionKey = Encoding.UTF8.GetBytes(settings.EncryptKey);
        //         var validationParameters = new TokenValidationParameters
        //         {
        //             ClockSkew = TimeSpan.Zero,
        //             RequireSignedTokens = true,

        //             ValidateIssuerSigningKey = true,
        //             IssuerSigningKey = new SymmetricSecurityKey(secretKey),

        //             RequireExpirationTime = true,
        //             ValidateLifetime = true,

        //             ValidateAudience = true,
        //             ValidAudience = settings.Audience,

        //             ValidateIssuer = true,
        //             ValidIssuer = settings.Issuer,

        //             TokenDecryptionKey = new SymmetricSecurityKey(encryptionKey) // for ewt
        //         };
        //         options.RequireHttpsMetadata = false;
        //         options.SaveToken = true;
        //         options.TokenValidationParameters = validationParameters;
        //         options.Events = new JwtBearerEvents
        //         {
        //             // بعد از آن که اعتبارسنجی توکن ناموفق باشد
        //             OnAuthenticationFailed = context =>
        //             {
        //                 // var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBeareEvents));
        //                 //logger.LogError("Authentication failed.", context.Exception);
        //                 if (context.Exception != null)
        //                     throw new AppException(Common.Api.ApiResultStatusCode.Unauthorized, "Authentication failed.", System.Net.HttpStatusCode.Unauthorized, context.Exception, null);
        //                 return Task.CompletedTask;
        //             },
        //             // زمانی که درخواست دارای توکن نباشد
        //             OnChallenge = context =>
        //             {
        //                 // var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
        //                 // logger.LogError("OnChallenge error", context.Error, context.ErrorDescription);
        //                 if (context.AuthenticateFailure != null)
        //                     throw new AppException(ApiResultStatusCode.Unauthorized, "Authenticate failure.", System.Net.HttpStatusCode.Unauthorized, context.AuthenticateFailure, null);
        //                 throw new AppException(ApiResultStatusCode.Unauthorized, "You are unauthorized to access this resource", System.Net.HttpStatusCode.Unauthorized);
        //             },
        //             // زمانی که یک درخواست دارای توکن، می‌رسد
        //             // OnMessageReceived = context =>
        //             // {
        //             //     return null;
        //             // },
        //             // زمانی که توکن اعتبارسنجی می‌شود
        //             OnTokenValidated = async context =>
        //             {
        //                 // By Identity
        //                 var applicationSignInManager = context.HttpContext.RequestServices.GetRequiredService<SignInManager<User>>();

        //                 var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
        //                 if (claimsIdentity.Claims?.Any() != true)
        //                     context.Fail("This token has no claims.");

        //                 var securityStamp = claimsIdentity.FindFirstValue(new ClaimsIdentityOptions().SecurityStampClaimType);
        //                 if (!securityStamp.HasValue())
        //                     context.Fail("This token has no security stamp");

        //                 //Find user and token from database and perform your custom validation
        //                 var userRepository = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
        //                 var userId = claimsIdentity.GetUserId<int>();
        //                 var user = await userRepository.GetByIdAsync(context.HttpContext.RequestAborted, userId);

        //                 // check security stamp by normal
        //                 // if (user.SecurityStamp != securityStamp)
        //                 //     context.Fail("Token security stamp is not valid.");

        //                 //check security stamp by identity
        //                 var validatedUser = await applicationSignInManager.ValidateSecurityStampAsync(context.Principal);
        //                 if (validatedUser == null)
        //                     context.Fail("Token security stamp is not valid");

        //                 if (!user.IsActive)
        //                     context.Fail("User is not active.");

        //                 await userRepository.UpdateLastLoginDateAsync(user, context.HttpContext.RequestAborted);
        //             },
        //         };
        //     });
        // }

        public static void AddCustomApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(Options=>
            {
                //زمانی که یک اکشن هیچ ورژنی را نداشته باشد
                Options.AssumeDefaultVersionWhenUnspecified = true;
                Options.DefaultApiVersion = new ApiVersion(1,0); // v1 = v1.0
                //نکته: برای مقایسه ورژنها می‌توان از دستور زیر استفاده کرد:
                // ApiVersion.TryParse("1.0", out var version10);
                // ApiVersion.TryParse("1", out var version1);
                // var a = version1 == version10;


                // 1. Options.ApiVersionReader = new QueryStringApiVersionReader("api-version"); // api/post?api-version=1
                // 2.
                Options.ApiVersionReader = new UrlSegmentApiVersionReader();
                // 3. Options.ApiVersionReader = new HeaderApiVersionReader("Api-Version"); // header => Api-Version : 1
                // 4. Options.ApiVersionReader = new MediaTypeApiVersionReader();
                // 5. Options.ApiVersionReader = ApiVersionReader.Combine(new QueryStringApiVersionReader("api-version"), new UrlSegmentApiVersionReader())

                //اعلام می‌کند که مثلا فلان ورژن منسوخ شده است و ...
                Options.ReportApiVersions = true;
            });
        }
    }
}