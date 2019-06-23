using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Alsahab.Common;
using FluentValidation;
using Alsahab.Setting.Common.Validation;
using Alsahab.Setting.Common;
using System.Threading;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Entities;

namespace Alsahab.Setting.BL
{
    public interface IBaseBL<TEntity, TDto, TFilterDto> : IScopedDependency
    where TEntity : class, IEntity
    where TDto : BaseDTO
    where TFilterDto : TDto
    {
        ResponseStatus ResponseStatus { get; set; }
        int? ResultCount { get; set; }
        PagingInfoDTO PagingInfo { get; set; }
        string ErrorMessage { get; set; }
        IList<FluentValidation.Results.ValidationFailure> ValidationErrors { get; set; }
        CultureInfo Culture { get; set; }
        UserInfoDTO User { get; set; }

        Task<IList<TDto>> GetAsync(TFilterDto filter, CancellationToken cancellationToken);
        Task<TDto> InsertAsync(TDto data, CancellationToken cancellationToken);
        TDto Insert(TDto data);
        Task<List<TDto>> InsertListAsync(List<TDto> list, CancellationToken cancellationToken);
        List<TDto> InsertList(List<TDto> list);
        Task<TDto> UpdateAsync(TDto data, CancellationToken cancellationToken);
        TDto Update(TDto data);
        Task<List<TDto>> UpdateListAsync(List<TDto> list, CancellationToken cancellationToken);
        List<TDto> UpdateList(List<TDto> list);
        Task<TDto> DeleteAsync(TDto data, CancellationToken cancellationToken);
        TDto Delete(TDto data);
        Task<List<TDto>> DeleteListAsync(List<TDto> list, CancellationToken cancellationToken);
        List<TDto> DeleteList(List<TDto> list);
    }

    public interface IBaseBL<TEntity, TDto> : IBaseBL<TEntity, TDto, TDto>
    where TEntity : class, IEntity
    where TDto : BaseDTO
    {
    }

}
