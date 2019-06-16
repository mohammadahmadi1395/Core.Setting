using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alsahab.Setting.DTO {
    public class CurrencyDTO : BaseDTO {
        public String Title { get; set; }
        public String Symbol { get; set; }
    }

    public class CurrencyValidator : AbstractValidator<CurrencyDTO> {
        public CurrencyValidator () {
            RuleFor (x => x.Title).NotEmpty ();
            RuleFor (x => x.Symbol).NotEmpty ();
            RuleFor (x => x.IsDeleted).NotEqual (true);
        }

    }

}