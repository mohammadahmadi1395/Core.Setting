//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gostar.Setting.DA.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class ExchangeRate
    {
        public long ID { get; set; }
        public long FromCurrencyID { get; set; }
        public long ToCurrencyID { get; set; }
        public double Ratio { get; set; }
        public System.DateTime Year { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreateDate { get; set; }
    
        public virtual Currency Currency { get; set; }
        public virtual Currency Currency1 { get; set; }
    }
}
