using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;
using Gostar.Common.Validation;
using System.Globalization;

namespace Gostar.Setting.DTO.Validation
{
    
   public class PrefixValidator : AbstractValidator<PrefixDTO>
    {

        public PrefixValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.IsDefault).NotEmpty();
            RuleFor(x => x.IsDeleted).NotEqual(true);
        }

    }
}
