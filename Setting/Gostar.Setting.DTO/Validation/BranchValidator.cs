using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;
using Gostar.Common.Validation;

namespace Gostar.Setting.DTO.Validation
{
    public class BranchValidator : AbstractValidator<BranchDTO>
    {
        public BranchValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(s => s.BranchAddressID).NotEmpty();
            RuleFor(x => x.IsDeleted).NotEqual(true);
        }
    }
}
