
using Gostar.Common.Validation;
using Gostar.Setting.DA;
using Gostar.Setting.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gostar.Setting.BL.Validation
{
    internal class SectorValidator : Gostar.Setting.DTO.Validation.SectorValidator
    {
        public SectorValidator() : base()
        {
            RuleFor(x => x.RegionID).Must(RegionNotExist).When(x => x.RegionID > 0).WithMessage(ValidatorOptions.LanguageManager.GetString("NotExist"));
            RuleFor(x => x.Name).Must(NameNotExist).When(x => !string.IsNullOrWhiteSpace(x.Name)).WithMessage(ValidatorOptions.LanguageManager.GetString("NotExist"));
            RuleFor(x => x.Code).Must((DTO, Code) => CodeNotExist(Code, DTO.RegionID)).When(x => x.Code > 0 && x.RegionID > 0).WithMessage(ValidatorOptions.LanguageManager.GetString("NotExist"));

        }

        private bool RegionNotExist(long? regionID)
        {
            RegionDA RegionDA = new RegionDA();
            var RegionExist = RegionDA.RegionGet(new RegionDTO { ID = regionID }, null)?.Count();
            if (!(RegionExist > 0))
            {
                return false;
            }
            return true;
        }
        private bool NameNotExist(string name)
        {
            SectorBL SectorBL = new SectorBL();
            var result = SectorBL.SectorGet(new SectorDTO { Name = name });
            var Count = result?.Where(s => s.Name == name)?.Count();
            if (Count > 0)
            {
                return false;
            }
            return true;
        }
        private bool CodeNotExist(int? code, long? regionID)
        {
            SectorBL SectorBL = new SectorBL();
            var result = SectorBL.SectorGet(new SectorDTO { Code = code, RegionID = regionID });
            var Count = result?.Where(s => s.Code == code && s.CityID == regionID)?.Count();
            if (Count > 0)
            {
                return false;
            }
            return true;
        }
    }
}
