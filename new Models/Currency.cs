using System;
using System.Collections.Generic;

namespace Alsahab.Setting.Data.Models
{
    public partial class Currency
    {
        public Currency()
        {
            ExchangeRateFromCurrency = new HashSet<ExchangeRate>();
            ExchangeRateToCurrency = new HashSet<ExchangeRate>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string Symbol { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }

        public ICollection<ExchangeRate> ExchangeRateFromCurrency { get; set; }
        public ICollection<ExchangeRate> ExchangeRateToCurrency { get; set; }
    }
}
