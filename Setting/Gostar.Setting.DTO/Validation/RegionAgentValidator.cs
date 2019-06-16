using Gostar.Common.Validation;
using Gostar.Setting.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gostar.Setting.DTO.Validation
{
    public class RegionAgentValidator : AbstractValidator<RegionAgentDTO>
    {
        public RegionAgentValidator()
        {
            RuleFor(x => x.RegionID).NotEmpty();
            RuleFor(x => x.AgentPersonID).NotEmpty();
            RuleFor(x => x.IsDeleted).NotEqual(true);
            RuleFor(x => x.StartDate).LessThan(x => x.EndDate).When(x=>x.StartDate>DateTime.MinValue && x.EndDate>DateTime.MinValue);
        }

  
    }
}
