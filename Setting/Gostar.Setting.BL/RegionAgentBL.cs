using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DA;
using Gostar.Setting.DTO;

namespace Gostar.Setting.BL
{
    public class RegionAgentBL : BaseBusiness
    {
        RegionAgentDA RegionAgentDA = new RegionAgentDA();
        /// <summary>
        /// Check Data For Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool Validate(RegionAgentDTO data)
        {
            return Validate<Validation.RegionAgentValidator,RegionAgentDTO>(data ?? new RegionAgentDTO());
            //if (data?.IsDeleted == true)
            //{
            //    ErrorMessage = "RegionAgent Not yet Save in Database\n";
            //    return false;
            //}
            //if (data?.StartDate >= data?.EndDate)
            //{
            //    ErrorMessage = "RegionAgent Start and Ende Date Not Enter Correctly\n";
            //    return false;
            //}
            //if (!(data?.RegionID > 0))
            //{
            //    ErrorMessage = "Region is Not Defined\n";
            //    return false;
            //}

            //else
            //{
            //    //RegionDA RegionDA = new RegionDA();
            //    //var RegionExist = RegionDA.RegionGet(new RegionDTO { ID = data.RegionID ?? 0 }, null)?.Count();
            //    //if (!(RegionExist > 0))
            //    //{
            //    //    ErrorMessage = "This Region Not Exist\n";
            //    //    return false;
            //    //}
            //}
            //if (!(data?.AgentPersonID > 0))
            //{
            //    ErrorMessage = "Person is Not Defined\n";
            //    return false;
            //}
            //else
            //{
            //    //PersonDA PersonDA = new PersonDA();
            //    //var PersonExist = PersonDA.PersonGet(new PersonDTO { ID = data.PersonID ?? 0 }, null)?.Count();
            //    //if (!(PersonExist > 0))
            //    //{
            //    //    ErrorMessage = "This Person Not Exist\n";
            //    //    return false;
            //    //}
            //}
            //var Res = RegionAgentGet(new RegionAgentDTO { RegionID = data?.RegionID}, null);
            //foreach(var val in Res)
            //{
            //    if(val.EndDate == null || val.EndDate==DateTime.MinValue)
            //    {
            //        ErrorMessage = "Before you insert a new agent, first update the previous agent's date.";
            //        return false;
            //    }
            //}

            //return true;
        }
        /// <summary>
        /// Check Data For Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool DeletePermission(RegionAgentDTO data)
        {
            if (!(data.ID > 0))
            {
                ErrorMessage = "Entered RegionAgent is Mistake";
                return false;
            }
            return true;
        }
        /// <summary>
        /// Get List of RegionAgent 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<RegionAgentDTO> RegionAgentGet(RegionAgentDTO data, RegionAgentFilterDTO filter = null)
        {
            var Response = RegionAgentDA.RegionAgentGet(data, filter);

            ResponseStatus = RegionAgentDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += RegionAgentDA.ErrorMessage;
                return null;
            }

