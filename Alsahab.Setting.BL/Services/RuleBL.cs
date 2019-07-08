using Alsahab.Setting.DTO;
using Alsahab.Setting.DL.Interfaces;
using Alsahab.Setting.Entities.Models;
using Alsahab.Common;

namespace Alsahab.Setting.BL
{
    public class RuleBL : BaseBL<Alsahab.Setting.Entities.Models.Rule, RuleDTO, RuleFilterDTO>
    {
        private readonly IBaseDL<Alsahab.Setting.Entities.Models.Rule, RuleDTO, RuleFilterDTO> _RuleDL;
        private readonly IBaseDL<RuleTag, RuleTagDTO, RuleTagFilterDTO> _RuleTagDL;
        public RuleBL(IBaseDL<Alsahab.Setting.Entities.Models.Rule, RuleDTO, RuleFilterDTO> ruleDL,
                    IBaseDL<RuleTag, RuleTagDTO, RuleTagFilterDTO> ruleTagDL,
                    IBaseDL<Entities.Models.Log, LogDTO, LogFilterDTO> logDL) : base(ruleDL, logDL)
        {
            _RuleDL = ruleDL;
            _RuleTagDL = ruleTagDL;
        }
    }
}
