using Alsahab.Common;
using Alsahab.Setting.DL.Interfaces;
using Alsahab.Setting.Entities;
using FluentValidation;

namespace Alsahab.Setting.BL.BLValidation
{
    public abstract class BaseBLValidator<TEntity, TDto, TFilterDto> : AbstractValidator<TDto>//: BaseDTOValidator<TDto>
       where TEntity : class, IEntity
       where TDto : BaseDTO
       where TFilterDto : TDto
    {
        private readonly IBaseDL<TEntity, TDto, TFilterDto> _BaseDL;
        public BaseBLValidator(IBaseDL<TEntity, TDto, TFilterDto> _BaseDL) : base()
        {
        }
    }
}