using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DA;
using Gostar.Setting.DTO;

namespace Gostar.Setting.BL
{
    public class SectorBL : BaseBL
    {
        SectorDA SectorDA = new SectorDA();
        private bool Validate(SectorDTO data)
        {


            return Validate<Validation.SectorValidator,SectorDTO>(data ?? new SectorDTO());
            //if (string.IsNullOrWhiteSpace(data.Name))
            //{
            //    ErrorMessage = "Sector Name Not Entered\n";
            //    return false;
            //}
            //if (data.IsDeleted == true)
            //{
            //    ErrorMessage = "Sector Not yet Save in Database\n";
            //    return false;
            //}
            //if (!(data.RegionID > 0))
            //{
            //    ErrorMessage = "Region is Not Defined\n";
            //    return false;
            //}
            //else
            //{
            //    RegionDA RegionDA = new RegionDA();
            //    var AreaExist = RegionDA.RegionGet(new RegionDTO { ID = data.RegionID ?? 0 }, null)?.Count();
            //    if (!(AreaExist > 0))
            //    {
            //        ErrorMessage = "This Region Not Exist\n";
            //        return false;
            //    }
            //}
            //var SectorList = SectorGet(new SectorDTO { RegionID = data.RegionID }, null);
            //var CheckSector = SectorList.Where(s => s.Name == data?.Name);
            //if (CheckSector?.Count() > 0)
            //{
            //    ErrorMessage = "This Sector Is Exist\n";
            //    return false;
            //}
            //else
            //{
            //    var checkCode = SectorList.Where(s => s.RegionID == data?.RegionID && s.Code == data?.Code);
            //    if (checkCode?.Count() > 0)
            //    {
            //        ErrorMessage = "This Code Is Exist And Sector Name is" + checkCode.FirstOrDefault().Name + "\n";
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
        private bool DeletePermission(SectorDTO data)
        {
            if (!(data.ID > 0))
            {
                ErrorMessage = "Entered Sector is Mistake";
                return false;
            }
            //SectorAddressDA SectorAddressDA = new SectorAddressDA();
            //var SectorAddressSectorIDCheck = SectorAddressDA.SectorAddressGet(new SectorAddressDTO { SectorID = data.ID },null).Count();
            //if ((SectorAddressSectorIDCheck > 0))
            //{
            //    ErrorMessage = "This Sector use in another Tables,Please Delete them First";
            //    return false;
            //}
            //SectorAgentDA SectorAgentDA = new SectorAgentDA();
            //var SectorAgentSectorIDCheck = SectorAgentDA.SectorAgentGet(new SectorAgentDTO { SectorID = data.ID },null).Count();
            //if ((SectorAgentSectorIDCheck > 0))
            //{
            //    ErrorMessage = "This Sector use in another Tables,Please Delete them First";
            //    return false;
            //}
            return true;
        }
        /// <summary>
        /// Get List of Sector 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<SectorDTO> SectorGet(SectorDTO data, SectorFilterDTO filter = null)
        {
            var Response = SectorDA.SectorGet(data, filter);

            ResponseStatus = SectorDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += SectorDA.ErrorMessage;
                return null;
            }
            return Response;
        }
        /// <summary>
        /// Insert Sector in Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public SectorDTO SectorInsert(SectorDTO data)
        {
            if (!Validate(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }

            data.CreateDate = DateTime.Now;
            var Response = SectorDA.SectorInsert(data);

            if (Response?.ID > 0)
            {
                var resp = SectorGet(new SectorDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
                Observers.ObserverStates.SectorAdd state = new Observers.ObserverStates.SectorAdd
                {
                    Sector = resp ?? Response,
                    User = User,
                };
                Notify(state);
                if (resp != null)
                    Response = resp;
            }

            ResponseStatus = SectorDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += SectorDA.ErrorMessage;
                return null;
            }
            return Response;
        }
        /// <summary>
        /// Insert List of Sector In Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<SectorDTO> SectorInsert(List<SectorDTO> data)
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
            var Response = SectorDA.SectorInsert(data);

            List<SectorDTO> respList = new List<SectorDTO>();
            foreach (var val in Response)
            {
                var resp = SectorGet(new SectorDTO { ID = val?.ID ?? 0 })?.FirstOrDefault();
                Observers.ObserverStates.SectorAdd state = new Observers.ObserverStates.SectorAdd
                {
                    Sector = resp ?? val,
                    User = User,
                };
                Notify(state);
                respList.Add(resp);
            }

            ResponseStatus = SectorDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += SectorDA.ErrorMessage;
                return null;
            }

            return respList ?? Response;
        }
        /// <summary>
        /// Update Sector
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public SectorDTO SectorUpdate(SectorDTO data)
        {
            if (!(data.ID > 0))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                ErrorMessage = "Entered Sector is Mistake";
                return null;
            }
            var Response = SectorDA.SectorUpdate(data);

            var resp = SectorGet(new SectorDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
            Observers.ObserverStates.SectorEdit state = new Observers.ObserverStates.SectorEdit
            {
                Sector = resp ?? Response,
                User = User,
            };
            Notify(state);
            
            ResponseStatus = SectorDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += SectorDA.ErrorMessage;
                return null;
            }
            return resp ?? Response;
        }
        /// <summary>
        /// Delete Logicly
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public SectorDTO SectorDelete(SectorDTO data)
        {
            //Search For Use This Item Before Delete
            if (!DeletePermission(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }
            data.IsDeleted = true;
            var Response = SectorDA.SectorUpdate(data);

            var resp = SectorGet(new SectorDTO { ID = Response?.ID ?? 0, IsDeleted = true })?.FirstOrDefault();
            Observers.ObserverStates.SectorDelete state = new Observers.ObserverStates.SectorDelete
            {
                Sector = resp ?? Response,
                User = User,
            };
            Notify(state);

            ResponseStatus = SectorDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += SectorDA.ErrorMessage;
                return null;
            }
            return resp ?? Response;
        }
        /// <summary>
        /// Delete physically
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public SectorDTO SectorDeleteComplete(SectorDTO data)
        {
            if (!DeletePermission(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }

            var Response = SectorDA.SectorDelete(data);

            var resp = SectorGet(new SectorDTO { ID = Response?.ID ?? 0, IsDeleted = true })?.FirstOrDefault();
            Observers.ObserverStates.SectorDelete state = new Observers.ObserverStates.SectorDelete
            {
                Sector = resp ?? Response,
                User = User,
            };
            Notify(state);

            ResponseStatus = SectorDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += SectorDA.ErrorMessage;
                return null;
            }
            return resp ?? Response;
        }
    }
}
