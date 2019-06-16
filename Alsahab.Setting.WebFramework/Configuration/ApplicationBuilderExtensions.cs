using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Alsahab.Setting.WebFramework.Configuration
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseHsts(this IApplicationBuilder app, IHostingEnvironment env)
        {
            if (! env.IsDevelopment())
                app.UseHsts();
        }
    }
}