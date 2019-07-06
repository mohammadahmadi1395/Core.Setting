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
using Alsahab.Common.Exceptions;

namespace Alsahab.Setting.BL
{
    public class StatementBL : BaseBL<Statement, StatementDTO, StatementFilterDTO>
    {
        private readonly IBaseDL<Subsystem, SubsystemDTO, SubsystemFilterDTO> _SubsystemDL;
        private readonly IBaseDL<Statement, StatementDTO, StatementFilterDTO> _StatementDL;
        private readonly IBaseDL<StatementSubsystem, StatementSubsystemDTO, StatementSubsystemFilterDTO> _StatementSubsystemDL;
        public StatementBL(IBaseDL<Statement, StatementDTO, StatementFilterDTO> statementDL,
                        IBaseDL<StatementSubsystem, StatementSubsystemDTO, StatementSubsystemFilterDTO> statementSubsystemDL,
                        IBaseDL<Subsystem, SubsystemDTO, SubsystemFilterDTO> subsystemDL,
                        IBaseDL<Entities.Models.Log, LogDTO, LogFilterDTO> logDL) : base(statementDL, logDL)
        {
            _StatementDL = statementDL;
            _StatementSubsystemDL = statementSubsystemDL;
            _SubsystemDL = subsystemDL;
        }
        public async override Task<IList<StatementDTO>> GetAsync(StatementFilterDTO filter, CancellationToken cancellationToken, PagingInfoDTO paging = null)
        {
            var statementList = await _StatementDL.GetAsync(filter, cancellationToken, paging);
            statementList = await MapStatementSubsystemToStatement(statementList, filter, cancellationToken);
            ResultCount = _StatementDL.ResultCount;
            return statementList;
        }

        private async Task<IList<StatementDTO>> MapStatementSubsystemToStatement(IList<StatementDTO> statementList, StatementDTO data, CancellationToken cancellationToken)
        {
            var allStatementSubsystem = await _StatementSubsystemDL.GetAllAsync(cancellationToken);
            var allSubsystem = await _SubsystemDL.GetAllAsync(cancellationToken);

            foreach (var val in statementList)
            {
                var subsystemIdList = allStatementSubsystem?.Where(s => s.StatementID == val.ID)?.ToList()?.Select(t => t.SubsystemID)?.ToList();
                var subsystemList = new List<SubsystemDTO>();

                subsystemList = allSubsystem?.Where(s => subsystemIdList.Contains(s.ID))?.ToList();

                if (data?.FilterSubsystemID > 0 && !subsystemIdList.Contains(data?.FilterSubsystemID))
                    subsystemList = null;

                val.SubsystemList = subsystemList;
                val.SubsystemIDList = subsystemIdList;
                val.SubsystemNameList = string.Join(" , ", subsystemList?.Select(s => s.Name)?.ToList());

                // if (data?.FilterSubsystemID > 0)
                //     val.SubsystemNameList = string.Join(" , ", subsystemList?.Where(t => t.ID == data?.FilterSubsystemID)?.ToList()?.Select(s => s.Name)?.ToList());
                // else
                //     val.SubsystemNameList = string.Join(" , ", subsystemList?.Select(s => s.Name)?.ToList());

                // val.SubsystemIDList = subsystemList?.Select(s => (long?)s.ID)?.ToList();
            }
            // ResultCount = res.Count;

            // if (PagingInfo != null)
            // {
            //     if (PagingInfo.IsPaging)
            //     {
            //         int skip = (PagingInfo.Index - 1) * PagingInfo.Size;
            //         res = res.OrderBy(i => i.ID).Skip(skip).Take(PagingInfo.Size).ToList();
            //     }
            // }
            return statementList?.Where(s => s.SubsystemIDList?.Count > 0)?.ToList();
        }

        public async override Task<StatementDTO> InsertAsync(StatementDTO data, CancellationToken cancellationToken)
        {
            Validate<Validation.BLStatementValidator, StatementDTO>(data);

            var statementList = await _StatementDL.GetAsync(new StatementFilterDTO { TagName = data.TagName }, cancellationToken);
            var statementResponse = new StatementDTO();

            //بررسی می‌کند که آیا این عبارت قبلا تعریف شده است یا خیر؟
            var oldSubsystemIdList = new List<long?>();
            var oldStatement = statementList?.FirstOrDefault(s => s.TagName.Equals(data.TagName));
            if (oldStatement?.ID > 0) // Statement Is Exist
            {
                // اگر قبلا در دیتابیس وجود داشته باشد، لیست زیرسیستم‌های قبلی آن را می‌آورد، 
                var statementSubsystemList = await _StatementSubsystemDL.GetAsync(new StatementSubsystemFilterDTO { StatementID = oldStatement.ID }, cancellationToken);
                oldSubsystemIdList = statementSubsystemList?.Select(s => s.SubsystemID)?.ToList();
            }
            else
            {
                // اگر عبارت جدید باشد، آن را در دیتابیس ذخیره می‌کند
                data.CreateDate = DateTime.Now;
                statementResponse = await _StatementDL.InsertAsync(data, cancellationToken);

                RegisterLogAsync(data, ActionType.Insert, cancellationToken);
            }

            // اگر از قبل عبارت وجود داشته باشد، فقط زیرسیستم‌های جدید را برای این عبارت استخراج می‌کند تا درج شوند
            foreach (var val in oldSubsystemIdList)
                data.SubsystemIDList.Remove(val);

            // ساختن شیء از زیرسیستم‌های عبارت برای درج در دیتابیس
            var newStatementSubsystemList = new List<StatementSubsystemDTO>();
            foreach (var val in data.SubsystemIDList)
                newStatementSubsystemList.Add(new StatementSubsystemDTO { StatementID = statementResponse?.ID, SubsystemID = val });

            var statementSubsystemResponse = await _StatementSubsystemDL.InsertListAsync(newStatementSubsystemList, cancellationToken);

            //TODO:
            //Log List Insert
            
            return statementResponse;
        }

