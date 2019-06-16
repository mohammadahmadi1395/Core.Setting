using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace His.Reception.DAL.Context
{
   public class ApplicationDbContextFactory: IDesignTimeDbContextFactory<ApplicationDbContext>
   {
       public ApplicationDbContext CreateDbContext(string[] args)
       {
            // var basePath = Directory.GetCurrentDirectory();
            // Console.WriteLine($"Using `{basePath}` as the BasePath");
            // var configuration = new ConfigurationBuilder().SetBasePath(basePath).AddJsonFile("appsettings.json").Build();
            // var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //var connectionString = configuration.GetConnectionString("DefaultConnection");
            // builder.UseSqlServer(connectionString,opt=>opt.MigrationsHistoryTable("__CertMigrationsHistory", "cert"));
            // return new ApplicationDbContext(builder.Options);
            return null;
       }
   }
}
