using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Alsahab.Common;
using Alsahab.Setting.Common;
using Alsahab.Setting.Common.Api;
using Alsahab.Setting.Common.Exceptions;
using Alsahab.Setting.Common.Utilities;
using Alsahab.Setting.Data.Contracts;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Alsahab.Setting.Data.Repositories
{
    public class BaseDL<TEntity, TDto, TFilterDto> : IBaseDL<TEntity, TDto, TFilterDto>
        where TEntity : BaseEntity<TEntity, TDto, long>, IEntity
        where TDto : BaseDTO//class
        where TFilterDto : TDto
    {

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


        // public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        // {
        //     Assert.NotNull(entities, nameof(entities));
        //     Entities.UpdateRange(entities);
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

        public virtual TDto Add(TDto dto, bool saveNow = true)
        {
            Assert.NotNull(dto, nameof(dto));

            TEntity entity = BaseEntity<TEntity, TDto, long>.FromDto(dto); // AutoMapper.Mapper.Map<TDto, TEntity>(dto);

            Entities.Add(entity);
            if (saveNow)
                DbContext.SaveChanges();

            var resultDto = TableNoTracking.ProjectTo<TDto>()
                .SingleOrDefault(s => s.ID.Equals(entity.ID));

            return resultDto;
        }

        public virtual async Task<TDto> AddAsync(TDto dto, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(dto, nameof(dto));

            TEntity entity = BaseEntity<TEntity, TDto, long>.FromDto(dto); // AutoMapper.Mapper.Map<TDto, TEntity>(dto);
            await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            var resultDto = await TableNoTracking.ProjectTo<TDto>()
                .SingleOrDefaultAsync(s => s.ID.Equals(entity.ID), cancellationToken);

            return resultDto;
        }

        public virtual async Task<IEnumerable<TDto>> AddRangeAsync(IEnumerable<TDto> dtoList, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(dtoList, nameof(dtoList));

            var entityList = Mapper.Map<IEnumerable<TDto>, IQueryable<TEntity>>(dtoList);

            await Entities.AddRangeAsync(entityList, cancellationToken).ConfigureAwait(false);

            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            var resultDto = await entityList.ProjectTo<TDto>().ToListAsync(cancellationToken);
            return resultDto;
        }

        public virtual IEnumerable<TDto> AddRange(IEnumerable<TDto> dtoList, bool saveNow)
        {
            Assert.NotNull(dtoList, nameof(dtoList));

            var entityList = Mapper.Map<IEnumerable<TDto>, IQueryable<TEntity>>(dtoList);

            Entities.AddRange(entityList);

            if (saveNow)
                DbContext.SaveChanges();

            var resultDto = entityList.ProjectTo<TDto>().ToList();
            return resultDto;
        }

        public virtual IEnumerable<TDto> UpdateRange(IEnumerable<TDto> dtoList, bool saveNow = true)
        {
            Assert.NotNull(dtoList, nameof(dtoList));

            var entityList = Mapper.Map<IEnumerable<TDto>, IQueryable<TEntity>>(dtoList);

            Entities.UpdateRange(entityList);
            if (saveNow)
                DbContext.SaveChanges();

            var resultDto = entityList.ProjectTo<TDto>().ToList();

            return resultDto;
        }


        // public virtual void DeleteRange(IEnumerable<TEntity> entities, bool saveNow = true)
        // {
        //     Assert.NotNull(entities, nameof(entities));
        //     Entities.RemoveRange(entities);
        //     if (saveNow)
        //         DbContext.SaveChanges();
        // }

        public virtual TDto Update(TDto dto, bool saveNow = true)
        {
            Assert.NotNull(dto, nameof(dto));

            var entity = Entities.Find(dto.ID);
            if (entity == null)
                throw new AppException(ApiResultStatusCode.NotFound, "not found entity.");

            entity = entity.ToEntity(dto);
            Entities.Update(entity);

            if (saveNow)
                DbContext.SaveChanges();

            var resultDto = TableNoTracking.ProjectTo<TDto>()
                .SingleOrDefault(q => q.ID.Equals(dto.ID));

            return resultDto;
        }


        public virtual async Task<TDto> UpdateAsync(TDto dto, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(dto, nameof(dto));

            var entity = await Entities.FindAsync(dto.ID);
            if (entity == null)
                throw new AppException(ApiResultStatusCode.NotFound, "not found entity.");

            entity = entity.ToEntity(dto);
            Entities.Update(entity);

            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken);

            var resultDto = await TableNoTracking.ProjectTo<TDto>()
                .SingleOrDefaultAsync(q => q.ID.Equals(dto.ID), cancellationToken);

            return resultDto;
        }

        public virtual TDto Delete(TDto dto, bool saveNow = true)
        {
            Assert.NotNull(dto, nameof(dto));
            var entity = Entities.Find(dto.ID);
            if (entity == null)
                throw new AppException(ApiResultStatusCode.NotFound, "not found entity.");

            entity = entity.ToEntity(dto);

            Entities.Remove(entity);
            if (saveNow)
                DbContext.SaveChanges();

            return dto;
        }

        public virtual async Task<TDto> DeleteAsync(TDto dto, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(dto, nameof(dto));
            var entity = await Entities.FindAsync(dto.ID);
            if (entity == null)
                throw new AppException(ApiResultStatusCode.NotFound, "not found entity.");

            entity = entity.ToEntity(dto);

            Entities.Remove(entity);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken);

            return dto;
        }

        public virtual async Task<IList<TDto>> GetAsync(TFilterDto filterDto, CancellationToken cancellationToken)
        {
            var query = TableNoTracking;
            var result = await query.ProjectTo<TDto>().ToListAsync(cancellationToken);
            ErrorMessage = "";
            ResponseStatus = ResponseStatus.Successful;
            return result;
            // foreach (var prop in filterDto.GetType().GetProperties())
            // {
            //     var type = prop.PropertyType;
            //     var value = prop.GetValue(filterDto);
            //     if ((prop.Name.Contains("Id") || prop.Name.Contains("ID")) && !prop.Name.Contains("List") && (long)value > 0)
            //     {
            //         query = query.Where(s => prop.GetValue(s) == prop.GetValue(filterDto));
            //     }
            //     else if (type == typeof(string) && !string.IsNullOrWhiteSpace(value.ToString()))
            //     {
            //         query = query.Where(s => prop.GetValue(s).ToString().Contains(prop.GetValue(filterDto).ToString()));
            //     }
            //     else if ((type == typeof(DateTime) || type == typeof(DateTime?) || type == typeof(DateTimeOffset) || type == typeof(DateTimeOffset?)) && (DateTime)value > DateTime.MinValue)
            //     {
            //         if (prop.Name.Contains("From"))
            //         {
            //             string filterProp = prop.Name.Replace("From", "");
            //             Type infoType = typeof(TFilterDto);
            //             PropertyInfo info = infoType.GetProperty(filterProp);
            //             // query = query.Where(s => Convert.ChangeType(prop.GetValue(s), Type.GetType("DateTime")) > Convert.ChangeType(info.GetValue(filterDto), Type.GetType("DateTime")));// (DateTime)info.GetValue(filterDto));
            //             query = query.Where(s => (DateTime)prop.GetValue(s) > (DateTime)info.GetValue(filterDto));
            //         }
            //         else if (prop.Name.Contains("To"))
            //         {
            //             string filterProp = prop.Name.Replace("To", "");
            //             Type infoType = typeof(TFilterDto);
            //             PropertyInfo info = infoType.GetProperty(filterProp);
            //             query = query.Where(s => (DateTime)prop.GetValue(s) < (DateTime)info.GetValue(filterDto));
            //         }
            //     }
            //     else if ((type == typeof(long) || type == typeof(long?)) && (long)value > 0)
            //     {
            //         if (prop.Name.Contains("From"))
            //         {
            //             string filterProp = prop.Name.Replace("From", "");
            //             Type infoType = typeof(TFilterDto);
            //             PropertyInfo info = infoType.GetProperty(filterProp);
            //             query = query.Where(s => (long)prop.GetValue(s) > (long)info.GetValue(filterDto));
            //         }
            //         else if (prop.Name.Contains("To"))
            //         {
            //             string filterProp = prop.Name.Replace("To", "");
            //             Type infoType = typeof(TFilterDto);
            //             PropertyInfo info = infoType.GetProperty(filterProp);
            //             query = query.Where(s => (long)prop.GetValue(s) < (long)info.GetValue(filterDto));
            //         }
            //     }
            //     else if ((type == typeof(int) || type == typeof(int?)) && (int)value > 0)
            //     {
            //         if (prop.Name.Contains("From"))
            //         {
            //             string filterProp = prop.Name.Replace("From", "");
            //             Type infoType = typeof(TFilterDto);
            //             PropertyInfo info = infoType.GetProperty(filterProp);
            //             query = query.Where(s => (int)prop.GetValue(s) > (int)info.GetValue(filterDto));
            //         }
            //         else if (prop.Name.Contains("To"))
            //         {
            //             string filterProp = prop.Name.Replace("To", "");
            //             Type infoType = typeof(TFilterDto);
            //             PropertyInfo info = infoType.GetProperty(filterProp);
            //             query = query.Where(s => (int)prop.GetValue(s) < (int)info.GetValue(filterDto));
            //         }
            //     }
            // }
            // return await query.ProjectTo<TDto>()
            //     .ToListAsync(cancellationToken); ;
        }


        public List<TDto> Get(TFilterDto filterDto)
        {
            throw new NotImplementedException();
        }


        // #region  Async Methods
        // public virtual Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
        // {
        //     return Entities.FindAsync(ids);
        // }

        // #endregion

        // #region Sync Methods
        // public virtual TEntity GetById(params object[] ids)
        // {
        //     return Entities.Find(ids);
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