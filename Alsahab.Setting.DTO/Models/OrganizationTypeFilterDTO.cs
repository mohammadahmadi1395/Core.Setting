using System;
using System.Collections.Generic;
using FluentValidation;

namespace Alsahab.Setting.DTO
{
    public class OrganizationTypeFilterDTO : OrganizationTypeDTO
    {
        public DateTime? CreateDateFrom { get; set; }
        public DateTime? CreateDateTo { get; set; }
        public List<long?> IDList { get; set; }
    }

}