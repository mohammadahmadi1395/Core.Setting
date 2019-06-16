using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gostar.Setting.DTO
{
    public class BaseDTO
    {
        public long? ID { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? FromCreateDate { get; set; }
        public DateTime? ToCreateDate { get; set; }
        public bool? IsDeleted { get; set; } = false;
    }
}
