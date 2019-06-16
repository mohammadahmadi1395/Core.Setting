using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;
using Gostar.Common.Validation;

namespace Gostar.Setting.DTO.Validation
{
    public class ZoneValidator : AbstractValidator<ZoneDTO>
    {
   
        public ZoneValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Type).NotNull();
            RuleFor(x => x.Type).IsInEnum();
            RuleFor(x => x.IsDeleted).NotEqual(true);
        }
  

    }
}
