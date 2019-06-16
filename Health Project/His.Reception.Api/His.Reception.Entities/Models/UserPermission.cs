using System;
using System.Collections.Generic;

namespace His.Reception.Entities.Models
{
    public partial class UserPermission
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? PermissionId { get; set; }
        public int SectionId { get; set; }

        public virtual Permissions Permission { get; set; }
        public virtual Section Section { get; set; }
        public virtual Users User { get; set; }
    }
}
