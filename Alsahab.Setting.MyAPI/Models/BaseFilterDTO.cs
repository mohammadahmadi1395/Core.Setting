using System;

namespace Alsahab.Setting.MyAPI.Models
{
    public class BaseFilterDTO
    {
        public long? Id { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreateDateFrom { get; set; }
        public DateTime? CreateDateTo { get; set; }
    }
}