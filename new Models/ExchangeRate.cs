using System;
using System.Collections.Generic;

namespace Alsahab.Setting.Data.Models
{
    public partial class ExchangeRate
    {
        public long Id { get; set; }
        public long FromCurrencyId { get; set; }
        public long ToCurrencyId { get; set; }
        public double Ratio { get; set; }
        public DateTime Year { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }

        public Currency FromCurrency { get; set; }
        public Currency ToCurrency { get; set; }
    }
}
