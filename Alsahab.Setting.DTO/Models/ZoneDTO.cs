using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Common;
using FluentValidation;

namespace Alsahab.Setting.DTO
{
    public class ZoneDTO : BaseDTO
    {
        public String Code { get; set; }
        public long? ParentID { get; set; }
        public String Title { get; set; }
        public Enums.ZoneType? Type { get; set; }
        public String Comment { get; set; }
        public String ZoneAddress { get; set; }
        public List<long> ZoneAndParents { get; set; }
        public List<long> ZoneAndChilds { get; set; }
        public string ParentTitle { get; set; }
        public long? LeftIndex { get; set; }
        public long? RightIndex { get; set; }
        public long? Depth { get; set; }
        public String OldCode { get; set; }
    }

    public class ZoneValidator : AbstractValidator<ZoneDTO>
    {

        public ZoneValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Type).NotNull();
            RuleFor(x => x.Type).IsInEnum();
            RuleFor(x => x.IsDeleted).NotEqual(true);
        }
    }

}