using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alsahab.Setting.DTO {
    public class BranchRegionWorkDTO : BaseDTO {
        public long? BranchID { get; set; }
        public long? ZoneID { get; set; }

        public List<long> ZoneAndChilds { get; set; }
        public List<long> ZoneAndParents { get; set; }

    }

    public class BranchRegionWorkValidator : AbstractValidator<BranchRegionWorkDTO> {
        public BranchRegionWorkValidator () {
            RuleFor (x => x.ZoneID).NotEmpty ();
            RuleFor (x => x.BranchID).NotEmpty ();
            RuleFor (x => x.IsDeleted).NotEqual (true);
        }
    }

}