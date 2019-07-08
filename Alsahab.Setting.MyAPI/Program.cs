using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace Alsahab.Setting.MyAPI
{
    /// <summary>
    /// program class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// main method
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            //Set default proxy
            // WebRequest.DefaultWebProxy = new WebProxy("http://127.0.0.1/8118", true)
            // {
            //     UseDefaultCredentials = true,
            // };

            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("init main");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch(Exception ex)
            {
                //NLog: catch setup errors
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                //Ensure to flush and stop internal timers / threads before application exit (Avoid segmentaion fault)
                NLog.LogManager.Shutdown();
            }
        }

        /// <summary>
        /// configuration of nlog file
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(options=>options.ClearProviders())
                .UseNLog()
                .UseStartup<Startup>();
    }
}
