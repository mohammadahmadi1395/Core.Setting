using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Data;
using Alsahab.Setting.Entities.Models;
using Alsahab.Setting.Data.Interfaces;
using Alsahab.Common;
using System.Threading;

namespace Alsahab.Setting.BL
{
    public class RuleTagBL : BaseBL<RuleTag, RuleTagDTO, RuleTagFilterDTO>, IRuleTagBL
    {
        private readonly IBaseDL<RuleTag, RuleTagDTO, RuleTagFilterDTO> _RuleTagDL;
        public RuleTagBL(IBaseDL<RuleTag, RuleTagDTO, RuleTagFilterDTO> ruleTagDL) : base(ruleTagDL)
        {
            _RuleTagDL = ruleTagDL;
        }

        private bool Validate(RuleTagDTO data)
        {
            return Validate<Validation.BLRuleTagValidator, RuleTagDTO>(data ?? new RuleTagDTO());
        }
        private IList<RuleTagDTO> _AllRuleTags;
        private IList<RuleTagDTO> AllRuleTags
        {
            get
            {
                if (!(_AllRuleTags?.Count > 0))
                    _AllRuleTags = _RuleTagDL.GetAll();
                return _AllRuleTags;
            }
        }

        private bool IsExist(RuleTagDTO data)
        {
            return _AllRuleTags.Where(s => s.RuleID == data.RuleID && s.FormTypeID == data.FormTypeID)?.ToList()?.Count > 0;
        }
        private bool CheckDeletePermision(RuleTagDTO data)
        {
            //TODO:           
            return true;
        }
        public async override Task<IList<RuleTagDTO>> GetAsync(RuleTagFilterDTO filter, CancellationToken cancellationToken, PagingInfoDTO paging = null)
        {
            var response = await _RuleTagDL.GetAsync(filter, cancellationToken, paging);
            ResultCount = _RuleTagDL.ResultCount;
            return response;
        }
        public async override Task<RuleTagDTO> InsertAsync(RuleTagDTO data, CancellationToken cancellationToken)
        {
            Validate(data);

            data.CreateDate = DateTime.Now;
            var response = await _RuleTagDL.InsertAsync(data, cancellationToken);

            response = await _RuleTagDL.GetByIdAsync(cancellationToken, response?.ID ?? 0);
            
            //TODO:
            // Observers.ObserverStates.RuleTagAdd state = new Observers.ObserverStates.RuleTagAdd
            // {
            //     RuleTag = response,
            //     User = User,
            // };
            // Notify(state);

            return response;
        }

        public async override Task<IList<RuleTagDTO>> InsertListAsync(IList<RuleTagDTO> data, CancellationToken cancellationToken)
        {
            foreach (var d in data)
            {
                Validate(d);
                d.CreateDate = DateTime.Now;
            }

            var newData = new List<RuleTagDTO>();
            foreach (var val in data)
                if (!(IsExist(val)))
                    newData.Add(val);

            var response = await _RuleTagDL.GetAsync(new RuleTagFilterDTO { RuleID = data.FirstOrDefault().RuleID }, cancellationToken);

            foreach (var val in response)
            {
                bool exist = false;
                foreach (var newval in data)
                {
                    if (val.RuleID == newval.RuleID && val.FormTypeID == newval.FormTypeID)
                    {
                        exist = true;
                        break;
                    }
                }
                if (!exist)
                    await _RuleTagDL.DeleteAsync(val, cancellationToken);
            }

            response = await _RuleTagDL.InsertListAsync(newData, cancellationToken);

            return response;
        }

        public async override Task<RuleTagDTO> UpdateAsync(RuleTagDTO data, CancellationToken cancellationToken)
        {
            data = await MergeNewAndOldDataForUpdate(data, cancellationToken);

            Validate(data);

            var response = await _RuleTagDL.UpdateAsync(data, cancellationToken);

            response = await _RuleTagDL.GetByIdAsync(cancellationToken, response?.ID ?? 0);

            //TODO:
            // Observers.ObserverStates.RuleTagEdit state = new Observers.ObserverStates.RuleTagEdit
            // {
            //     RuleTag = response,
            //     User = User,
            // };
            // Notify(state);

            return response;
        }

        public async override Task<RuleTagDTO> SoftDeleteAsync(RuleTagDTO data, CancellationToken cancellationToken)
        {
            //Search For Use This Item Before Delete
            CheckDeletePermision(data);

            data.IsDeleted = true;
            var response = await _RuleTagDL.UpdateAsync(data, cancellationToken);

            //TODO:
            // Observers.ObserverStates.RuleTagDelete state = new Observers.ObserverStates.RuleTagDelete
            // {
            //     RuleTag = response,
            //     User = User,
            // };
            // Notify(state);

            return response;
        }

        public async Task<IList<RuleTagDTO>> SoftDeleteByRuleID(long RuleID, CancellationToken cancellationToken)
        {
            var data = await _RuleTagDL.GetAsync(new RuleTagFilterDTO { RuleID = RuleID }, cancellationToken);
            var temp = new List<RuleTagDTO>();

            foreach (var val in data)
            {
                if (!CheckDeletePermision(val))
                    continue;
                val.IsDeleted = true;
                temp.Add(val);
            }

            var response = await _RuleTagDL.UpdateListAsync(temp, cancellationToken);

            foreach (var val in response)
            {
                //TODO:
                // Observers.ObserverStates.RuleTagDelete state = new Observers.ObserverStates.RuleTagDelete
                // {
                //     RuleTag = val,
                //     User = User,
                // };
                // Notify(state);
            }

            return response;
        }
    }
}
