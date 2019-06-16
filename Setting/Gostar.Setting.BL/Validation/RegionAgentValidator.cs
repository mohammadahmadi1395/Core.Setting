using Gostar.Common.Validation;
using Gostar.Setting.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gostar.Setting.BL.Validation
{
    internal class RegionAgentValidator : Gostar.Setting.DTO.Validation.RegionAgentValidator
    {
        public RegionAgentValidator() : base()
        {

            RuleFor(x => x.RegionID).Must(RegionError);

        }

        private bool RegionError(long? regionID)
        {
            RegionAgentBL RegionAgentBL = new RegionAgentBL();
            var Res = RegionAgentBL.RegionAgentGet(new RegionAgentDTO { RegionID = regionID }, null);
            foreach (var val in Res)
            {
                if (val.EndDate == null || val.EndDate == DateTime.MinValue)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
