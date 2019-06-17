using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Alsahab.Common;
using Alsahab.Setting.Common;
using Alsahab.Setting.Common.Utilities;
using Alsahab.Setting.Data.Contracts;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Alsahab.Setting.Data.Repositories
{
    public class BaseDL<TEntity, TDto> : IBaseDL<TEntity, TDto>
        where TEntity : BaseEntity, IEntity
        where TDto : class
    {

        public BaseDL()
        {
            // AutoMapper.Mapper.Initialize(config =>
            // {
            //     config.AddCustomMappingProfile();
            // });

            // //Compile Mapping after configuration to boost map speed
            // AutoMapper.Mapper.Configuration.CompileMappings();

        }

        public virtual async Task<TDto> AddAsync(TDto dto, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(dto, nameof(dto));

            TEntity entity = AutoMapper.Mapper.Map<TDto, TEntity>(dto);
            await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return dto;
        }

        // public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        // {
        //     Assert.NotNull(entity, nameof(entity));
        //     await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
        //     if (saveNow)
        //         await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        // }













        private readonly ApplicationDbContext DbContext;
        public DbSet<TEntity> Entities { get; }
        public virtual IQueryable<TEntity> Table => Entities;
        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();
        public ResponseStatus ResponseStatus { get; set; }
        public string ErrorMessage { get; set; }

        public BaseDL(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
            Entities = DbContext.Set<TEntity>();
            ResponseStatus = ResponseStatus.DatabaseError;
            ErrorMessage = "خطای پایگاه داده";
        }

        // #region  Async Methods
        // public virtual Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
        // {
        //     return Entities.FindAsync(ids, cancellationToken);
        // }

        // public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        // {
        //     Assert.NotNull(entities, nameof(entities));
        //     await Entities.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
        //     if (saveNow)
        //         await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        // }

        // public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        // {
        //     Assert.NotNull(entity, nameof(entity));
        //     Entities.Update(entity);
        //     if (saveNow)
        //         await DbContext.SaveChangesAsync(cancellationToken);
        // }

        // public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        // {
        //     Assert.NotNull(entities, nameof(entities));
        //     Entities.UpdateRange(entities);
        //     if (saveNow)
        //         await DbContext.SaveChangesAsync(cancellationToken);
        // }

        // public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        // {
        //     Assert.NotNull(entity, nameof(entity));
        //     Entities.Remove(entity);
        //     if (saveNow)
        //         await DbContext.SaveChangesAsync(cancellationToken);
        // }

        // public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        // {
        //     Assert.NotNull(entities, nameof(entities));
        //     Entities.RemoveRange(entities);
        //     if (saveNow)
        //         await DbContext.SaveChangesAsync(cancellationToken);
        // }
        // #endregion

        // #region Sync Methods
        // public virtual TEntity GetById(params object[] ids)
        // {
        //     return Entities.Find(ids);
        // }

        // public virtual void Add(TEntity entity, bool saveNow = true)
        // {
        //     Assert.NotNull(entity, nameof(entity));
        //     Entities.Add(entity);
        //     if (saveNow)
        //         DbContext.SaveChanges();
        // }

        // public virtual void AddRange(IEnumerable<TEntity> entities, bool saveNow)
        // {
        //     Assert.NotNull(entities, nameof(entities));
        //     Entities.AddRange(entities);
        //     if (saveNow)
        //         DbContext.SaveChanges();
        // }

        // public virtual void Update(TEntity entity, bool saveNow = true)
        // {
        //     Assert.NotNull(entity, nameof(entity));
        //     Entities.Update(entity);
        //     if (saveNow)
        //         DbContext.SaveChanges();
        // }

        // public virtual void UpdateRange(IEnumerable<TEntity> entities, bool saveNow = true)
        // {
        //     Assert.NotNull(entities, nameof(entities));
        //     Entities.UpdateRange(entities);
        //     if (saveNow)
        //         DbContext.SaveChanges();
        // }

        // public virtual void Delete(TEntity entity, bool saveNow = true)
        // {
        //     Assert.NotNull(entity, nameof(entity));
        //     Entities.Remove(entity);
        //     if (saveNow)
        //         DbContext.SaveChanges();
        // }

        // public virtual void DeleteRange(IEnumerable<TEntity> entities, bool saveNow = true)
        // {
        //     Assert.NotNull(entities, nameof(entities));
        //     Entities.RemoveRange(entities);
        //     if (saveNow)
        //         DbContext.SaveChanges();
        // }
        // #endregion

        // #region Attach && Detach
        // public virtual void Attach(TEntity entity)
        // {
        //     Assert.NotNull(entity, nameof(entity));
        //     if (DbContext.Entry(entity).State == EntityState.Detached)
        //         Entities.Attach(entity);
        // }

        // public virtual void Detach(TEntity entity)
        // {
        //     Assert.NotNull(entity, nameof(entity));
        //     var entry = DbContext.Entry(entity);
        //     if (entry != null)
        //         entry.State = EntityState.Detached;
        // }
        // #endregion

        // #region Explicit Loading
        //  public virtual async Task LoadCollectionAsync<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken)
        //     where TProperty : class
        // {
        //     Attach(entity);

        //     var collection = DbContext.Entry(entity).Collection(collectionProperty);
        //     if (!collection.IsLoaded)
        //         await collection.LoadAsync(cancellationToken).ConfigureAwait(false);
        // }

        // public virtual void LoadCollection<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty)
        //     where TProperty : class
        // {
        //     Attach(entity);
        //     var collection = DbContext.Entry(entity).Collection(collectionProperty);
        //     if (!collection.IsLoaded)
        //         collection.Load();
        // }

        // public virtual async Task LoadReferenceAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty, CancellationToken cancellationToken)
        //     where TProperty : class
        // {
        //     Attach(entity);
        //     var reference = DbContext.Entry(entity).Reference(referenceProperty);
        //     if (!reference.IsLoaded)
        //         await reference.LoadAsync(cancellationToken).ConfigureAwait(false);
        // }

        // public virtual void LoadReference<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty)
        //     where TProperty : class
        // {
        //     Attach(entity);
        //     var reference = DbContext.Entry(entity).Reference(referenceProperty);
        //     if (!reference.IsLoaded)
        //         reference.Load();
        // }
        // #endregion
    }
}