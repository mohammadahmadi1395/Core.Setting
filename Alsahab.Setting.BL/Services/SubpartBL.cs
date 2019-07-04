using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Setting.DTO;
using Alsahab.Common;
using Alsahab.Setting.Data.Interfaces;
using Alsahab.Setting.Entities.Models;
using System.Threading;

namespace Alsahab.Setting.BL
{
    public class SubpartBL : BaseBL<Subpart, SubpartDTO, SubpartFilterDTO>
    {
        private readonly IBaseDL<Subpart, SubpartDTO, SubpartFilterDTO> _SubpartDL;
        public SubpartBL(IBaseDL<Subpart, SubpartDTO, SubpartFilterDTO> subpartDL) : base(subpartDL)
        {
            _SubpartDL = subpartDL;
        }
        private bool Validate(SubpartDTO data)
        {
            return Validate<Validation.BLSubpartValidator, SubpartDTO>(data);
        }

        /// <summary>
        /// Check Data For Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool CheckDeletePermission(SubpartDTO data)
        {
            if (!(data.ID > 0))
            {
                ErrorMessage = "Entered Subpart is Mistake";
                return false;
            }
            //TODO:
            //SubsystemDA SubsystemDA = new SubsystemDA();
            //var SubsystemIDCheck = SubsystemDA.SubsystemGet(new DTO.SubsystemDTO { ID = data.SubsystemID }).Count();
            //if ((SubsystemIDCheck > 0))
            //{
            //    ErrorMessage = "This Subpart use in another Tables,Please Delete  them First.\n";
            //    return false;
            //}
            return true;
        }

        public async override Task<IList<SubpartDTO>> GetAsync(SubpartFilterDTO data, CancellationToken cancellationToken, PagingInfoDTO paging)
        {
            var result = await _SubpartDL.GetAsync(data, cancellationToken, paging);
            ResultCount = _SubpartDL.ResultCount;
            return result;
        }

        /// <summary>
        /// Insert Subpart In Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async override Task<SubpartDTO> InsertAsync(SubpartDTO data, CancellationToken cancellationToken)
        {
            Validate(data);

            data.CreateDate = DateTime.Now;

            var response = await _SubpartDL.InsertAsync(data, cancellationToken);

            response = await _SubpartDL.GetByIdAsync(cancellationToken, response?.ID);

            //TODO:
            // Observers.ObserverStates.SubpartAdd state = new Observers.ObserverStates.SubpartAdd
            // {
            //     Subpart = response,
            //     User = User,
            // };
            // Notify(state);

            return response;
        }

        /// <summary>
        /// Insert List of Subpart In Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async override Task<IList<SubpartDTO>> InsertListAsync(IList<SubpartDTO> data, CancellationToken cancellationToken)
        {
            foreach (var d in data)
            {
                Validate(d);
                d.CreateDate = DateTime.Now;
            }
            
            var response = await _SubpartDL.InsertListAsync(data, cancellationToken);

            var respList = new List<SubpartDTO>();
            foreach (var val in response)
            {
                var resp = await _SubpartDL.GetByIdAsync(cancellationToken, val?.ID);

                //TODO:
                // Observers.ObserverStates.SubpartAdd state = new Observers.ObserverStates.SubpartAdd
                // {
                //     Subpart = resp ?? val,
                //     User = User,
                // };
                // Notify(state);

                respList.Add(resp);
            }

            return respList ?? response;

        }
        /// <summary>
        /// SubpartUpdate
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async override Task<SubpartDTO> UpdateAsync(SubpartDTO data, CancellationToken cancellationToken)
        {
            data = await MergeNewAndOldDataForUpdate(data, cancellationToken);
            
            Validate(data);

            var response = await _SubpartDL.UpdateAsync(data, cancellationToken);

            response = await _SubpartDL.GetByIdAsync(cancellationToken, response?.ID);

            //TODO:
            // Observers.ObserverStates.SubpartEdit state = new Observers.ObserverStates.SubpartEdit
            // {
            //     Subpart = response,
            //     User = User,
            // };
            // Notify(state);

            return response;
        }

        /// <summary>
        /// Delete Logicly
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async override Task<SubpartDTO> SoftDeleteAsync(SubpartDTO data, CancellationToken cancellationToken)
        {
            CheckDeletePermission(data);
            
            data.IsDeleted = true;
            
            var response = await _SubpartDL.UpdateAsync(data, cancellationToken);

            //TODO:
            // Observers.ObserverStates.SubpartDelete state = new Observers.ObserverStates.SubpartDelete
            // {
            //     Subpart = response,
            //     User = User,
            // };
            // Notify(state);

            return response;
        }

        /// <summary>
        /// Delete physically
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async override Task<SubpartDTO> DeleteAsync(SubpartDTO data, CancellationToken cancellationToken)
        {
            CheckDeletePermission(data);
            data = await _SubpartDL.GetByIdAsync(cancellationToken, data.ID);
            var response = await _SubpartDL.DeleteAsync(data, cancellationToken);

            //TODO:
            // Observers.ObserverStates.SubpartDelete state = new Observers.ObserverStates.SubpartDelete
            // {
            //     Subpart = data,
            //     User = User,
            // };
            // Notify(state);

            return data;
        }
    }
}
