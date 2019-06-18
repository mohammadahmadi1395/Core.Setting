using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Alsahab.Setting.Data.Contracts
{
    public interface IBaseDL<TEntity, TDto, TFilterDto>
        where TEntity : class, IEntity
        where TDto : class
        where TFilterDto : TDto
    {
        Alsahab.Common.ResponseStatus ResponseStatus { get; set; }
        string ErrorMessage { get; set; }
        DbSet<TEntity> Entities { get; }
        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableNoTracking { get; }

        // Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);
        Task<TDto> AddAsync(TDto dto, CancellationToken cancellationToken, bool saveNow = true);
        Task<TDto> UpdateAsync(TDto dto, CancellationToken cancellationToken, bool saveNow = true);
        Task<TDto> DeleteAsync(TDto dto, CancellationToken cancellationToken, bool saveNow = true);
        Task<IList<TDto>> Get(TFilterDto filterDto, CancellationToken cancellationToken);
        // Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);
        // Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);
        // Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);
        // TEntity GetById(params object[] ids);
        // void Add(TEntity entity, bool saveNow = true);
        // void AddRange(IEnumerable<TEntity> entities, bool saveNow);
        // void Update(TEntity entity, bool saveNow = true);
        // void UpdateRange(IEnumerable<TEntity> entities, bool saveNow = true);
        // void Delete(TEntity entity, bool saveNow = true);
        // void DeleteRange(IEnumerable<TEntity> entities, bool saveNow = true);
        // void Attach(TEntity entity);
        // void Detach(TEntity entity);
        // Task LoadCollectionAsync<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken) where TProperty : class;
        // void LoadCollection<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty) where TProperty : class;
        // void LoadReference<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty) where TProperty : class;
        // Task LoadReferenceAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty, CancellationToken cancellationToken) where TProperty : class;

    }

        public interface IBaseDL<TEntity, TDto> : IBaseDL<TEntity, TDto, TDto>
        where TEntity : class, IEntity
        where TDto : class
    {
    }
}