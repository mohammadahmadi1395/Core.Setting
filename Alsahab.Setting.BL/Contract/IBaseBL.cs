﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Alsahab.Common;
using System.Threading;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Entities;

namespace Alsahab.Setting.BL
{
    public interface IBaseBL<TEntity, TDto, TFilterDto> : IScopedDependency
        where TEntity : BaseEntity<TEntity, TDto, long>, IEntity
        where TDto : BaseDTO//class
        where TFilterDto : TDto
    {
        bool FormHasTree {get;set;}
        bool NeedToAutoCode {get;set;}
        UserInfoDTO User { get; set; }
        Language Language { get; set; }
        int? ResultCount { get; set; }
        PagingInfoDTO PagingInfo { get; set; }
        IList<FluentValidation.Results.ValidationFailure> ValidationErrors { get; set; }

        Task<IList<TDto>> GetAllAsync(CancellationToken cancellationToken, PagingInfoDTO paging = null);
        IList<TDto> GetAll(PagingInfoDTO paging = null);
        IList<TDto> Get(TFilterDto filter, PagingInfoDTO paging = null);
        Task<IList<TDto>> GetAsync(TFilterDto filter, CancellationToken cancellationToken, PagingInfoDTO paging = null);
        Task<TDto> InsertAsync(TDto data, CancellationToken cancellationToken);
        TDto Insert(TDto data);
        Task<IList<TDto>> InsertListAsync(IList<TDto> list, CancellationToken cancellationToken);
        IList<TDto> InsertList(IList<TDto> list);
        Task<TDto> UpdateAsync(TDto data, CancellationToken cancellationToken);
        TDto Update(TDto data);
        Task<IList<TDto>> UpdateListAsync(IList<TDto> list, CancellationToken cancellationToken);
        IList<TDto> UpdateList(IList<TDto> list);
        Task<TDto> DeleteAsync(TDto data, CancellationToken cancellationToken);
        TDto Delete(TDto data);
        Task<IList<TDto>> DeleteListAsync(IList<TDto> list, CancellationToken cancellationToken);
        IList<TDto> DeleteList(IList<TDto> list);
        Task<TDto> SoftDeleteAsync(TDto data, CancellationToken cancellationToken);
        TDto SoftDelete(TDto data);
        Task<IList<TDto>> SoftDeleteListAsync(IList<TDto> list, CancellationToken cancellationToken);
        IList<TDto> SoftDeleteList(IList<TDto> list);
    }

    public interface IBaseBL<TEntity, TDto> : IBaseBL<TEntity, TDto, TDto>
        where TEntity : BaseEntity<TEntity, TDto, long>, IEntity
        where TDto : BaseDTO
    {
    }

}
