using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Common;
using FluentValidation;

namespace Alsahab.Setting.DTO
{
    public class RuleDTO : BaseDTO//<RuleDTO, Rule>
    {
         public String Title { get; set; }
        public String Description { get; set; }
        public RuleType? Type { get; set; }
    }

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