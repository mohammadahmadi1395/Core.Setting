using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Setting.DTO;
using Alsahab.Setting.DA;
using Alsahab.Setting.DA.Entities;
using Alsahab.Common;
using Alsahab.Setting.Entities.Models;

namespace Alsahab.Setting.BL
{
    public class StatementBL : BaseBL<Statement, StatementDTO, StatementFilterDTO>
    {
        StatementDA StatementDA = new StatementDA();
        private bool Validate(DTO.StatementDTO data)
        {

            return Validate<Validation.StatementValidator,DTO.StatementDTO>(data ?? new DTO.StatementDTO());
            //if (string.IsNullOrWhiteSpace(data.TagName))
            //{
            //    ErrorMessage = "Statement Tag Name Not Entered\n";
            //    return false;
            //}
            ////if (string.IsNullOrWhiteSpace(data.SubsystemName) && !(data.SubsystemID > 0))
            ////{
            ////    ErrorMessage = "Statement Subsystem Not Entered\n";
            ////    return false;
            ////}

            //if (data.IsDeleted == true)
            //{
            //    ErrorMessage = "Statement Not yet Save in Database\n";
            //    return false;
            //}
            ////var StatementList = StatementGet(new DTO.StatementDTO())?.ToList();
            ////var CheckStatement = StatementList?.Where(s => s.TagName == data?.TagName /*&& (s.SubsystemID == data?.SubsystemID || s.SubsystemName == data?.SubsystemName)*/)?.Count();
            ////if (CheckStatement > 0)
            ////{
            ////    ErrorMessage = "This Statement Is Exist\n";
            ////    return false;
            ////}

            //return true;
        }
        /// <summary>
        /// Check Data For Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool DeletePermission(DTO.StatementDTO data)
        {
            if (!(data.ID > 0))
            {
                ErrorMessage = "Entered Statement is Mistake";
                return false;
            }
            //StatementDA StatementDA = new StatementDA();
            //var StatementStatementIDCheck = StatementDA.StatementGet(new StatementDTO { ID = data.ID }).Count();
            //if ((StatementStatementIDCheck > 0))
            //{
            //    ErrorMessage = "This Statement use in another Tables,Please Delete  them First";
            //    return false;
            //}
            return true;
        }
        public List<DTO.StatementDTO> StatementGet(DTO.StatementDTO data)
        {
            var res = StatementDA.StatementGet(data);
            
            var Response = MapStatementSubsystemToStatement(res, data);
            //ResultCount = StatementDA.ResultCount;

            ResponseStatus = StatementDA.ResponseStatus;
            if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
            {
                ErrorMessage += StatementDA.ErrorMessage;
                return null;
            }

            return Response;
        }
        private List<DTO.StatementDTO> MapStatementSubsystemToStatement(List<StatementSubsystemDTO> response, DTO.StatementDTO data)
        {
            var res = response?.Select(s => new DTO.StatementDTO
            {
                ArabicText = s.ArabicText,
                CreateDate = s.CreateDate,
                EnglishText = s.EnglishText,
                ID = s.ID,
                IsDeleted = s.IsDeleted,
                PersianText = s.PersianText,
                TagName = s.TagName,
                //SubsystemList = response?.Where(t => t.StatementID == s.ID)?.ToList()?.Select(z => new SubsystemDTO
                //{ ID = z.ID,
                //IsActive
                //})
            })?.GroupBy(s => s.TagName)?.Select(t => t.FirstOrDefault())?.ToList();

            foreach (var val in res)
            {
                var idList = response?.Where(s => s.ID == val.ID)?.ToList()?.Select(t => t.SubsystemID)?.ToList();
                List<SubsystemDTO> SubsystemList = new List<SubsystemDTO>();
                SubsystemList = new SubsystemBL()?.SubsystemGet(null)?.Where(s => idList.Contains(s.ID))?.ToList();
                if (data?.FilterSubsystemID > 0)
                    SubsystemList = SubsystemList?.Where(s => SubsystemList?.Select(t => t.ID)?.ToList().Contains(data?.FilterSubsystemID) ?? false)?.ToList();

                val.SubsystemList = SubsystemList;
                if (data?.FilterSubsystemID > 0)
                    val.SubsystemNameList = string.Join(" , ", SubsystemList?.Where(t => t.ID == data?.FilterSubsystemID)?.ToList()?.Select(s => s.Name)?.ToList());
                else
                    val.SubsystemNameList = string.Join(" , ", SubsystemList?.Select(s => s.Name)?.ToList());
                val.SubsystemIDList = SubsystemList?.Select(s => (long?)s.ID)?.ToList();
            }
            ResultCount = res.Count;

            if (PagingInfo != null)
            {
                if (PagingInfo.IsPaging)
                {
                    int skip = (PagingInfo.Index - 1) * PagingInfo.Size;
                    res = res.OrderBy(i => i.ID).Skip(skip).Take(PagingInfo.Size).ToList();
                }
            }

            return res?.Where(s => s.SubsystemIDList?.Count > 0)?.ToList();
        }
        /// <summary>
        /// Insert Statement In Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public DTO.StatementDTO StatementInsert(DTO.StatementDTO data)
        {
            //validate data
            if (!Validate(data))
            {
                ResponseStatus = Alsahab.Common.ResponseStatus.BusinessError;
                return null;
            }
            data.CreateDate = DateTime.Now;
            var StatementList = StatementGet(new DTO.StatementDTO { });
            var Count = StatementList?.Where(s => s.TagName == data.TagName)?.ToList().Count;
            if (Count > 0) // Statement Is Exist
            {
                var Statement = StatementList?.Where(s => s.TagName == data.TagName)?.FirstOrDefault();
                List<SubsystemDTO> NewSubSustemList = new List<SubsystemDTO>();
                foreach (var val in data?.SubsystemList)
                {
                    if (!(bool)Statement.SubsystemIDList?.Contains(val.ID))
                    {
                        NewSubSustemList.Add(val);
                    }
                }
                Statement.SubsystemList = NewSubSustemList;
                data = Statement;
            }
            var Response = StatementDA.StatementInsert(data);
            ResponseStatus = StatementDA.ResponseStatus;
            if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
            {
                ErrorMessage += StatementDA.ErrorMessage;
                return null;
            }

            var subsystemRes = StatementSubsystemInsert(data);

            if (Response?.ID > 0)
            {
                var resp = StatementGet(new DTO.StatementDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
                Observers.ObserverStates.StatementAdd state = new Observers.ObserverStates.StatementAdd
                {
                    Statement = resp ?? Response,
                    User = User,
                };
                Notify(state);
                if (resp != null)
                    Response = resp;
            }
            return Response;
        }
        /// <summary>
        /// Insert List of Statement In Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<DTO.StatementDTO> StatementInsert(List<DTO.StatementDTO> data)
        {
            foreach (var d in data)
            {
                if (!Validate(d))
                {
                    ResponseStatus = Alsahab.Common.ResponseStatus.BusinessError;

                    return null;
                }
                d.CreateDate = DateTime.Now;
            }
            //data = (from d in data
            //        join s in new SubsystemDA().SubsystemGet(null)?.ToList()
            //        on d.SubsystemName equals s.Name
            //        select new StatementDTO
            //        {
            //            ID = d.ID,
            //            ArabicText = d.ArabicText,
            //            CreateDate = d.CreateDate,
            //            EnglishText = d.EnglishText,
            //            IsDeleted = d.IsDeleted,
            //            PersianText = d.PersianText,
            //            SubsystemID = s.ID,
            //            SubsystemName = s.Name,
            //            TagName = d.TagName,
            //            //TypeID = d.TypeID,
            //        })?.ToList();

            var StatementList = StatementGet(new DTO.StatementDTO { });
            for (int i = 0; i < data.Count; i++)
            {
                var Count = StatementList?.Where(s => s.TagName == data[i].TagName)?.ToList().Count;
                if (Count > 0) // Statement Is Exist
                {
                    var Statement = StatementList?.Where(s => s.TagName == data[i].TagName)?.FirstOrDefault();
                    data[i].ID = Statement?.ID;
                    if (Statement.SubsystemList.Select(s=>s.ID).ToList().Contains(data[i]?.SubsystemList?.FirstOrDefault()?.ID))
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
