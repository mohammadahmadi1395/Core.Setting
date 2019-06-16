using Gostar.Common.Validation;
using Gostar.Setting.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gostar.Setting.DTO.Validation
{
    public class BranchRegionWorkValidator : AbstractValidator<BranchRegionWorkDTO>
    {
        public BranchRegionWorkValidator()
        {
            RuleFor(x => x.ZoneID).NotEmpty();
            RuleFor(x => x.BranchID).NotEmpty();
            RuleFor(x => x.IsDeleted).NotEqual(true);
        }
    }
}
