using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alsahab.Setting.DTO {
    public class TypeoforganizationDTO : BaseDTO {
        public string Title { get; set; }
    }

    public class TypeoforganizationValidator : AbstractValidator<TypeoforganizationDTO> {
        public TypeoforganizationValidator () {
            RuleFor (x => x.Title).NotEmpty ();
            RuleFor (x => x.IsDeleted).NotEqual (true);
        }
    }
}