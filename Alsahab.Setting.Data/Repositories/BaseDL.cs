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
        public virtual IQueryable<TEntity> Table => Entities.Where(s => s.IsDeleted == false).AsNoTracking();
        public virtual IQueryable<TEntity> TableNoTrackingAllData => Entities.AsNoTracking();
        public virtual IQueryable<TEntity> TableNoTracking => Entities.Where(s => s.IsDeleted == false).AsNoTracking();
        public ResponseStatus ResponseStatus { get; set; }
        public string ErrorMessage { get; set; }
        public int ResultCount { get; set; }
        public BaseDL(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
            Entities = DbContext.Set<TEntity>();
            ResponseStatus = ResponseStatus.DatabaseError;
            ErrorMessage = "خطای پایگاه داده";
            ResultCount = 0;
        }
        // public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        // {
        //     Assert.NotNull(entities, nameof(entities));
        //     Entities.RemoveRange(entities);
        //     if (saveNow)
        //         await DbContext.SaveChangesAsync(cancellationToken);
        // }
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
        public virtual async Task<TDto> InsertAsync(TDto dto, CancellationToken cancellationToken, bool saveNow = true)
        {
            // TEntity entity = Activator.CreateInstance(typeof(TEntity)) as TEntity;//, new Object[] {});//new TEntity();//BaseEntity<TEntity, TDto, long>.FromDto(dto);
            var entity = Mapper.Map<TEntity>(dto);

            // foreach (var property in typeof(TEntity).GetProperties())
            // {
            //     try
            //     {
            //         var value = property.GetValue(dto);
            //         var a = property.ChangeType();
            //     }
            //     catch
            //     {
            //         string filterProp = property.Name;//.Replace("From", "");
            //         Type infoType = typeof(TEntity);
            //         PropertyInfo info = infoType.GetProperty(filterProp);
            //         var type = property.PropertyType;
            //         // var value = prop.GetValue(filterDto);
            //         var nullableType = typeof(Nullable<>).MakeGenericType(type);
            //         var value = property.GetValue(dto);
            //     }
            //     if (value != null)
            //         property.SetValue(entity, value, null);
            // }

            // foreach (var prop in entity.GetType().GetProperties())
            //     prop.SetValue(entity,prop.GetValue(dto));


            await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            var resultDto = await TableNoTracking.ProjectTo<TDto>()
                .SingleOrDefaultAsync(s => s.ID == entity.ID, cancellationToken);

            return resultDto;
        }
        public virtual async Task<IList<TDto>> InsertListAsync(IList<TDto> dtoList, CancellationToken cancellationToken, bool saveNow = true)
        {
            var entityList = Mapper.Map<IEnumerable<TDto>, IQueryable<TEntity>>(dtoList);

            await Entities.AddRangeAsync(entityList, cancellationToken).ConfigureAwait(false);

            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            var resultDto = await entityList.ProjectTo<TDto>().ToListAsync(cancellationToken);
            return resultDto;
        }
        public virtual IList<TDto> InsertList(IList<TDto> dtoList, bool saveNow)
        {
            var entityList = Mapper.Map<IEnumerable<TDto>, IQueryable<TEntity>>(dtoList);

            Entities.AddRange(entityList);

            if (saveNow)
                DbContext.SaveChanges();

            var resultDto = entityList.ProjectTo<TDto>().ToList();
            return resultDto;
        }
        public virtual IList<TDto> UpdateList(IList<TDto> dtoList, bool saveNow = true)
        {
            IList<TDto> resultList = new List<TDto>();
            foreach (var dto in dtoList)
            {
                resultList.Add(Update(dto));
            }
            return resultList;
            // List<TEntity> tempList = new List<TEntity>();

            // foreach (var dto in dtoList)
            // {
            //     TEntity entity = (TEntity)Activator.CreateInstance(typeof(TEntity), new object[] { });
            //     tempList.Add(entity.ToEntity(dto)); // AutoMapper.Mapper.Map<TDto, TEntity>(dto);
            // }
            // // var entityList = Mapper.Map<IEnumerable<TDto>, IQueryable<TEntity>>(dtoList);
            // // var entityList = tempList.AsQueryable();

            // foreach (var temp in tempList)
            //     Entities.Update(temp);
            // // Entities.UpdateRange(tempList);
            // if (saveNow)
            //     DbContext.SaveChanges();

            // // var resultDto = entityList.Select(s=>Mapper.Map<TDto>(s))?.ToList();// await entityList.ProjectTo<TDto>().ToListAsync(cancellationToken);
            // var resultDto = tempList.AsQueryable().ProjectTo<TDto>().ToList();

            // return resultDto;
        }
        public virtual async Task<List<TDto>> UpdateListAsync(IList<TDto> dtoList, CancellationToken cancellationToken, bool saveNow = true)
        {
            var entityList = Mapper.Map<IEnumerable<TDto>, IQueryable<TEntity>>(dtoList);

            Entities.UpdateRange(entityList);

            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken);

            var resultDto = entityList.ProjectTo<TDto>().ToList();

            return resultDto;
        }
        public virtual List<TDto> DeleteRange(IList<TDto> dtoList, bool saveNow = true)
        {
            var entityList = Mapper.Map<IEnumerable<TDto>, IQueryable<TEntity>>(dtoList);

            Entities.RemoveRange(entityList);

            if (saveNow)
                DbContext.SaveChanges();

            var resultDto = entityList.ProjectTo<TDto>().ToList();

            return resultDto;
        }
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
        public virtual async Task<TDto> DeleteAsync(TDto dto, CancellationToken cancellationToken, bool saveNow = true)
        {
            var entity = await Entities.FindAsync(dto.ID);
            if (entity == null)
                throw new AppException(ResponseStatus.NotFound, "not found entity.");

            entity = entity.ToEntity(dto);

            Entities.Remove(entity);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken);

            return dto;
        }
        public virtual async Task<IList<TDto>> GetAllAsync(CancellationToken cancellationToken, PagingInfoDTO paging = null)
        {
            var query = TableNoTracking;
            ResultCount = query.Count();
            if (paging?.IsPaging == true)
            {
                int skip = (paging.Index - 1) * paging.Size;
                query = query.OrderBy(s => s.ID).Skip(skip).Take(paging.Size);
            }
            var result = await query.ProjectTo<TDto>().ToListAsync(cancellationToken);
            ErrorMessage = "";
            ResponseStatus = ResponseStatus.Successful;
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
            ErrorMessage = "";
            ResponseStatus = ResponseStatus.Successful;
            return result;
            //TODO:
            // اگر تابع ای‌سینک بالا کامل شد، در اینجا هم بیاید
        }
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
            ErrorMessage = "";
            ResponseStatus = ResponseStatus.Successful;
            return result;
        }
        public virtual async Task<IList<TDto>> GetAsync(TFilterDto filterDto, CancellationToken cancellationToken, PagingInfoDTO paging = null)
        {
            var query = TableNoTracking;
            ResultCount = query.Count();
            if (paging?.IsPaging == true)
            {
                int skip = (paging.Index - 1) * paging.Size;
                query = query.OrderBy(s => s.ID).Skip(skip).Take(paging.Size);
            }
            var result = await query.ProjectTo<TDto>().ToListAsync(cancellationToken);
            ErrorMessage = "";
            ResponseStatus = ResponseStatus.Successful;
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
        Task<IList<TDto>> IBaseDL<TEntity, TDto, TFilterDto>.UpdateListAsync(IList<TDto> dtoList, CancellationToken cancellationToken, bool saveNow)
        {
            throw new NotImplementedException();
        }
        public Task<IList<TDto>> DeleteListAsync(IList<TDto> dtoList, CancellationToken cancellationToken, bool saveNow = true)
        {
            throw new NotImplementedException();
        }
        public IList<TDto> DeleteList(IList<TDto> dtoList, bool saveNow = true)
        {
            throw new NotImplementedException();
        }
        public virtual async Task<TDto> GetByIdAsync(CancellationToken cancellationToken, long? id)
        {
            if (!(id > 0))
                throw new AppException(ResponseStatus.BadRequest, "id must specified.");

            return await TableNoTracking.ProjectTo<TDto>()
                .SingleOrDefaultAsync(q => q.ID.Equals(id), cancellationToken);
        }
        public virtual TDto GetById(long? id)
        {
            if (!(id > 0))
                throw new AppException(ResponseStatus.BadRequest, "id must specified.");

            return TableNoTracking.ProjectTo<TDto>().SingleOrDefault(q => q.ID.Equals(id));
        }
        public virtual async Task<IList<TDto>> GetByIdListAsync(CancellationToken cancellationToken, IList<long> idList)
        {
            return await TableNoTracking.ProjectTo<TDto>()
                .Where(q => idList.Contains(q.ID ?? 0)).ToListAsync(cancellationToken);
        }
        public virtual IList<TDto> GetByIdList(IList<long> idList)
        {
            return TableNoTracking.ProjectTo<TDto>()
                .Where(q => idList.Contains(q.ID ?? 0)).ToList();
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