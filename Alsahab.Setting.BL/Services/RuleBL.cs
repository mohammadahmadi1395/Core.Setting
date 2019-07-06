using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Data;
using Alsahab.Setting.BL;
using System.Data;
using Alsahab.Setting.Data.Interfaces;
using Alsahab.Setting.Entities.Models;
using System.Threading;
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
