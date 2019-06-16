using System;
using System.Collections.Generic;

namespace His.Reception.Entities.Models
{
    public partial class Users
    {
        public Users()
        {
            UserPermission = new HashSet<UserPermission>();
            UserRoles = new HashSet<UserRoles>();
        }

        public int Id { get; set; }
        public int? PersonId { get; set; }
        public bool? IsActive { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool? IsLimitByIp { get; set; }

        public virtual Person Person { get; set; }
        public virtual ICollection<UserPermission> UserPermission { get; set; }
        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}
