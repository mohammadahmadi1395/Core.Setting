using System.Collections.Generic;
using System.Threading.Tasks;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Entities.Models;
using Alsahab.Setting.DL.Interfaces;
using Alsahab.Common;
using System.Threading;

namespace Alsahab.Setting.BL
{
    public class RuleTagBL : BaseBL<RuleTag, RuleTagDTO, RuleTagFilterDTO>, IRuleTagBL
    {
        private readonly IBaseDL<RuleTag, RuleTagDTO, RuleTagFilterDTO> _RuleTagDL;
        public RuleTagBL(IBaseDL<RuleTag, RuleTagDTO, RuleTagFilterDTO> ruleTagDL,
                        IBaseDL<Entities.Models.Log, LogDTO, LogFilterDTO> logDL) : base(ruleTagDL, logDL)
        {
            _RuleTagDL = ruleTagDL;
            NeedToAutoCode = true;
        }

        public async Task<IList<RuleTagDTO>> SoftDeleteByRuleID(long RuleID, CancellationToken cancellationToken)
        {
            var data = await _RuleTagDL.GetAsync(new RuleTagFilterDTO { RuleID = RuleID }, cancellationToken);
            var temp = new List<RuleTagDTO>();
            foreach (var val in data)
            {
                await CheckDeletePermisionAsync(val, cancellationToken);
                val.IsDeleted = true;
                temp.Add(val);
            }

            var response = await _RuleTagDL.UpdateListAsync(temp, cancellationToken);

            RegisterListLog(temp, ActionType.Update);
            
            return response;
        }
    }
}
