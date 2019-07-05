using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Setting.DTO;
using Alsahab.Common;
using Alsahab.Setting.Data.Interfaces;
using Alsahab.Setting.Entities.Models;
using Alsahab.Setting.BL.Validation;
using System.Threading;
using Alsahab.Common.Exceptions;

namespace Alsahab.Setting.BL
{
    public class SubsystemBL : BaseBL<Subsystem, SubsystemDTO, SubsystemFilterDTO>
    {
        private readonly IBaseDL<Subsystem, SubsystemDTO, SubsystemFilterDTO> _SubsystemDL;
        private readonly IBaseDL<StatementSubsystem, StatementSubsystemDTO, StatementSubsystemFilterDTO> _StatementSubsystemDL;
        private readonly IBaseDL<Subpart, SubpartDTO, SubpartFilterDTO> _SubpartDL;
        public SubsystemBL(IBaseDL<Subsystem, SubsystemDTO, SubsystemFilterDTO> subsystemDL,
                           IBaseDL<StatementSubsystem, StatementSubsystemDTO, StatementSubsystemFilterDTO> statementSubsystemDL,
                           IBaseDL<Subpart, SubpartDTO, SubpartFilterDTO> subpartDL,
                           IBaseDL<Entities.Models.Log, LogDTO, LogFilterDTO> logDL) : base(subsystemDL, logDL)
        {
            _SubsystemDL = subsystemDL;
            _StatementSubsystemDL = statementSubsystemDL;
            _SubpartDL = subpartDL;
        }

        private bool Validate(SubsystemDTO data)
        {
            return Validate<BLSubsystemValidator, SubsystemDTO>(data);
        }

        /// <summary>
        /// Check Data For Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task<bool> CheckDeletePermission(SubsystemDTO data, CancellationToken cancellationToken)
        {
            if (!(data.ID > 0))
            {
                ErrorMessage = "Entered Subsystem is Mistake";
                return false;
            }
            var statementSubsystemIDList = await _StatementSubsystemDL.GetAsync(new StatementSubsystemFilterDTO { SubsystemID = data.ID }, cancellationToken);
            var subpartIDList = await _SubpartDL.GetAsync(new DTO.SubpartFilterDTO { SubsystemID = data.ID }, cancellationToken);

            if (statementSubsystemIDList.Count > 0 || subpartIDList.Count > 0)
                throw new AppException(ResponseStatus.LoginError, "This Subsystem use in another Tables,Please Delete  them First");

            return true;
        }

        public async override Task<IList<SubsystemDTO>> GetAsync(SubsystemFilterDTO data, CancellationToken cancellationToken, PagingInfoDTO paging)
        {
            return await _SubsystemDL.GetAsync(data, cancellationToken, paging);
        }

        /// <summary>
        /// Insert Subsystem In Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        // public async override Task<SubsystemDTO> InsertAsync(SubsystemDTO data, CancellationToken cancellationToken)
        // {
        //     Validate(data);
        //     data.CreateDate = DateTime.Now;
        //     var response = await _SubsystemDL.InsertAsync(data, cancellationToken);
        //     response = await _SubsystemDL.GetByIdAsync(cancellationToken, data.ID);
            
        //     //TODO:
        //     // Observers.ObserverStates.SubsystemAdd state = new Observers.ObserverStates.SubsystemAdd
        //     // {
        //     //     Subsystem = response,
        //     //     User = User,
        //     // };
        //     // Notify(state);

        //     return response;
        // }

        /// <summary>
        /// Insert List of Subsystem In Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async override Task<IList<SubsystemDTO>> InsertListAsync(IList<SubsystemDTO> data, CancellationToken cancellationToken)
        {
            foreach (var d in data)
            {
                Validate(d);
                d.CreateDate = DateTime.Now;
            }
            
            var response = await _SubsystemDL.InsertListAsync(data, cancellationToken);

            var respList = new List<SubsystemDTO>();
            foreach (var val in response)
            {
                var resp = await _SubsystemDL.GetByIdAsync(cancellationToken, val?.ID);
                
                //TODO:
                // Observers.ObserverStates.SubsystemAdd state = new Observers.ObserverStates.SubsystemAdd
                // {
                //     Subsystem = resp ?? val,
                //     User = User,
                // };
                // Notify(state);

                respList.Add(resp);
            }

            return respList ?? response;
        }

        /// <summary>
        /// SubsystemUpdate
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async override Task<SubsystemDTO> UpdateAsync(SubsystemDTO data, CancellationToken cancellationToken)
        {
            data = await MergeNewAndOldDataForUpdate(data, cancellationToken);
            Validate(data);
            var response = await _SubsystemDL.UpdateAsync(data, cancellationToken);

            response = await _SubsystemDL.GetByIdAsync(cancellationToken, response?.ID);
            
            //TODO:
            // Observers.ObserverStates.SubsystemEdit state = new Observers.ObserverStates.SubsystemEdit
            // {
            //     Subsystem = response,
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
        public async override Task<SubsystemDTO> SoftDeleteAsync(SubsystemDTO data, CancellationToken cancellationToken)
        {
            await CheckDeletePermission(data, cancellationToken);
            data.IsDeleted = true;

            var response = await _SubsystemDL.UpdateAsync(data, cancellationToken);

            //TODO:
            // Observers.ObserverStates.SubsystemDelete state = new Observers.ObserverStates.SubsystemDelete
            // {
            //     Subsystem = response,
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
        public async override Task<SubsystemDTO> DeleteAsync(SubsystemDTO data, CancellationToken cancellationToken)
        {
            await CheckDeletePermission(data, cancellationToken);
            data = await _SubsystemDL.GetByIdAsync(cancellationToken, data.ID);
            var response = await _SubsystemDL.DeleteAsync(data, cancellationToken);

            //TODO:
            // Observers.ObserverStates.SubsystemDelete state = new Observers.ObserverStates.SubsystemDelete
            // {
            //     Subsystem = data,
            //     User = User,
            // };
            // Notify(state);

            return data;
        }
    }
}
