using Gostar.Common.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gostar.Setting.DTO.Validation

{
    public class RuleTagValidator : AbstractValidator<RuleTagDTO>
    {
        public RuleTagValidator()
        {
            RuleFor(x => x.RuleID).NotEmpty();
            RuleFor(x => x.FormTypeID).NotEmpty();
            RuleFor(x => x.IsDeleted).NotEqual(true);

        }
    }
}
