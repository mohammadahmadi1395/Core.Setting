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
using Alsahab.Common;

namespace Alsahab.Setting.Data.Contracts
{
    public interface IBaseDL<TEntity, TDto, TFilterDto>
        where TEntity : class, IEntity
        where TDto : BaseDTO
        where TFilterDto : TDto
    {        
        ResponseStatus ResponseStatus { get; set; }
        string ErrorMessage { get; set; }
        DbSet<TEntity> Entities { get; }
        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableNoTracking { get; }

        // Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);
        Task<TDto> InsertAsync(TDto dto, CancellationToken cancellationToken, bool saveNow = true);
        Task<TDto> UpdateAsync(TDto dto, CancellationToken cancellationToken, bool saveNow = true);
        Task<TDto> DeleteAsync(TDto dto, CancellationToken cancellationToken, bool saveNow = true);
        Task<IList<TDto>> GetAsync(TFilterDto filterDto, CancellationToken cancellationToken);
        Task<IList<TDto>> GetAllAsync(CancellationToken cancellationToken);
        IList<TDto> GetAll();
        TDto Insert(TDto dto, bool saveNow = true);
        TDto Update(TDto dto, bool saveNow = true);
        TDto Delete(TDto dto, bool saveNow = true);
        IList<TDto> Get(TFilterDto filterDto);
        Task<IList<TDto>> InsertListAsync(IList<TDto> dtoList, CancellationToken cancellationToken, bool saveNow = true);
        Task<IList<TDto>> UpdateListAsync(IList<TDto> dtoList, CancellationToken cancellationToken, bool saveNow = true);
        Task<IList<TDto>> DeleteListAsync(IList<TDto> dtoList, CancellationToken cancellationToken, bool saveNow = true);
        // TEntity GetById(params object[] ids);
        IList<TDto> InsertList(IList<TDto> dtoList, bool saveNow);
        IList<TDto> UpdateList(IList<TDto> dtoList, bool saveNow = true);
        IList<TDto> DeleteList(IList<TDto> dtoList, bool saveNow = true);
        // void Attach(TEntity entity);
        // void Detach(TEntity entity);
        // Task LoadCollectionAsync<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken) where TProperty : class;
        // void LoadCollection<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty) where TProperty : class;
        // void LoadReference<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty) where TProperty : class;
        // Task LoadReferenceAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty, CancellationToken cancellationToken) where TProperty : class;
    }

        public interface IBaseDL<TEntity, TDto> : IBaseDL<TEntity, TDto, TDto>
        where TEntity : class, IEntity
        where TDto : BaseDTO
    {
    }
}