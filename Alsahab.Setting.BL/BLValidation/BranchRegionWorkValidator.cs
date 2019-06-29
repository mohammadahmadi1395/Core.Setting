using Alsahab.Setting.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Gostar.Setting.BL;
using Alsahab.Setting.Data.Interfaces;
using Alsahab.Setting.Entities.Models;

namespace Alsahab.Setting.BL.Validation
{
    internal class BLBranchRegionWorkValidator : Alsahab.Setting.DTO.BranchRegionWorkValidator
    {
        private readonly IBaseDL<BranchRegionWork, BranchRegionWorkDTO, BranchRegionWorkFilterDTO> _BranchRegionWorkDL;
        private readonly IBaseDL<Branch, BranchDTO, BranchFilterDTO> _BranchDL;
        public BLBranchRegionWorkValidator(IBaseDL<BranchRegionWork, BranchRegionWorkDTO, BranchRegionWorkFilterDTO> _branchRegionWorkDL,
                                IBaseDL<Branch, BranchDTO, BranchFilterDTO> _branchDL) : base()
        {
            _BranchRegionWorkDL = _branchRegionWorkDL;
            _BranchDL = _branchDL;
            RuleFor(x => x.BranchID).Must((DTO, BranchID) => BranchRegionCheck(DTO)).When(x => x.ZoneID > 0 && x.BranchID > 0);
        }
        private bool BranchRegionCheck(BranchRegionWorkDTO data)
        {
            var branch = _BranchDL.Get(new BranchFilterDTO { IsCentral = true })?.FirstOrDefault();
            if (!(branch.ID == data.BranchID))
            {
                var allRegions = _BranchRegionWorkDL.GetAll();
                foreach (var reg in allRegions)
                {
                    if (reg.BranchID == branch.ID)
                        continue;
                    if (reg.BranchID != data.BranchID && reg.ZoneAndParents.Contains(data.ZoneID ?? 0))
                        return false;
                    if (reg.BranchID != data.BranchID && reg.ZoneAndChilds.Contains(data.ZoneID ?? 0))
                        return false;
                }
            }
            return true;
        }
    }
}
