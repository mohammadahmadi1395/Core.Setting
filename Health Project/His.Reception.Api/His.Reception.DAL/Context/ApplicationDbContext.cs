
using His.Reception.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace His.Reception.DAL.Context
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<Allergy> Allergy { get; set; }
        public virtual DbSet<BirthPlace> BirthPlace { get; set; }
        public virtual DbSet<BloodGroup> BloodGroup { get; set; }
        public virtual DbSet<Doctors> Doctors { get; set; }
        public virtual DbSet<Education> Education { get; set; }
        public virtual DbSet<Expertise> Expertise { get; set; }
        public virtual DbSet<GeneralStatus> GeneralStatus { get; set; }
        public virtual DbSet<Illness> Illness { get; set; }
        public virtual DbSet<IssuePlace> IssuePlace { get; set; }
        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<MaritalStatus> MaritalStatus { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<PatientExtraInfo> PatientExtraInfo { get; set; }
        public virtual DbSet<Permissions> Permissions { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Presenter> Presenter { get; set; }
        public virtual DbSet<ReceptionService> ReceptionService { get; set; }
        public virtual DbSet<ReceptionType> ReceptionType { get; set; }
        public virtual DbSet<Receptions> Receptions { get; set; }
        public virtual DbSet<RefferFrom> RefferFrom { get; set; }
        public virtual DbSet<RefferReason> RefferReason { get; set; }
        public virtual DbSet<Regional> Regional { get; set; }
        public virtual DbSet<Rh> Rh { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Section> Section { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<Sex> Sex { get; set; }
        public virtual DbSet<SpecialIllness> SpecialIllness { get; set; }
        public virtual DbSet<SpecialIllnessReception> SpecialIllnessReception { get; set; }
        public virtual DbSet<UserPermission> UserPermission { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //    base.OnModelCreating(modelBuilder);
            //    //builder.HasDefaultSchema("cert");
            //    //builder.ApplyConfiguration(new CertificateControlEntityTypeConfiguration());
            //    //builder.ApplyConfiguration(new CertificateRequestEntityTypeConfiguration());
            //    //builder.ApplyConfiguration(new CertificateTemplateEntityTypeConfiguration());
            //    //builder.ApplyConfiguration(new ControlEntityTypeConfiguration());
            //    //builder.ApplyConfiguration(new PrintTemplateEntityTypeConfiguration());
            //    //builder.ApplyConfiguration(new SubjectEntityTypeConfiguration());
            //    //builder.AddAuditableShadowProperties();

            #region config

            modelBuilder.Entity<Allergy>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<BirthPlace>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<BloodGroup>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<Doctors>(entity =>
            {
                entity.Property(e => e.MedicalSystemNo).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(200);

                entity.HasOne(d => d.Expertise)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.ExpertiseId)
                    .HasConstraintName("FK_Doctors_Expertise");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_Doctors_Person");
            });

            modelBuilder.Entity<Education>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<Expertise>(entity =>
            {
                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<GeneralStatus>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<Illness>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Icd10)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Icd9)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<IssuePlace>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.Property(e => e.Browser).HasMaxLength(50);

                entity.Property(e => e.ExpireDate).HasColumnType("datetime");

                entity.Property(e => e.Ip).HasMaxLength(20);

                entity.Property(e => e.Language).HasMaxLength(10);

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MaritalStatus>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.FileNo).HasMaxLength(20);

                entity.Property(e => e.Hisno).HasColumnName("HISNo");

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.HasOne(d => d.BloodGroup)
                    .WithMany(p => p.Patient)
                    .HasForeignKey(d => d.BloodGroupId)
                    .HasConstraintName("FK_Paitient_BloodGroup");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Patient)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_Paitient_Person");
            });

            modelBuilder.Entity<PatientExtraInfo>(entity =>
            {
                entity.Property(e => e.Height).HasMaxLength(10);

                entity.Property(e => e.IssueDate).HasColumnType("date");

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.Weight).HasMaxLength(10);

                entity.Property(e => e.WorkAdress).HasMaxLength(250);

                entity.Property(e => e.WorkPhone).HasMaxLength(12);

                entity.HasOne(d => d.Allergy)
                    .WithMany(p => p.PatientExtraInfo)
                    .HasForeignKey(d => d.AllergyId)
                    .HasConstraintName("FK_PatientExtraInfo_Allergy");

                entity.HasOne(d => d.ChronicIllness)
                    .WithMany(p => p.PatientExtraInfo)
                    .HasForeignKey(d => d.ChronicIllnessId)
                    .HasConstraintName("FK_PaitientExtraInfo_ChronicIIlness");

                entity.HasOne(d => d.Education)
                    .WithMany(p => p.PatientExtraInfo)
                    .HasForeignKey(d => d.EducationId)
                    .HasConstraintName("FK_PaitientExtraInfo_Education");

                entity.HasOne(d => d.IssuePlace)
                    .WithMany(p => p.PatientExtraInfo)
                    .HasForeignKey(d => d.IssuePlaceId)
                    .HasConstraintName("FK_PaitientExtraInfo_IssuePlace");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.PatientExtraInfo)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_PaitientExtraInfo_Job");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.PatientExtraInfo)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK_PaitientExtraInfo_Patient");

                entity.HasOne(d => d.Regional)
                    .WithMany(p => p.PatientExtraInfo)
                    .HasForeignKey(d => d.RegionalId)
                    .HasConstraintName("FK_PatientExtraInfo_Regional");

                entity.HasOne(d => d.Rh)
                    .WithMany(p => p.PatientExtraInfo)
                    .HasForeignKey(d => d.RhId)
                    .HasConstraintName("FK_PatientExtraInfo_Rh");
            });

            modelBuilder.Entity<Permissions>(entity =>
            {
                entity.Property(e => e.ModuleName).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(100);

                entity.Property(e => e.NoteLang2).HasMaxLength(100);

                entity.Property(e => e.PageAdress).HasMaxLength(100);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FatherName).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.Mobile).HasMaxLength(20);

                entity.Property(e => e.NationalCode).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.ShNo).HasMaxLength(20);

                entity.HasOne(d => d.BirthPlace)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.BirthPlaceId)
                    .HasConstraintName("FK_birthplace_person");

                entity.HasOne(d => d.MaritalStatus)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.MaritalStatusId)
                    .HasConstraintName("FK_Person_MaritalStatus");

                entity.HasOne(d => d.Sex)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.SexId)
                    .HasConstraintName("FK_Person_Sex");
            });

            modelBuilder.Entity<Presenter>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<ReceptionService>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Reception)
                    .WithMany(p => p.ReceptionService)
                    .HasForeignKey(d => d.ReceptionId)
                    .HasConstraintName("FK_ReceptionService_Reception");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ReceptionService)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_ReceptionService_Service");
            });

            modelBuilder.Entity<ReceptionType>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<Receptions>(entity =>
            {
                entity.Property(e => e.Advice).HasMaxLength(500);

                entity.Property(e => e.Note).HasMaxLength(250);

                entity.Property(e => e.ReceptionDate).HasColumnType("datetime");

                entity.Property(e => e.RefferDate).HasColumnType("datetime");

                entity.HasOne(d => d.BedDoctor)
                    .WithMany(p => p.ReceptionsBedDoctor)
                    .HasForeignKey(d => d.BedDoctorId)
                    .HasConstraintName("FK_Reception_Doctors1");

                entity.HasOne(d => d.CurrentIllness)
                    .WithMany(p => p.Receptions)
                    .HasForeignKey(d => d.CurrentIllnessId)
                    .HasConstraintName("FK_Receptions_Illness");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.ReceptionsDoctor)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_Reception_Doctors");

                entity.HasOne(d => d.GeneralStatus)
                    .WithMany(p => p.Receptions)
                    .HasForeignKey(d => d.GeneralStatusId)
                    .HasConstraintName("FK_Reception_GeneralStatus");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Receptions)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK_Reciption_Paitient");

                entity.HasOne(d => d.Presenter)
                    .WithMany(p => p.Receptions)
                    .HasForeignKey(d => d.PresenterId)
                    .HasConstraintName("FK_Receptions_Presenter");

                entity.HasOne(d => d.ReceptionType)
                    .WithMany(p => p.Receptions)
                    .HasForeignKey(d => d.ReceptionTypeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Reciption_ReceptionType");

                entity.HasOne(d => d.RefferFrom)
                    .WithMany(p => p.Receptions)
                    .HasForeignKey(d => d.RefferFromId)
                    .HasConstraintName("FK_Receptions_RefferFrom");

                entity.HasOne(d => d.RefferReason)
                    .WithMany(p => p.Receptions)
                    .HasForeignKey(d => d.RefferReasonId)
                    .HasConstraintName("FK_Reciption_RefferReason");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Receptions)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("FK_Reciption_Section");
            });

            modelBuilder.Entity<RefferFrom>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<RefferReason>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<Regional>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<Rh>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(150);

                entity.Property(e => e.No).HasMaxLength(10);

                entity.Property(e => e.Note).HasMaxLength(200);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.Property(e => e.InterNationalCode).HasMaxLength(50);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.Property(e => e.TitleLang2).HasMaxLength(100);
            });

            modelBuilder.Entity<Sex>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<SpecialIllness>(entity =>
            {
                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<SpecialIllnessReception>(entity =>
            {
                entity.HasOne(d => d.Reception)
                    .WithMany(p => p.SpecialIllnessReception)
                    .HasForeignKey(d => d.ReceptionId)
                    .HasConstraintName("FK_SpecialIllnessReception_Receptions");

                entity.HasOne(d => d.SpecialIllness)
                    .WithMany(p => p.SpecialIllnessReception)
                    .HasForeignKey(d => d.SpecialIllnessId)
                    .HasConstraintName("FK_Illness_SpecialIllnessReception");
            });

            modelBuilder.Entity<UserPermission>(entity =>
            {
                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.UserPermission)
                    .HasForeignKey(d => d.PermissionId)
                    .HasConstraintName("FK_UserPermission_Permissions");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.UserPermission)
                    .HasForeignKey(d => d.SectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserPermission_Section");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPermission)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserPermission_Users");
            });

            modelBuilder.Entity<UserRoles>(entity =>
            {
                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_UserRoles_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserRoles_Users");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Password).HasMaxLength(250);

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_Users_Person");
            });

            #endregion

        }
        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            Set<TEntity>().AddRange(entities);
        }

        public void ExecuteSqlCommand(string query)
        {
            Database.ExecuteSqlCommand(query);
        }

        public void ExecuteSqlCommand(string query, params object[] parameters)
        {
            Database.ExecuteSqlCommand(query, parameters);
        }

        public T GetShadowPropertyValue<T>(object entity, string propertyName) where T : IConvertible
        {
            var value = this.Entry(entity).Property(propertyName).CurrentValue;
            return value != null
                ? (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture)
                : default(T);
        }

        public object GetShadowPropertyValue(object entity, string propertyName)
        {
            return this.Entry(entity).Property(propertyName).CurrentValue;
        }

        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Update(entity);
        }

        public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            Set<TEntity>().RemoveRange(entities);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ChangeTracker.DetectChanges();

            beforeSaveTriggers();

            ChangeTracker.AutoDetectChangesEnabled = false; // for performance reasons, to avoid calling DetectChanges() again.
            var result = base.SaveChanges(acceptAllChangesOnSuccess);
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            beforeSaveTriggers();

            ChangeTracker.AutoDetectChangesEnabled = false; // for performance reasons, to avoid calling DetectChanges() again.
            var result = base.SaveChanges();
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();

            beforeSaveTriggers();

            ChangeTracker.AutoDetectChangesEnabled = false; // for performance reasons, to avoid calling DetectChanges() again.
            var result = base.SaveChangesAsync(cancellationToken);
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();

            beforeSaveTriggers();

            ChangeTracker.AutoDetectChangesEnabled = false; // for performance reasons, to avoid calling DetectChanges() again.
            var result = base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        private void beforeSaveTriggers()
        {
            validateEntities();
            setShadowProperties();
            //this.ApplyCorrectYeKe();
        }

        private void setShadowProperties()
        {
            // we can't use constructor injection anymore, because we are using the `AddDbContextPool<>`
            var httpContextAccessor = this.GetService<IHttpContextAccessor>();

            //httpContextAccessor.CheckArgumentIsNull(nameof(httpContextAccessor));
            //ChangeTracker.SetAuditableEntityPropertyValues(httpContextAccessor);
        }

        private void validateEntities()
        {
            //var errors = this.GetValidationErrors();
            //if (!string.IsNullOrWhiteSpace(errors))
            //{
            //    // we can't use constructor injection anymore, because we are using the `AddDbContextPool<>`
            //    var loggerFactory = this.GetService<ILoggerFactory>();
            //    loggerFactory.CheckArgumentIsNull(nameof(loggerFactory));
            //    var logger = loggerFactory.CreateLogger<ApplicationDbContext>();
            //    logger.LogError(errors);
            //    throw new InvalidOperationException(errors);
            //}
        }

    }
    
}
