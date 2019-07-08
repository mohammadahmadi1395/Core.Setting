using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Alsahab.Common;
using Alsahab.Common.Exceptions;
using Alsahab.Common.Utilities;
using Alsahab.Setting.Data.Interfaces;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        public virtual IQueryable<TEntity> TableAllData => Entities;
        public virtual IQueryable<TEntity> Table => Entities.Where(s => s.IsDeleted == false);
        public virtual IQueryable<TEntity> TableNoTrackingAllData => Entities.AsNoTracking();
        public virtual IQueryable<TEntity> TableNoTracking => Entities.Where(s => s.IsDeleted == false).AsNoTracking();
        public int ResultCount { get; set; }
        public BaseDL(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
            Entities = DbContext.Set<TEntity>();
            ResultCount = 0;
        }

        #region Async Get
        public virtual async Task<IList<TDto>> GetAllAsync(CancellationToken cancellationToken, PagingInfoDTO paging = null)
        {
            var query = TableNoTracking;
            ResultCount = await query.CountAsync(cancellationToken);
            if (paging?.IsPaging == true)
            {
                int skip = (paging.Index - 1) * paging.Size;
                query = query.OrderBy(s => s.ID).Skip(skip).Take(paging.Size);
            }
            var result = await query.ProjectTo<TDto>().ToListAsync(cancellationToken);
            return result;
        }
        public virtual async Task<IList<TDto>> GetAsync(TFilterDto filterDto, CancellationToken cancellationToken, PagingInfoDTO paging = null)
        {
            var query = TableNoTracking;
            ResultCount = await query.CountAsync(cancellationToken);
            if (paging?.IsPaging == true)
            {
                int skip = (paging.Index - 1) * paging.Size;
                query = query.OrderBy(s => s.ID).Skip(skip).Take(paging.Size);
            }
            var result = await query.ProjectTo<TDto>().ToListAsync(cancellationToken);
            return result;
            //TODO:
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
        public virtual async Task<TDto> GetByIdAsync(CancellationToken cancellationToken, long? id)
        {
            if (!(id > 0))
                throw new AppException(ResponseStatus.BadRequest, "id must specified.");

            return await TableNoTracking.ProjectTo<TDto>()
                .SingleOrDefaultAsync(q => q.ID.Equals(id), cancellationToken);
        }
        public virtual async Task<IList<TDto>> GetByIdListAsync(CancellationToken cancellationToken, IList<long> idList)
        {
            idList = idList.Where(s => s > 0)?.ToList();
            if (idList?.Count > 0)
                return await TableNoTracking.ProjectTo<TDto>()
                    .Where(q => idList.Contains(q.ID ?? 0)).ToListAsync(cancellationToken);
            else
                return new List<TDto>();
        }
        #endregion Async Get
        #region Sync Get
        public virtual IList<TDto> GetAll(PagingInfoDTO paging = null)
        {
            var query = TableNoTracking;
            ResultCount = query.Count();
            if (paging?.IsPaging == true)
            {
                int skip = (paging.Index - 1) * paging.Size;
                query = query.OrderBy(s => s.ID).Skip(skip).Take(paging.Size);
            }
            var result = query.ProjectTo<TDto>().ToList();
            return result;
        }
        public virtual IList<TDto> Get(TFilterDto filterDto, PagingInfoDTO paging = null)
        {
            var query = TableNoTracking;
            ResultCount = query.Count();
            if (paging?.IsPaging == true)
            {
                int skip = (paging.Index - 1) * paging.Size;
                query = query.OrderBy(s => s.ID).Skip(skip).Take(paging.Size);
            }
            var result = query.ProjectTo<TDto>().ToList();
            return result;
            //TODO:
            // اگر تابع ای‌سینک بالا کامل شد، در اینجا هم بیاید
        }
        public virtual TDto GetById(long? id)
        {
            if (!(id > 0))
                throw new AppException(ResponseStatus.BadRequest, "id must specified.");

            return TableNoTracking.ProjectTo<TDto>().SingleOrDefault(q => q.ID.Equals(id));
        }
        public virtual IList<TDto> GetByIdList(IList<long> idList)
        {
            idList = idList.Where(s => s > 0)?.ToList();
            if (idList?.Count > 0)
                return TableNoTracking.ProjectTo<TDto>()
                    .Where(q => idList.Contains(q.ID ?? 0)).ToList();
            else return new List<TDto>();
        }
        #endregion Sync Get

        #region Async Insert
        public virtual async Task<TDto> InsertAsync(TDto dto, CancellationToken cancellationToken, bool saveNow = true)
        {
            TEntity entity = BaseEntity<TEntity, TDto, long>.FromDto(dto); // AutoMapper.Mapper.Map<TDto, TEntity>(dto);

            await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            var resultDto = await TableNoTracking.ProjectTo<TDto>()
                .SingleOrDefaultAsync(s => s.ID == entity.ID, cancellationToken);

            return resultDto;
        }
        public virtual async Task<IList<TDto>> InsertListAsync(IList<TDto> dtoList, CancellationToken cancellationToken, bool saveNow = true)
        {
            var entityList = Mapper.Map<IList<TEntity>>(dtoList).AsQueryable();

            await Entities.AddRangeAsync(entityList, cancellationToken).ConfigureAwait(false);

            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            IList<TDto> resultDtoList = new List<TDto>();
            foreach (var val in entityList)
                resultDtoList.Add(await TableNoTracking.ProjectTo<TDto>().SingleOrDefaultAsync(s => s.ID == val.ID, cancellationToken));
            return resultDtoList;
        }
        #endregion Async Insert
        #region Sync Insert
        public virtual TDto Insert(TDto dto, bool saveNow = true)
        {
            TEntity entity = BaseEntity<TEntity, TDto, long>.FromDto(dto); // AutoMapper.Mapper.Map<TDto, TEntity>(dto);

            Entities.Add(entity);
            if (saveNow)
                DbContext.SaveChanges();

            var resultDto = TableNoTracking.ProjectTo<TDto>()
                .SingleOrDefault(s => s.ID == entity.ID);

            return resultDto;
        }
        public virtual IList<TDto> InsertList(IList<TDto> dtoList, bool saveNow)
        {
            var entityList = Mapper.Map<IList<TEntity>>(dtoList).AsQueryable();

            Entities.AddRange(entityList);

            if (saveNow)
                DbContext.SaveChanges();

            IList<TDto> resultDtoList = new List<TDto>();
            foreach (var val in entityList)
                resultDtoList.Add(TableNoTracking.ProjectTo<TDto>().SingleOrDefault(s => s.ID == val.ID));
            return resultDtoList;
        }
        #endregion SyncInsert

        #region Async Update
        public virtual async Task<TDto> UpdateAsync(TDto dto, CancellationToken cancellationToken, bool saveNow = true)
        {
            var entity = await Entities.FindAsync(dto.ID);
            if (entity == null)
                throw new AppException(ResponseStatus.NotFound, "not found entity.");

            entity = entity.ToEntity(dto);
            Entities.Update(entity);

            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken);

            var resultDto = await TableNoTracking.ProjectTo<TDto>()
                .SingleOrDefaultAsync(q => q.ID == dto.ID, cancellationToken);

            return resultDto;
        }
        public virtual async Task<IList<TDto>> UpdateListAsync(IList<TDto> dtoList, CancellationToken cancellationToken, bool saveNow = true)
        {
            var entityList = Mapper.Map<IList<TEntity>>(dtoList).AsQueryable();

            Entities.UpdateRange(entityList);

            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken);

            IList<TDto> resultDtoList = new List<TDto>();
            foreach (var val in entityList)
                resultDtoList.Add(TableNoTracking.ProjectTo<TDto>().SingleOrDefault(s => s.ID == val.ID));
            return resultDtoList;
        }
        #endregion Async Update

        #region Sync Update
        public virtual TDto Update(TDto dto, bool saveNow = true)
        {
            var entity = Entities.Find(dto.ID);
            if (entity == null)
                throw new AppException(ResponseStatus.NotFound, "not found entity.");

            entity = entity.ToEntity(dto);
            Entities.Update(entity);

            if (saveNow)
                DbContext.SaveChanges();

            var resultDto = TableNoTrackingAllData.ProjectTo<TDto>()
                .SingleOrDefault(q => q.ID.Equals(dto.ID));

            return resultDto;
        }

        public virtual IList<TDto> UpdateList(IList<TDto> dtoList, bool saveNow = true)
        {
            var entityList = Mapper.Map<IList<TEntity>>(dtoList).AsQueryable();
            Entities.UpdateRange(entityList);
            if (saveNow)
                DbContext.SaveChanges();
            IList<TDto> resultDtoList = new List<TDto>();
            foreach (var val in entityList)
                resultDtoList.Add(TableNoTracking.ProjectTo<TDto>().SingleOrDefault(s => s.ID == val.ID));
            return resultDtoList;
        }
        #endregion Sync Update


        #region Delete Async
        public virtual async Task<TDto> DeleteAsync(TDto dto, CancellationToken cancellationToken, bool saveNow = true)
        {
            var entity = await Entities.FindAsync(dto.ID);
            if (entity == null)
                throw new AppException(ResponseStatus.NotFound, "not found entity.");
            entity = entity.ToEntity(dto);

            var result = TableNoTracking.ProjectTo<TDto>()
                    .SingleOrDefault(q => q.ID.Equals(dto.ID));

            Entities.Remove(entity);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
        public async virtual Task<IList<TDto>> DeleteListAsync(IList<TDto> dtoList, CancellationToken cancellationToken, bool saveNow = true)
        {
            var entityList = Mapper.Map<IList<TEntity>>(dtoList).AsQueryable();
            var idList = dtoList?.Select(s=>s.ID)?.ToList();
            var result = await TableNoTracking.ProjectTo<TDto>()
                    .Where(q => idList.Contains(q.ID ?? 0)).ToListAsync(cancellationToken);

            Entities.RemoveRange(entityList);
            if (saveNow)
                DbContext.SaveChanges();
            
            return result;
        }
        #endregion Delete Async
        #region Deleted sync
        public virtual TDto Delete(TDto dto, bool saveNow = true)
        {
            var entity = Entities.Find(dto.ID);
            if (entity == null)
                throw new AppException(ResponseStatus.NotFound, "not found entity.");
            entity = entity.ToEntity(dto);
            Entities.Remove(entity);
            if (saveNow)
                DbContext.SaveChanges();
            return dto;
        }
        public IList<TDto> DeleteList(IList<TDto> dtoList, bool saveNow = true)
        {
            var entityList = Mapper.Map<IList<TEntity>>(dtoList).AsQueryable();
            var idList = dtoList?.Select(s=>s.ID)?.ToList();
            var result = TableNoTracking.ProjectTo<TDto>()
                    .Where(q => idList.Contains(q.ID ?? 0)).ToList();

            Entities.RemoveRange(entityList);
            if (saveNow)
                DbContext.SaveChanges();
            
            return result;
        }
        #endregion Delete sync


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