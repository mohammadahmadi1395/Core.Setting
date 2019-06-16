using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Common;
using FluentValidation;

namespace Alsahab.Setting.DTO.Models {
    public class RuleTagDTO : BaseDTO//<RuleTagDTO, RuleTag>
     {
        public long? RuleID { get; set; }
        public long? FormTypeID { get; set; }

        //
        public String RuleDescription { get; set; }
        public RuleType? RuleType { get; set; }
        public String FormTypeTitle { get; set; }
        public long? SubSystemID { get; set; }
        public String SubSystemTitle { get; set; }
    }

    public class RuleTagValidator : AbstractValidator<RuleTagDTO> {
        public RuleTagValidator () {
            RuleFor (x => x.RuleID).NotEmpty ();
            RuleFor (x => x.FormTypeID).NotEmpty ();
            RuleFor (x => x.IsDeleted).NotEqual (true);

        }
    }

}