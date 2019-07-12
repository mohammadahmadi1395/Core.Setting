using Alsahab.Common;
using FluentValidation;

namespace Alsahab.Setting.DTO
{
    public class OrganizationTypeDTO : BaseDTO
    {
         public string Title { get; set; }
    }

    public class OrganizationTypeValidator : AbstractValidator<OrganizationTypeDTO>
    {
        public OrganizationTypeValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.IsDeleted).NotEqual(true);
        }
    }
}