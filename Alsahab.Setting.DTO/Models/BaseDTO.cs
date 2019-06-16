using System;

namespace Alsahab.Setting.DTO.Models
{
    public class BaseDTO : IHaveCustomMapping
    {
        public long? Id {get;set;}
        public bool? IsDeleted {get;set;}
        public DateTime? CreateDate {get;set;}
    }
}