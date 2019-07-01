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
    public class StatementBL : BaseBL<Statement, StatementDTO, StatementFilterDTO>
    {
        private readonly IBaseDL<Subsystem, SubsystemDTO, SubsystemFilterDTO> _SubsystemDL;
        private readonly IBaseDL<Statement, StatementDTO, StatementFilterDTO> _StatementDL;
        private readonly IBaseDL<StatementSubsystem, StatementSubsystemDTO, StatementSubsystemFilterDTO> _StatementSubsystemDL;
        public StatementBL(IBaseDL<Statement, StatementDTO, StatementFilterDTO> statementDL,
                        IBaseDL<StatementSubsystem, StatementSubsystemDTO, StatementSubsystemFilterDTO> statementSubsystemDL,
                        IBaseDL<Subsystem, SubsystemDTO, SubsystemFilterDTO> subsystemDL) : base(statementDL)
        {
            _StatementDL = statementDL;
            _StatementSubsystemDL = statementSubsystemDL;
            _SubsystemDL = subsystemDL;
        }
        private bool Validate(StatementDTO data)
        {
            return Validate<Validation.BLStatementValidator, StatementDTO>(data ?? new StatementDTO());
        }

        /// <summary>
        /// Check Data For Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool CheckDeletePermission(StatementDTO data)
        {
            //TODO:
            return true;
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

        /// <summary>
        /// Insert Statement In Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async override Task<StatementDTO> InsertAsync(StatementDTO data, CancellationToken cancellationToken)
        {
            Validate(data);

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
                Observers.ObserverStates.StatementAdd state = new Observers.ObserverStates.StatementAdd
                {
                    Statement = statementResponse,
                    User = User,
                };
                Notify(state);
            }

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
                Observers.ObserverStates.StatementSubsystemAdd state = new Observers.ObserverStates.StatementSubsystemAdd
                {
                    StatementSubsystem = val,
                    User = User,
                };
                Notify(state);
            }

            return statementResponse;
        }
        
        /// <summary>
        /// Insert List of Statement In Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async override Task<IList<StatementDTO>> InsertListAsync(IList<StatementDTO> data, CancellationToken cancellationToken)
        {
            foreach (var d in data)
            {
                Validate(d);
                d.CreateDate = DateTime.Now;
            }

            var StatementList = StatementGet(new DTO.StatementDTO { });
            for (int i = 0; i < data.Count; i++)
            {
                var Count = StatementList?.Where(s => s.TagName == data[i].TagName)?.ToList().Count;
                if (Count > 0) // Statement Is Exist
                {
                    var Statement = StatementList?.Where(s => s.TagName == data[i].TagName)?.FirstOrDefault();
                    data[i].ID = Statement?.ID;
                    if (Statement.SubsystemList.Select(s => s.ID).ToList().Contains(data[i]?.SubsystemList?.FirstOrDefault()?.ID))
                    {
                        data[i].SubsystemList.Clear();
                    }
                    data[i].SubsystemList.AddRange(Statement.SubsystemList);
                    //Statement.EnglishText = String.IsNullOrWhiteSpace(data[i]?.EnglishText) ? Statement?.EnglishText : data[i]?.EnglishText;
                    //Statement.ArabicText = String.IsNullOrWhiteSpace(data[i]?.ArabicText) ? Statement?.ArabicText : data[i]?.ArabicText;
                    //Statement.PersianText = String.IsNullOrWhiteSpace(data[i]?.PersianText) ? Statement?.PersianText : data[i]?.PersianText;

                    StatementUpdate(data[i]);
                }
            }
            var Response = StatementDA.StatementInsert(data);

            List<DTO.StatementDTO> respList = new List<DTO.StatementDTO>();
            foreach (var val in Response)
            {
                var resp = StatementGet(new DTO.StatementDTO { ID = val?.ID ?? 0 })?.FirstOrDefault();
                Observers.ObserverStates.StatementAdd state = new Observers.ObserverStates.StatementAdd
                {
                    Statement = resp ?? val,
                    User = User,
                };
                Notify(state);
                respList.Add(resp);
            }

            ResponseStatus = StatementDA.ResponseStatus;
            if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
            {
                ErrorMessage += StatementDA.ErrorMessage;
                return null;
            }

            return respList ?? Response;

        }
        /// <summary>
        /// StatementUpdate
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public DTO.StatementDTO StatementUpdate(DTO.StatementDTO data)
        {
            if (!(data.ID > 0))
            {
                ResponseStatus = Alsahab.Common.ResponseStatus.BusinessError;
                ErrorMessage = "Entered Statement is Mistake";
                return null;
            }

            DTO.StatementDTO Statementdto = new DTO.StatementDTO();
            Statementdto = StatementGet(new DTO.StatementDTO { ID = data.ID })?.FirstOrDefault();
            Statementdto = new DTO.StatementDTO
            {
                ID = data.ID,
                TagName = !string.IsNullOrWhiteSpace(data?.TagName) ? data?.TagName : Statementdto.TagName,
                ArabicText = !string.IsNullOrWhiteSpace(data.ArabicText) ? data.ArabicText : Statementdto.ArabicText,
                EnglishText = !string.IsNullOrWhiteSpace(data.EnglishText) ? data.EnglishText : Statementdto.EnglishText,
                PersianText = !string.IsNullOrWhiteSpace(data.PersianText) ? data.PersianText : Statementdto.PersianText,
                CreateDate = data?.CreateDate > DateTime.MinValue ? data?.CreateDate : Statementdto.CreateDate,
                IsDeleted = data.IsDeleted,
            };

            var Response = StatementDA.StatementUpdate(Statementdto);
            ResponseStatus = StatementDA.ResponseStatus;
            if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
            {
                ErrorMessage += StatementDA.ErrorMessage;
                return null;
            }

            if (data?.SubsystemList?.Count > 0)
            {
                var subsystemRes = StatementSubsystemDelete(new StatementSubsystemDTO { StatementID = data?.ID });
                ResponseStatus = StatementDA.ResponseStatus;
                if (ResponseStatus != ResponseStatus.Successful)
                {
                    ErrorMessage += StatementDA.ErrorMessage;
                    return null;
                }
                subsystemRes = StatementSubsystemInsert(data);
                ResponseStatus = StatementDA.ResponseStatus;
                if (ResponseStatus != ResponseStatus.Successful)
                {
                    ErrorMessage += StatementDA.ErrorMessage;
                    return null;
                }
            }
            var resp = StatementGet(new DTO.StatementDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
            Observers.ObserverStates.StatementEdit state = new Observers.ObserverStates.StatementEdit
            {
                Statement = resp ?? Response,
                User = User,
            };
            Notify(state);
            return resp ?? Response;
        }
        private List<StatementSubsystemDTO> StatementSubsystemDelete(StatementSubsystemDTO data)
        {
            if (!(data.ID > 0) && !(data?.StatementID > 0))
            {
                ResponseStatus = Alsahab.Common.ResponseStatus.BusinessError;
                ErrorMessage = "Entered Statement is Mistake";
                return null;
            }
            var subsystems = StatementDA.StatementSubsystemGet(data);

            foreach (var val in subsystems)
            {
                var response = StatementDA.StatementSubsystemDelete(val);
                Observers.ObserverStates.StatementSubsystemDelete state = new Observers.ObserverStates.StatementSubsystemDelete
                {
                    StatementSubsystem = response,
                    User = User,

                };
                Notify(state);
            }
            return subsystems;
        }
        private List<StatementSubsystemDTO> StatementSubsystemInsert(DTO.StatementDTO data)
        {
            var response = new List<StatementSubsystemDTO>();
            var statementSubsystems = data?.SubsystemList?.Select(s => new StatementSubsystemDTO { ID = 0, SubsystemID = s.ID, StatementID = data?.ID })?.ToList();

            foreach (var val in statementSubsystems)
            {
                var res = StatementDA.StatementSubsystemInsert(val);
                response.Add(res);
                Observers.ObserverStates.StatementSubsystemAdd state = new Observers.ObserverStates.StatementSubsystemAdd
                {
                    StatementSubsystem = res,
                    User = User,

                };
                Notify(state);
            }
            return response;
        }
        /// <summary>
        /// Delete Logicly
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public DTO.StatementDTO StatementDelete(DTO.StatementDTO data)
        {
            //Search For Use This Item Before Delete
            if (!DeletePermission(data))
            {
                ResponseStatus = Alsahab.Common.ResponseStatus.BusinessError;
                return null;
            }

            var statementSubsystemResponse = StatementSubsystemDelete(new StatementSubsystemDTO { StatementID = data?.ID });

            if (StatementDA.ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
            {
                ErrorMessage += StatementDA.ErrorMessage;
                return null;
            }

            data = StatementGet(data)?.FirstOrDefault();
            data.IsDeleted = true;
            var Response = StatementDA.StatementUpdate(data);

            var resp = StatementGet(new DTO.StatementDTO { ID = Response?.ID ?? 0, IsDeleted = true })?.FirstOrDefault();
            Observers.ObserverStates.StatementDelete state = new Observers.ObserverStates.StatementDelete
            {
                Statement = resp ?? Response,
                User = User,
            };
            Notify(state);

            ResponseStatus = StatementDA.ResponseStatus;
            if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
            {
                ErrorMessage += StatementDA.ErrorMessage;
                return null;
            }

            return resp ?? Response;
        }
        /// <summary>
        /// Delete physically
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public DTO.StatementDTO StatementDeleteComplete(DTO.StatementDTO data)
        {
            if (!DeletePermission(data))
            {
                ResponseStatus = Alsahab.Common.ResponseStatus.BusinessError;
                return null;
            }
            var Response = StatementDA.StatementDelete(data);

            var resp = StatementGet(new DTO.StatementDTO { ID = Response?.ID ?? 0, IsDeleted = true })?.FirstOrDefault();
            Observers.ObserverStates.StatementDelete state = new Observers.ObserverStates.StatementDelete
            {
                Statement = resp ?? Response,
                User = User,
            };
            Notify(state);

            ResponseStatus = StatementDA.ResponseStatus;
            if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
            {
                ErrorMessage += StatementDA.ErrorMessage;
                return null;
            }

            return resp ?? Response;
        }
    }
}
