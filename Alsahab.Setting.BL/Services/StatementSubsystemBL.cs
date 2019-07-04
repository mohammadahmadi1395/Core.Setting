using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Setting.DTO;
using Alsahab.Common;
using Alsahab.Setting.Entities.Models;
using Alsahab.Setting.Data.Interfaces;
using System.Threading;

namespace Alsahab.Setting.BL
{
    public class StatementSubsystemBL : BaseBL<StatementSubsystem, StatementSubsystemDTO, StatementSubsystemFilterDTO>
    {
        private readonly IBaseDL<StatementSubsystem, StatementSubsystemDTO, StatementSubsystemFilterDTO> _StatementSubsystemDL;
        public StatementSubsystemBL(IBaseDL<StatementSubsystem, StatementSubsystemDTO, StatementSubsystemFilterDTO> statementSubsystemDL) : base(statementSubsystemDL)
        {
            _StatementSubsystemDL = statementSubsystemDL;
        }
        private bool Validate(StatementSubsystemDTO data)
        {
            return Validate<Validation.BLStatementSubsystemValidator, StatementSubsystemDTO>(data ?? new StatementSubsystemDTO());
        }

        /// <summary>
        /// Insert StatementSubsystem In Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async override Task<StatementSubsystemDTO> InsertAsync(StatementSubsystemDTO data, CancellationToken cancellationToken)
        {
            Validate(data);

            data.CreateDate = DateTime.Now;
            var response = await _StatementSubsystemDL.InsertAsync(data, cancellationToken);

            response = await _StatementSubsystemDL.GetByIdAsync(cancellationToken, response?.ID);
            
            //TODO:
            // Observers.ObserverStates.StatementSubsystemAdd state = new Observers.ObserverStates.StatementSubsystemAdd
            // {
            //     StatementSubsystem = response,
            //     User = User,
            // };
            // Notify(state);

            return response;
        }
        /// <summary>
        /// Insert List of Statement In Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async override Task<IList<StatementSubsystemDTO>> InsertListAsync(IList<StatementSubsystemDTO> data, CancellationToken cancellationToken)
        {
            foreach (var d in data)
            {
                Validate(d);
                d.CreateDate = DateTime.Now;
            }

            var response = await _StatementSubsystemDL.InsertListAsync(data, cancellationToken);

            var respList = new List<StatementSubsystemDTO>();
            foreach (var val in response)
            {
                var resp = await _StatementSubsystemDL.GetByIdAsync(cancellationToken, val?.ID);
                
                //TODO:
                // Observers.ObserverStates.StatementSubsystemAdd state = new Observers.ObserverStates.StatementSubsystemAdd
                // {
                //     StatementSubsystem = resp ?? val,
                //     User = User,
                // };
                // Notify(state);

                respList.Add(resp);
            }

            return respList ?? response;

        }

        /// <summary>
        /// StatementSubsystem Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async override Task<StatementSubsystemDTO> UpdateAsync(StatementSubsystemDTO data, CancellationToken cancellationToken)
        {
            data = await MergeNewAndOldDataForUpdate(data, cancellationToken);

            Validate(data);

            var response = await _StatementSubsystemDL.UpdateAsync(data, cancellationToken);

            response = await _StatementSubsystemDL.GetByIdAsync(cancellationToken, response?.ID);

            //TODO:
            // Observers.ObserverStates.StatementSubsystemEdit state = new Observers.ObserverStates.StatementSubsystemEdit
            // {
            //     StatementSubsystem = response,
            //     User = User,
            // };
            // Notify(state);
            return response;
        }
        public async override Task<StatementSubsystemDTO> SoftDeleteAsync(StatementSubsystemDTO data, CancellationToken cancellationToken)
        {
            CheckDeletePermision(data, cancellationToken);

            data = await _StatementSubsystemDL.GetByIdAsync(cancellationToken, data.ID);
            data.IsDeleted = true;

            var response = await _StatementSubsystemDL.UpdateAsync(data, cancellationToken);
            
            //TODO:
            // Observers.ObserverStates.StatementSubsystemDelete state = new Observers.ObserverStates.StatementSubsystemDelete
            // {
            //     StatementSubsystem = response,
            //     User = User,
            // };
            // Notify(state);

            return response;
        }

        private bool CheckDeletePermision(StatementSubsystemDTO data, CancellationToken cancellationToken)
        {
            //TODO:
            return true;
        }
    }
}
