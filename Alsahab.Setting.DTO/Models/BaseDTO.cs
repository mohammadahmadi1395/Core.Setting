using System;

namespace Alsahab.Setting.DTO
{
    public class BaseDTO
    {
        public long? ID {get;set;}
        public bool? IsDeleted {get;set;}
        public DateTime? CreateDate {get;set;}
    }
}