        public async override Task<IList<StatementDTO>> InsertListAsync(IList<StatementDTO> data, CancellationToken cancellationToken)
        {
            var result = new List<StatementDTO>();
            foreach (var d in data)
                result.Add(await InsertAsync(d, cancellationToken));
            return result;
        }

        /// <summary>
        /// StatementUpdate
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async override Task<StatementDTO> UpdateAsync(StatementDTO data, CancellationToken cancellationToken)
        {
            var temp = await MergeNewAndOldDataForUpdateAsync(data, cancellationToken);
            temp.SubsystemIDList = data.SubsystemIDList;
            data = temp;

            Validate(data);

            var statementResponse = await _StatementDL.UpdateAsync(data, cancellationToken);
            //TODO:
            // Observers.ObserverStates.StatementAdd state = new Observers.ObserverStates.StatementAdd
            // {
            //     Statement = statementResponse,
            //     User = User,
            // };
            // Notify(state);

            // لیست زیرسیستم‌های قبلی آن را می‌آورد، 
            var statementSubsystemList = await _StatementSubsystemDL.GetAsync(new StatementSubsystemFilterDTO { StatementID = data.ID }, cancellationToken);
            var oldSubsystemIdList = statementSubsystemList?.Select(s => s.SubsystemID)?.ToList();

            // اگر از قبل عبارت وجود داشته باشد، فقط زیرسیستم‌های جدید را برای این عبارت استخراج می‌کند تا درج شوند
            foreach (var val in oldSubsystemIdList)
                data.SubsystemIDList.Remove(val);

            // ساختن شیء از زیرسیستم‌های عبارت برای درج در دیتابیس
            var newStatementSubsystemList = new List<StatementSubsystemDTO>();
            foreach (var val in data.SubsystemIDList)
                newStatementSubsystemList.Add(new StatementSubsystemDTO { StatementID = statementResponse?.ID, SubsystemID = val });

            var statementSubsystemResponse = await _StatementSubsystemDL.InsertListAsync(newStatementSubsystemList, cancellationToken);

            foreach (var val in statementSubsystemResponse)
            {
                //TODO:
                // Observers.ObserverStates.StatementSubsystemAdd newState = new Observers.ObserverStates.StatementSubsystemAdd
                // {
                //     StatementSubsystem = val,
                //     User = User,
                // };
                // Notify(newState);
            }
            return statementResponse;
        }

        /// <summary>
        /// Delete Logicly
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async override Task<StatementDTO> SoftDeleteAsync(StatementDTO data, CancellationToken cancellationToken)
        {
            CheckDeletePermission(data);

            var statementSubsystemList = await _StatementSubsystemDL.GetAsync(new StatementSubsystemFilterDTO { StatementID = data.ID }, cancellationToken);
            foreach (var val in statementSubsystemList)
            {
                val.IsDeleted = true;
                var statementSubsystemResponse = await _StatementSubsystemDL.UpdateAsync(val, cancellationToken);
                //TODO:
                // var statesubstate = new Observers.ObserverStates.StatementSubsystemDelete
                // {
                //     StatementSubsystem = statementSubsystemResponse,
                //     User = User,
                // };
                // Notify(statesubstate);
            }

            data = await _StatementDL.GetByIdAsync(cancellationToken, data.ID);
            data.IsDeleted = true;
            var response = await _StatementDL.UpdateAsync(data, cancellationToken);

            //TODO:
            // Observers.ObserverStates.StatementDelete state = new Observers.ObserverStates.StatementDelete
            // {
            //     Statement = response,
            //     User = User,
            // };
            // Notify(state);

            return response;
        }

        // /// <summary>
        // /// Delete physically
        // /// </summary>
        // /// <param name="data"></param>
        // /// <returns></returns>
        // public async override Task<StatementDTO> DeleteAsync(StatementDTO data, CancellationToken cancellationToken)
        // {
        //     CheckDeletePermission(data);
        //     var response = _StatementDL.StatementDelete(data);

        //     var resp = StatementGet(new DTO.StatementDTO { ID = Response?.ID ?? 0, IsDeleted = true })?.FirstOrDefault();
        //     Observers.ObserverStates.StatementDelete state = new Observers.ObserverStates.StatementDelete
        //     {
        //         Statement = resp ?? Response,
        //         User = User,
        //     };
        //     Notify(state);

        //     ResponseStatus = StatementDA.ResponseStatus;
        //     if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
        //     {
        //         ErrorMessage += StatementDA.ErrorMessage;
        //         return null;
        //     }

        //     return resp ?? Response;
        // }
    }
}
