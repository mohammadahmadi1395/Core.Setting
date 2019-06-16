using System;
using System.Collections.Generic;

namespace His.Reception.Entities.Models
{
    public partial class Role
    {
        public Role()
        {
            UserRoles = new HashSet<UserRoles>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }

        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}
