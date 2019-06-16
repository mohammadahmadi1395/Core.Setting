
using Gostar.Common.Validation;
using Gostar.Setting.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gostar.Setting.DTO.Validation
{
    public class FormTypeValidator : AbstractValidator<FormTypeDTO>
    {

        public FormTypeValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.SubSystemID).NotEmpty();
            RuleFor(x => x.PublicCode).NotEmpty();
            RuleFor(x => x.PublicCode).MinimumLength(4);
            RuleFor(x => x.IsDeleted).NotEqual(true);
        }
    }
}
