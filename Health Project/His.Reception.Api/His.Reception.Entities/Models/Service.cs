using System;
using System.Collections.Generic;

namespace His.Reception.Entities.Models
{
    public partial class Service
    {
        public Service()
        {
            ReceptionService = new HashSet<ReceptionService>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public decimal? Price { get; set; }
        public string InterNationalCode { get; set; }
        public string Note { get; set; }

        public virtual ICollection<ReceptionService> ReceptionService { get; set; }
    }
}
