using System;
using System.Collections.Generic;

namespace His.Reception.Entities.Models
{
    public partial class RefferFrom
    {
        public RefferFrom()
        {
            Receptions = new HashSet<Receptions>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public string Note { get; set; }
        public bool? IsAdmin { get; set; }
        public string Code1 { get; set; }
        public string Code2 { get; set; }

        public virtual ICollection<Receptions> Receptions { get; set; }
    }
}
