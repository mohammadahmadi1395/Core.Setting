using System;

namespace Alsahab.Setting.DTO
{
    public class BaseFilterDTO
    {
        public long? ID { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreateDateFrom { get; set; }
        public DateTime? CreateDateTo { get; set; }
    }
}