using System;
using System.Collections.Generic;

namespace Alsahab.Setting.Data.Models
{
    public partial class Typeoforganization
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
