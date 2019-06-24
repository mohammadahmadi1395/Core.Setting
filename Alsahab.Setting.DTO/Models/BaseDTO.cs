using System;
using FluentValidation;

namespace Alsahab.Setting.DTO
{
    public class BaseDTO
    {
        public long? ID { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? CreateDateFrom { get; set; }
        public DateTime? CreateDateTo { get; set; }
    }

    public interface IBaseValidator<TDto>
    where TDto : BaseDTO
    {        
        void ValidateDto();
    }

    public class BaseValidator<TDto> : AbstractValidator<TDto>, IBaseValidator<TDto>
    where TDto : BaseDTO
    {
        public BaseValidator()
        {
            ValidateDto();
        }
        public virtual void ValidateDto()
        {            
        }
    }

}