            if (Response?.Count > 0)
            {
                var personList = ServiceUtility.CallMember(s => s.Person(new Alyatim.Member.SC.Messages.PersonRequest
                {
                    User = User,
                    //                    
                    ActionType = Gostar.Common.ActionType.Select,
                    RequestDto = new Alyatim.Member.DTO.PersonDTO
                    {
                        FatherName = data?.AgentFatherName,
                        Name = data?.AgentName,
                        GrandFatherName = data?.AgentGrandFatherName,
                        MobileNo = data?.AgentMobile,
                    },
                    PersonFilter = new Alyatim.Member.DTO.PersonFilterDTO
                    {
                        IDList = Response?.Select(t => t.AgentPersonID)?.ToList(),
                    }
                }))?.ResponseDtoList;
                Response = (from r in Response
                            join p in personList on r.AgentPersonID equals p.ID
                            select new RegionAgentDTO
                            {
                                AgentFatherName = p.FatherName,
                                AgentGrandFatherName = p.GrandFatherName,
                                AgentMobile = p.MobileNo,
                                AgentName = p.Name,
                                AgentPersonID = p.ID,
                                AreaID = r.AreaID,
                                AreaName = r.AreaName,
                                CityID = r.CityID,
                                CityName = r.CityName,
                                CountryID = r.CountryID,
                                CountryName = r.CountryName,
                                CreateDate = r.CreateDate,
                                EndDate = r.EndDate,
                                ID = r.ID,
                                IsDeleted = r.IsDeleted,
                                RegionID = r.RegionID,
                                RegionName = r.RegionName,
                                StartDate = r.StartDate,
                                CityAreaRegionCode = r.CityAreaRegionCode,
                            })?.ToList();
            }
            return Response;
        }
        /// <summary>
        /// Insert RegionAgent in Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public RegionAgentDTO RegionAgentInsert(RegionAgentDTO data)
        {
            if (!Validate(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }

            data.CreateDate = DateTime.Now;
            var Response = RegionAgentDA.RegionAgentInsert(data);

            if (Response?.ID > 0)
            {
                var resp = RegionAgentGet(new RegionAgentDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
                Observers.ObserverStates.RegionAgentAdd state = new Observers.ObserverStates.RegionAgentAdd
                {
                    RegionAgent = resp ?? Response,
                    User = User,
                };
                Notify(state);
                if (resp != null)
                    Response = resp;
            }

            ResponseStatus = RegionAgentDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += RegionAgentDA.ErrorMessage;
                return null;
            }

            return Response;
        }
        /// <summary>
        /// Insert List of RegionAgent In Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<RegionAgentDTO> RegionAgentInsert(List<RegionAgentDTO> data)
        {
            foreach (var d in data)
            {
                if (!Validate(d))
                {
                    ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                    return null;
                }
                d.CreateDate = DateTime.Now;
            }
            var Response = RegionAgentDA.RegionAgentInsert(data);

            List<RegionAgentDTO> respList = new List<RegionAgentDTO>();
            foreach (var val in Response)
            {
                var resp = RegionAgentGet(new RegionAgentDTO { ID = val?.ID ?? 0 })?.FirstOrDefault();
                Observers.ObserverStates.RegionAgentAdd state = new Observers.ObserverStates.RegionAgentAdd
                {
                    RegionAgent = resp ?? val,
                    User = User,
                };
                Notify(state);
                respList.Add(resp);
            }

            ResponseStatus = RegionAgentDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += RegionAgentDA.ErrorMessage;
                return null;
            }
            
            return respList ?? Response;
        }
        /// <summary>
        /// Update RegionAgent
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public RegionAgentDTO RegionAgentUpdate(RegionAgentDTO data)
        {
            if (!(data.ID > 0))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                ErrorMessage = "Entered RegionAgent is Mistake";
                return null;
            }
            if (data?.StartDate >= data?.EndDate)
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                ErrorMessage = "RegionAgent Start and Ende Date Not Enter Correctly\n";
                return null;
            }
            var Response = RegionAgentDA.RegionAgentUpdate(data);

            var resp = RegionAgentGet(new RegionAgentDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
            Observers.ObserverStates.RegionAgentEdit state = new Observers.ObserverStates.RegionAgentEdit
            {
                RegionAgent = resp ?? Response,
                User = User,
            };
            Notify(state);

            ResponseStatus = RegionAgentDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += RegionAgentDA.ErrorMessage;
                return null;
            }

            return resp ?? Response;
        }
        /// <summary>
        /// Delete Logicly
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public RegionAgentDTO RegionAgentDelete(RegionAgentDTO data)
        {
            //Search For Use This Item Before Delete
            if (!DeletePermission(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }

            data.IsDeleted = true;
            var Response = RegionAgentDA.RegionAgentUpdate(data);

            var resp = RegionAgentGet(new RegionAgentDTO { ID = Response?.ID ?? 0, IsDeleted = true })?.FirstOrDefault();
            Observers.ObserverStates.RegionAgentDelete state = new Observers.ObserverStates.RegionAgentDelete
            {
                RegionAgent = resp ?? Response,
                User = User,
            };
            Notify(state);
            
            ResponseStatus = RegionAgentDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += RegionAgentDA.ErrorMessage;
                return null;
            }
            return resp ?? Response;
        }
        /// <summary>
        /// Delete physically
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public RegionAgentDTO RegionAgentDeleteComplete(RegionAgentDTO data)
        {
            if (!DeletePermission(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }

            var Response = RegionAgentDA.RegionAgentDelete(data);

            var resp = RegionAgentGet(new RegionAgentDTO { ID = Response?.ID ?? 0, IsDeleted = true })?.FirstOrDefault();
            Observers.ObserverStates.RegionAgentDelete state = new Observers.ObserverStates.RegionAgentDelete
            {
                RegionAgent = resp ?? Response,
                User = User,
            };
            Notify(state);

            ResponseStatus = RegionAgentDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += RegionAgentDA.ErrorMessage;
                return null;
            }
            return resp ?? Response;
        }
    }
}
