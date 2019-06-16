using System;
using System.Collections.Generic;

namespace His.Reception.Entities.Models
{
    public partial class Expertise
    {
        public Expertise()
        {
            Doctors = new HashSet<Doctors>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public string Note { get; set; }

        public virtual ICollection<Doctors> Doctors { get; set; }
    }
}
