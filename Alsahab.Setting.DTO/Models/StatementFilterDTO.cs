using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using FluentValidation;
using Alsahab.Common;

namespace Alsahab.Setting.DTO
{
    public class StatementFilterDTO : StatementDTO
    {
        public DateTime? CreateDateFrom { get; set; }
        public DateTime? CreateDateTo { get; set; }
        public List<long?> IDList { get; set; }
    }
}