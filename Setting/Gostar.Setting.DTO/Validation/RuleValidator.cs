using Gostar.Common.Validation;
using Gostar.Setting.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gostar.Setting.DTO.Validation
{
    public class RuleValidator : AbstractValidator<RuleDTO>
    {
        public RuleValidator()
        {
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Type).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.IsDeleted).NotEqual(true);
        }


    }
}
