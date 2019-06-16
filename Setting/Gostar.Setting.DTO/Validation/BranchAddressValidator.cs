using Gostar.Common.Validation;
using Gostar.Setting.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gostar.Setting.DTO.Validation
{
    public class BranchAddressValidator : AbstractValidator<BranchAddressDTO>
    {
        public BranchAddressValidator()
        {
            //RuleFor(x => x.Address).NotEmpty();

            RuleFor(x => x.IsDeleted).NotEqual(true);

        }
    }
}
