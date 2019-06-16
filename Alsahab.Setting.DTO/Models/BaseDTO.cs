using System;

namespace Alsahab.Setting.DTO
{
    public class BaseDTO
    {
        public BaseDTO()
        {  
            IsDeleted = false;
            CreateDate = DateTime.Now;            
        }
        public long Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool? IsDeleted { get; set; }
    }
}