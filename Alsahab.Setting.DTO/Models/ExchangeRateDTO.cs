using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alsahab.Setting.DTO {
    public class ExchangeRateDTO : BaseDTO {
        public long? FromCurrencyID { get; set; }
        public long? ToCurrencyID { get; set; }
        public String FromCurrencyTitle { get; set; }
        public String FromCurrencySymbol { get; set; }
        public String ToCurrencyTitle { get; set; }
        public String ToCurrencySymbol { get; set; }
        public double? Ratio { get; set; }
        public DateTime? Year { get; set; }
    }

    public class ExchangeRateValidator : AbstractValidator<ExchangeRateDTO> {
        public ExchangeRateValidator () {
            RuleFor (x => x.FromCurrencyID).NotEmpty ();
            RuleFor (x => x.ToCurrencyID).NotEmpty ();
            RuleFor (x => x.Ratio).NotEmpty ();
            RuleFor (x => x.Year).NotEmpty ().GreaterThan (DateTime.MinValue);
            RuleFor (x => x.IsDeleted).NotEqual (true);
        }
    }

}