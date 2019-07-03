using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Common.Utilities;
using Alsahab.Setting.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Alsahab.Setting.Data
{
    // نکته: اگر 
    //User,Role, int
    // را وارد نکنیم، از کلاس‌های سفارشی‌سازی نشده و پیش‌فرض خود 
    // Identity
    // استفاده می‌کند
    public class ApplicationDbContext : DbContext
    // to be added
    // IdentityDbContext<User,Role,int> 
    {
        // TODO: solve this problem on vscode
        //Comment it on migration
        //uncomment it after migration
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {            
        }

        // // Uncomment it on migration
        // // Comment it after migration
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Setting;Integrated Security=false; user id=sa; password=admin@123");
        //     base.OnConfiguring(optionsBuilder);
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //نکته: خط زیر باید حتما خط اول در تابع نوشته شود
            base.OnModelCreating(modelBuilder);

            var entitiesAssembly = typeof(IEntity).Assembly;
            modelBuilder.RegisterAllEntities<IEntity>(entitiesAssembly);
            modelBuilder.RegisterEntityTypeConfiguration(entitiesAssembly);
            modelBuilder.AddRestrictDeleteBehaviorConvention();
            modelBuilder.AddSequentialGuidForIdConvention();
            modelBuilder.AddPluralizingTableNameConvention();
        }

        public override int SaveChanges()
        {
            _cleanString();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            _cleanString();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            _cleanString();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            _cleanString();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void _cleanString()
        {
            var changedEntities = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            foreach (var item in changedEntities)
            {
                if (item.Entity == null)
                    continue;

                var properties = item.Entity.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

                foreach (var property in properties)
                {
                    var propName = property.Name;
                    var val = (string)property.GetValue(item.Entity, null);

                    if (val.HasValue())
                    {
                        var newVal = val.Fa2En().FixPersianChars();
                        if (newVal == val)
                            continue;

                        property.SetValue(item.Entity, newVal, null);
                    }
                }
            }
        }
    }
}