using System.Collections.Generic;
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
        UserInfoDTO User { get; set; }
        Language Language { get; set; }
        ResponseStatus ResponseStatus { get; set; }
        int? ResultCount { get; set; }
        PagingInfoDTO PagingInfo { get; set; }
        string ErrorMessage { get; set; }
        IList<FluentValidation.Results.ValidationFailure> ValidationErrors { get; set; }
        // CultureInfo Culture { get; set; }

        Task<IList<TDto>> GetAllAsync(CancellationToken cancellationToken);
        IList<TDto> GetAll();
        IList<TDto> Get(TFilterDto filter);
        Task<IList<TDto>> GetAsync(TFilterDto filter, CancellationToken cancellationToken);
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
    }

    public interface IBaseBL<TEntity, TDto> : IBaseBL<TEntity, TDto, TDto>
        where TEntity : BaseEntity<TEntity, TDto, long>, IEntity
        where TDto : BaseDTO
    {
    }

}
