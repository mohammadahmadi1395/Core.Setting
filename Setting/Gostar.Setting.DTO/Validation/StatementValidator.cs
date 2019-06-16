using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Common.Validation;

namespace Gostar.Setting.DTO.Validation
{
    public class StatementValidator : AbstractValidator<DTO.StatementDTO>
    {
        public StatementValidator()
        {

            RuleFor(x => x.TagName).NotEmpty();
            RuleFor(x => x.IsDeleted).NotEqual(true);
            RuleFor(x => x.SubsystemList).NotEmpty();
            RuleFor(x => x.ArabicText).NotEmpty();
            RuleFor(x => x.PersianText).NotEmpty();
            RuleFor(x => x.EnglishText).NotEmpty();

        }
    }
}
