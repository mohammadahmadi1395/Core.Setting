using System;
using System.Collections.Generic;

namespace His.Reception.Entities.Models
{
    public partial class Permissions
    {
        public Permissions()
        {
            UserPermission = new HashSet<UserPermission>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public string Note { get; set; }
        public string NoteLang2 { get; set; }
        public string PageAdress { get; set; }
        public string ModuleName { get; set; }

        public virtual ICollection<UserPermission> UserPermission { get; set; }
    }
}
