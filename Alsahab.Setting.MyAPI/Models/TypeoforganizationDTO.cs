using Alsahab.Setting.Entities.Models;
using Alsahab.Setting.WebFramework.Api;
using FluentValidation;

namespace Alsahab.Setting.MyAPI.Models
{
    public class TypeoforganizationDTO : BaseDTO<TypeoforganizationDTO, Typeoforganization> {
        public string Title { get; set; }
    }

    public class TypeoforganizationValidator : AbstractValidator<TypeoforganizationDTO> {
        public TypeoforganizationValidator () {
            RuleFor (x => x.Title).NotEmpty ();
            RuleFor (x => x.IsDeleted).NotEqual (true);
        }
    }
}