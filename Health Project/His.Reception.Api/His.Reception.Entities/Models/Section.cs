using System;
using System.Collections.Generic;

namespace His.Reception.Entities.Models
{
    public partial class Section
    {
        public Section()
        {
            Receptions = new HashSet<Receptions>();
            UserPermission = new HashSet<UserPermission>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public string No { get; set; }
        public string Note { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int? SuperVisorPersonelId { get; set; }

        public virtual ICollection<Receptions> Receptions { get; set; }
        public virtual ICollection<UserPermission> UserPermission { get; set; }
    }
}
