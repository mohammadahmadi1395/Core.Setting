using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Alsahab.Setting.Common.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using WebFramework.Swagger;

namespace Alsahab.Setting.WebFramework.Swagger
{
    public static class SwaggerConfigurationExtensions
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            Assert.NotNull(services, nameof(services));

            //Add services to use example filters in swagger
            services.AddSwaggerExamples();
            
            //Add services and configuration to use swagger
            services.AddSwaggerGen(options =>
            {
                //داکیومنتهایی که با /// در ابتدای هر اکشنی وجود دارند را نمایش می‌دهد
                var xmlDocPath = Path.Combine(AppContext.BaseDirectory, "MyApi.xml");
                // //show controller XML comments like summary
                options.IncludeXmlComments(xmlDocPath, true);

                // فعال کردن انوتیشن‌ها
                options.EnableAnnotations();
                // متن رشته‌ای اینام‌ها را نمایش می‌دهد
                options.DescribeAllEnumsAsStrings();
                // همه پارامترها را به صورت کمل کیس نمایش می‌دهد
                // options.DescribeAllParametersInCamelCase();
                // همه رشته‌های اینام‌ها را به صورت کمل کیس نمایش می‌دهد
                // options.DescribeStringEnumsInCamelCase();
                // 
                // options.UseReferencedDefinitionsForEnums();
                // // نادیده گرفتن اکشن‌ها و پراپرتی‌های منسوخ شده
                // options.IgnoreObsoleteActions();
                // options.IgnoreObsoleteProperties();

                options.SwaggerDoc("v1", new Info { Version = "v1", Title = "API V1" });
                options.SwaggerDoc("v2", new Info { Version = "v2", Title = "API V2" });

                // #region Filters
                // //Enable to use [SwaggerRequestExample] && [SwaggerResponseExample]
                // امکان نمایش مثال‌های درخواست و پاسخ
                // مثال در یوزرکنترلر، ایجاد کاربر جدید ورژن اول
                options.ExampleFilters();

                //Adds an upload button to endpoints which have [AddSwaggerFileUploadButton]
                // برای کار کردن دستور زیر باید دستور 
                // (Services.AddSwaggerExamples();)
                // نوشته شده باشد.
                // باعث میشود که هر اکشنی که بالای سر آن انوتیشن
                // [AddFileParamTypesOperationFilter]
                // وجود داشته باشد، یک گزینه آپلود هم به آن اکشن افزوده شود
                options.OperationFilter<AddFileParamTypesOperationFilter>();

                //Set summary of action if not already set
                // کامنتهای بالای هر اکشن را به صورت اتوماتیک خودش تولید می‌کند
                options.OperationFilter<ApplySummariesOperationFilter>();


                #region Add Jwt Authentication
                //تعریف امنیتی: نام و شمای سیستم احراز هویت را مشخص می‌کند
                // در این جا نام سیستم احراز هویت را مشخص می‌کنیم و اعلام می‌کنیم که توکن باید در کلید آوتورایزیشن هدر درخواست وجود داشته باشد
                // با افزودن این قسمت در شمال شرقی صفحه سوگر یک قفل باز می‌شود که می‌توان مشخص توکن را در آن وارد کرد
                // options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                // {
                //     Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer",
                //     Name = "Authorization",
                //     In = "header",
                // });
                
                //کنار هر اکشنی یک قفل برای احراز هویت نشان داده می‌شود
                // options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                // {
                //     {"Bearer", new string[] {}}
                // });
                // ولی با توجه به این که همه اکشن‌ها نیاز به احراز هویت ندارند، بهتر است از گزینه بالا استفاده نکنیم و به جای آن از آپشن پایین استفاده کنیم که تنها قفل را برای اکشن‌هایی که نیاز به احراز هویت دارند، نمایش می‌دهد
                // این گزینه علاوه بر آن، مشخص می‌کند که اکشن چه مجموعه حالاتی را برمی‌گرداند (مثلا 200 یا 401 یا 403 یا ...) در صورت عدم احراز هویت (پاذرامتر اول اگر ترو (صحیح) باشد این کار را انجام می‌دهد). و
                // #region Add Unahthorized to Response                
                //Add 401 response and security requirements (lock icon) to actions that need authorization
                options.OperationFilter<UnauthorizedResponsesOperationFilter>(false, "Bearer");
                // #endregion

                // در حالت عادی، ما باید توکن را دریافت کنیم (مثلا در لاگین) و بعد آن توکن را در هدر همه درخواست‌ها کپی کنیم و یا حداقل یک بار در بخش امنیتی آن را وارد کنیم
                // آپشن زیر کاری می‌کند که هنگام تولید توکن به صورت خودکار در بخش امنیتی توکن قرار گیرد و نیازی به کپی دستی نیست
                // این کار به کمک روش زیر انجام می‌شود که در آن نام کاربری و کلمه عبور از کاربر گرفته می‌شود و بعد به صورت خودکار در سوگر ذخیره می‌شود.
                // برای استفاده از این بخش باید تعریف امنیتی قبلی را برداریم
                // همچنین به دلیل این که این روش،‌اطلاعات را از طریق فرم می‌فرستد، باید یک کار اضافی هم انجام دهیم
                options.AddSecurityDefinition("Bearer", new OAuth2Scheme
                {
                    Flow = "password",
                    TokenUrl = "https://localhost:44322/api/v1/User/Token"
                    // TokenUrl = "http://localhost:53507/api/v1/User/Token"
                });
                #endregion                

                #region Versioning
                //Remove version parameter from all operations
                // پارامتر ورژن را از ورودی هر اکشن برمی‌دارد
                options.OperationFilter<RemoveVersionParameters>();

                //Set version "api/v{version}/[controller]" from current swagger doc version
                // پارامتر ورژن را از ورژن داکیومنت (شمال شرقی صفحه سوگر) می‌خواند
                options.DocumentFilter<SetVersionInPaths>();

                //Seperate and categorize endpoints by doc version
                // حذف پارامتر ورژن از یوآرال
                // با استفاده از رفلکشن همه متدها و اتریبیوت‌ها را به دست می‌آورد و اگر شماره ورژن آنها با شماره ورژن داکیومنت مطابقت داشت، همان را محاسبه می‌کند
                options.DocInclusionPredicate((docName, apiDesc) =>
                {
                    if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

                    var versions = methodInfo.DeclaringType
                        .GetCustomAttributes<ApiVersionAttribute>(true)
                        .SelectMany(attr => attr.Versions);

                    return versions.Any(v => $"v{v.ToString()}" == docName);
                });
                #endregion

                // // If use FluentValidation then must be use this package to show validation in swagger (MicroElement 
                // // options.AddFluentValidationRules();
                // #endregion
            });
        }

        public static void UseSwaggerAndUI(this IApplicationBuilder app)
        {
            Assert.NotNull(app, nameof(app));

            //Swagger middleware for generate "Open API Documentation" in swagger.json
            app.UseSwagger(/*options =>
            {
                options.RouteTemplate = "api-docs/{documentName}/swagger.json";
            }*/);

            //Swagger middelware for generate UI from swagger.json
            app.UseSwaggerUI(options =>
            {
                #region Customizing
                //Display
                // باعث میشود که همه اکشن‌ها به صورت پیشفرض کولپس شده باشند
                options.DocExpansion(DocExpansion.None);

                // options.DefaultModelExpandDepth(2);
                // options.DefaultModelRendering(ModelRendering.Model);
                // options.DefaultModelsExpandDepth(-1);
                // options.DisplayOperationId();
                // options.DisplayRequestDuration();
                // options.EnableDeepLinking();
                // options.EnableFilter();
                // options.MaxDisplayedTags(5);
                // options.ShowExtensions();

                // //Network
                // options.EnableValidator();
                // options.SupportedSubmitMethods(SubmitMethod.Get);

                // //Other
                // options.DocumentTitle = "CustomUIConfig";
                // options.InjectStylesheet("/ext/custom-stylesheet.css");
                // options.InjectJavascript("/ext/custom-javascript.js");
                // options.RoutePrefix = "api-docs";
                //آدرس فایل راهنما یا اینفو را در این قسمت مشخص می‌کنیم
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "V2 Docs");
                #endregion
            });
        }
    }
}