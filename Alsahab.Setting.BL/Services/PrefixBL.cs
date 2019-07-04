using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Data;
using Alsahab.Setting.Entities.Models;
using Alsahab.Setting.Data.Interfaces;
using System.Threading;
using Alsahab.Common;

namespace Alsahab.Setting.BL
{
    public class PrefixBL : BaseBL<Prefix, PrefixDTO, PrefixFilterDTO>
    {
        private readonly IBaseDL<Prefix, PrefixDTO, PrefixFilterDTO> _PrefixDL;
        public PrefixBL(IBaseDL<Prefix, PrefixDTO, PrefixFilterDTO> prefixDL) : base(prefixDL)
        {
            _PrefixDL = prefixDL;
        }
        private bool Validate(PrefixDTO data)
        {
            return Validate<Validation.BLPrefixValidator, PrefixDTO>(data ?? new PrefixDTO());
        }

        private bool CheckDeletePermision(PrefixDTO data)
        {
            //TODO:
            return true;
        }

        public async override Task<IList<PrefixDTO>> GetAsync(PrefixFilterDTO filter, CancellationToken cancellationToken, PagingInfoDTO paging = null)
        {
            var response = await _PrefixDL.GetAsync(filter, cancellationToken, paging);
            ResultCount = _PrefixDL.ResultCount;
            return response;
        }
        public async override Task<PrefixDTO> InsertAsync(PrefixDTO data, CancellationToken cancellationToken)
        {
            Validate(data);

            data.CreateDate = DateTime.Now;
            var response = await _PrefixDL.InsertAsync(data, cancellationToken);

            response = await _PrefixDL.GetByIdAsync(cancellationToken, response?.ID ?? 0);

            //TODO:
            // Observers.ObserverStates.PrefixAdd state = new Observers.ObserverStates.PrefixAdd
            // {
            //     Prefix = response,
            //     User = User,
            // };
            // Notify(state);

            return response;
        }
        
        public async override Task<IList<PrefixDTO>> InsertListAsync(IList<PrefixDTO> data, CancellationToken cancellationToken)
        {
            foreach (var d in data)
            {
                Validate(d);
                d.CreateDate = DateTime.Now;
            }
            var response = await _PrefixDL.InsertListAsync(data, cancellationToken);

            IList<PrefixDTO> respList = new List<PrefixDTO>();
            foreach (var val in response)
            {
                var resp = await _PrefixDL.GetByIdAsync(cancellationToken, val?.ID ?? 0);
                
                //TODO:
                // Observers.ObserverStates.PrefixAdd state = new Observers.ObserverStates.PrefixAdd
                // {
                //     Prefix = resp ?? val,
                //     User = User,
                // };
                // Notify(state);

                respList.Add(resp);
            }
            
            return respList ?? response;
        }
        
        public async override Task<PrefixDTO> UpdateAsync(PrefixDTO data, CancellationToken cancellationToken)
        {
            data = await MergeNewAndOldDataForUpdate(data, cancellationToken);
            
            Validate(data);

            var response = await _PrefixDL.UpdateAsync(data, cancellationToken);

            response = await _PrefixDL.GetByIdAsync(cancellationToken, response?.ID ?? 0);

            //TODO:            
            // Observers.ObserverStates.PrefixEdit state = new Observers.ObserverStates.PrefixEdit
            // {
            //     Prefix = response,
            //     User = User,
            // };
            // Notify(state);

            return response;
        }

        public async override Task<PrefixDTO> SoftDeleteAsync(PrefixDTO data, CancellationToken cancellationToken)
        {
            CheckDeletePermision(data);

            data.IsDeleted = true;
            var response = await _PrefixDL.UpdateAsync(data, cancellationToken);

            //TODO:
            // Observers.ObserverStates.PrefixDelete state = new Observers.ObserverStates.PrefixDelete
            // {
            //     Prefix = response,
            //     User = User,
            // };
            // Notify(state);

            return response;
        }
    }
}
