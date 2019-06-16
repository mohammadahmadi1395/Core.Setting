using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;
using Gostar.Common.Validation;

namespace Gostar.Setting.DTO.Validation
{
    public class TypeoforganizationValidator : AbstractValidator<TypeoforganizationDTO>
    {
        public TypeoforganizationValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.IsDeleted).NotEqual(true);
        }

   

    }
}
