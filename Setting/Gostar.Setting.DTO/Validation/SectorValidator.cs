
using Gostar.Common.Validation;
using Gostar.Setting.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gostar.Setting.DTO.Validation
{
    public class SectorValidator : AbstractValidator<SectorDTO>
    {
        public SectorValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.IsDeleted).NotEqual(true);
            RuleFor(x => x.RegionID).NotEmpty();
        }
    }
}
