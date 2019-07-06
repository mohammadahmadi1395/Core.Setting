using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Alsahab.Common;
using Alsahab.Setting.BL;
using Alsahab.Setting.BL.Validation;
using Alsahab.Setting.Data.Interfaces;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Entities.Models;

namespace Alsahab.Setting.BL
{
    public class OrganizationTypeBL : BaseBL<OrganizationType, OrganizationTypeDTO, OrganizationTypeFilterDTO>
    {
        private readonly IBaseDL<OrganizationType, OrganizationTypeDTO, OrganizationTypeFilterDTO> _OrganizationTypeDL;
        public OrganizationTypeBL(IBaseDL<OrganizationType, OrganizationTypeDTO, OrganizationTypeFilterDTO> organizationTypeDL,
                                IBaseDL<Entities.Models.Log, LogDTO, LogFilterDTO> logDL) : base(organizationTypeDL, logDL)
        {
            _OrganizationTypeDL = organizationTypeDL;
        }

        /// <summary>
        /// Check Data For Insert
        /// </summary>
        /// <param name="data">
        /// </param>
        /// <returns></returns>
        private bool Validate(OrganizationTypeDTO data)
        {

            return Validate<BLOrganizationTypeValidator, OrganizationTypeDTO>(data);
        }
        /// <summary>
        /// Check Data For Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool CheckDeletePermission(OrganizationTypeDTO data)
        {
            if (!(data.ID > 0))
            {
                // ErrorMessage = "Entered OrganizationType is Mistake";
                return false;
            }
            return true;
        }
        /// <summary>
        /// Get List of OrganizationType 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async override Task<IList<OrganizationTypeDTO>> GetAsync(OrganizationTypeFilterDTO data, CancellationToken cancellationToken, PagingInfoDTO paging = null)
        {
            var response = await _OrganizationTypeDL.GetAsync(data, cancellationToken, paging);
            ResultCount = _OrganizationTypeDL.ResultCount;
            return response;
        }

        /// <summary>
        /// Insert OrganizationType in Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async override Task<OrganizationTypeDTO> InsertAsync(OrganizationTypeDTO data, CancellationToken cancellationToken)
        {
            Validate(data);
            data.CreateDate = DateTime.Now;

            var response = await _OrganizationTypeDL.InsertAsync(data, cancellationToken);

            response = await _OrganizationTypeDL.GetByIdAsync(cancellationToken, response?.ID);

            //TODO:
            // Observers.ObserverStates.OrganizationTypeAdd state = new Observers.ObserverStates.OrganizationTypeAdd
            // {
            //     OrganizationType = response,
            //     User = User,
            // };
            // Notify(state);

            return response;
        }

        /// <summary>
        /// Insert List of OrganizationType In Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async override Task<IList<OrganizationTypeDTO>> InsertListAsync(IList<OrganizationTypeDTO> data, CancellationToken cancellationToken)
        {
            foreach (var d in data)
            {
                Validate(d);
                d.CreateDate = DateTime.Now;
            }

            var response = await _OrganizationTypeDL.InsertListAsync(data, cancellationToken);

            var respList = new List<OrganizationTypeDTO>();
            foreach (var val in response)
            {
                var temp = await _OrganizationTypeDL.GetByIdAsync(cancellationToken, val?.ID);

                //TODO:
                // Observers.ObserverStates.OrganizationTypeAdd state = new Observers.ObserverStates.OrganizationTypeAdd
                // {
                //     OrganizationType = val,
                //     User = User,
                // };
                // Notify(state);

                respList.Add(temp);
            }

            return respList ?? response;
        }

        /// <summary>
        /// Update OrganizationType
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async override Task<OrganizationTypeDTO> UpdateAsync(OrganizationTypeDTO data, CancellationToken cancellationToken)
        {
            data = await MergeNewAndOldDataForUpdateAsync(data, cancellationToken);
            Validate(data);

            var response = await _OrganizationTypeDL.UpdateAsync(data, cancellationToken);

            response = await _OrganizationTypeDL.GetByIdAsync(cancellationToken, response?.ID);

            //TODO:
            // Observers.ObserverStates.OrganizationTypeEdit state = new Observers.ObserverStates.OrganizationTypeEdit
            // {
            //     OrganizationType = response,
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
        public async override Task<OrganizationTypeDTO> SoftDeleteAsync(OrganizationTypeDTO data, CancellationToken cancellationToken)
        {
            CheckDeletePermission(data);
            data.IsDeleted = true;
            var response = await _OrganizationTypeDL.UpdateAsync(data, cancellationToken);

            //TODO:
            // Observers.ObserverStates.OrganizationTypeDelete state = new Observers.ObserverStates.OrganizationTypeDelete
            // {
            //     OrganizationType = data,
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
        public async override Task<OrganizationTypeDTO> DeleteAsync(OrganizationTypeDTO data, CancellationToken cancellationToken)
        {
            CheckDeletePermission(data);
            var response = await _OrganizationTypeDL.DeleteAsync(data, cancellationToken);

            //TODO:
            // Observers.ObserverStates.OrganizationTypeDelete state = new Observers.ObserverStates.OrganizationTypeDelete
            // {
            //     OrganizationType = data,
            //     User = User,
            // };
            // Notify(state);

            return data;
        }
    }
}
