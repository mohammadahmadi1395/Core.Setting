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
            IBaseDL<RuleTag, RuleTagDTO, RuleTagFilterDTO> ruleTagDL) : base(ruleDL)
        {
            _RuleDL = ruleDL;
            _RuleTagDL = ruleTagDL;
        }
        
        private bool Validate(RuleDTO data)
        {
            return Validate<Validation.BLRuleValidator, RuleDTO>(data ?? new RuleDTO());
        }
        private bool CheckDeletePermision(RuleDTO data)
        {
            //TODO:
            return true;
        }
        public async override Task<IList<RuleDTO>> GetAsync(RuleFilterDTO filter, CancellationToken cancellationToken, PagingInfoDTO paging = null)
        {
            var response = await _RuleDL.GetAsync(filter, cancellationToken, paging);
            ResultCount = _RuleDL.ResultCount;
            return response;
        }
        public async override Task<RuleDTO> InsertAsync(RuleDTO data, CancellationToken cancellationToken)
        {
            Validate(data);

            data.CreateDate = DateTime.Now;
            var response = await _RuleDL.InsertAsync(data, cancellationToken);

            response = await _RuleDL.GetByIdAsync(cancellationToken, response?.ID ?? 0);
            Observers.ObserverStates.RuleAdd state = new Observers.ObserverStates.RuleAdd
            {
                Rule = response,
                User = User,
            };
            Notify(state);

            return response;
        }

        public async override Task<IList<RuleDTO>> InsertListAsync(IList<RuleDTO> data, CancellationToken cancellationToken)
        {
            foreach (var d in data)
            {
                Validate(d);
                d.CreateDate = DateTime.Now;
            }

            var response = await _RuleDL.InsertListAsync(data, cancellationToken);

            var respList = new List<RuleDTO>();
            foreach (var val in response)
            {
                var resp = await _RuleDL.GetByIdAsync(cancellationToken, val?.ID ?? 0);
                Observers.ObserverStates.RuleAdd state = new Observers.ObserverStates.RuleAdd
                {
                    Rule = resp ?? val,
                    User = User,
                };
                Notify(state);
                respList.Add(resp);
            }

            return respList ?? response;
        }

        public async override Task<RuleDTO> UpdateAsync(RuleDTO data, CancellationToken cancellationToken)
        {
            data = await MergeNewAndOldDataForUpdate(data, cancellationToken);

            Validate(data);

            var response = await _RuleDL.UpdateAsync(data, cancellationToken);

            response = await _RuleDL.GetByIdAsync(cancellationToken, response?.ID ?? 0);

            Observers.ObserverStates.RuleEdit state = new Observers.ObserverStates.RuleEdit
            {
                Rule = response,
                User = User,
            };
            Notify(state);

            return response;
        }

        public async override Task<RuleDTO> SoftDeleteAsync(RuleDTO data, CancellationToken cancellationToken)
        {
            CheckDeletePermision(data);

            data.IsDeleted = true;
            var response = await _RuleDL.UpdateAsync(data, cancellationToken);

            Observers.ObserverStates.RuleDelete state = new Observers.ObserverStates.RuleDelete
            {
                Rule = response,
                User = User,
            };
            Notify(state);

            return response;
        }
    }
}
