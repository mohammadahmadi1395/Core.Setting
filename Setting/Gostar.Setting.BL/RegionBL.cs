using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DA;
using Gostar.Setting.DTO;

namespace Gostar.Setting.BL
{
    public class RegionBL : BaseBusiness
    {
        RegionDA RegionDA = new RegionDA();
        /// <summary>
        /// Check Data For Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool Validate(RegionDTO data)
        {

            return Validate<Validation.RegionValidator,RegionDTO>(data ?? new RegionDTO());
            //if (string.IsNullOrWhiteSpace(data.Name))
            //{
            //    ErrorMessage = "Region Name Not Entered\n";
            //    return false;
            //}
            //if (!(data?.Code > 0))
            //{
            //    ErrorMessage = "Region Code Not Entered\n";
            //    return false;
            //}
            //if (data.IsDeleted == true)
            //{
            //    ErrorMessage = "Region Not yet Save in Database\n";
            //    return false;
            //}
            //if (!(data.AreaID > 0))
            //{
            //    ErrorMessage = "Area is Not Defined\n";
            //    return false;
            //}
            //else
            //{
            //    AreaDA AreaDA = new AreaDA();
            //    var AreaExist = AreaDA.AreaGet(new AreaDTO { ID = data.AreaID ?? 0 }, null)?.Count();
            //    if (!(AreaExist > 0))
            //    {
            //        ErrorMessage = "This Area Not Exist\n";
            //        return false;
            //    }
            //}
            //var RegionList = RegionGet(new RegionDTO { AreaID = data.AreaID }, null);
            //var CheckRegion = RegionList.Where(s => s.Name == data?.Name);
            //if (CheckRegion?.Count() > 0)
            //{
            //    ErrorMessage = "This Region Is Exist\n";
            //    return false;
            //}
            //else
            //{
            //    var checkCode = RegionList.Where(s => s.AreaID == data?.AreaID && s.Code == data?.Code);
            //    if (checkCode?.Count() > 0)
            //    {
            //        ErrorMessage = "This Code Is Exist And Region Name is" + checkCode.FirstOrDefault().Name + "\n";
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
        private bool DeletePermission(RegionDTO data)
        {
            if (!(data.ID > 0))
            {
                ErrorMessage = "Entered Region is Mistake";
                return false;
            }

            SectorBL SectorBl = new SectorBL();
            var Count = SectorBl.SectorGet(new SectorDTO { RegionID = data?.ID })?.Count();
            if(Count>0)
            {
                ErrorMessage = "This Region Is Used In Another Table , Please First Delete them";
                return false;
            }
            //RegionAddressDA RegionAddressDA = new RegionAddressDA();
            //var RegionAddressRegionIDCheck = RegionAddressDA.RegionAddressGet(new RegionAddressDTO { RegionID = data.ID },null).Count();
            //if ((RegionAddressRegionIDCheck > 0))
            //{
            //    ErrorMessage = "This Region use in another Tables,Please Delete them First";
            //    return false;
            //}
            //RegionAgentDA RegionAgentDA = new RegionAgentDA();
            //var RegionAgentRegionIDCheck = RegionAgentDA.RegionAgentGet(new RegionAgentDTO { RegionID = data.ID },null).Count();
            //if ((RegionAgentRegionIDCheck > 0))
            //{
            //    ErrorMessage = "This Region use in another Tables,Please Delete them First";
            //    return false;
            //}
            return true;
        }
        /// <summary>
        /// Get List of Region 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<RegionDTO> RegionGet(RegionDTO data, RegionFilterDTO filter = null)
        {
            var Response = RegionDA.RegionGet(data, filter);

            ResponseStatus = RegionDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += RegionDA.ErrorMessage;
                return null;
            }
            return Response;
        }
        /// <summary>
        /// Insert Region in Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public RegionDTO RegionInsert(RegionDTO data)
        {
            if (!Validate(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }

            data.CreateDate = DateTime.Now;
            var Response = RegionDA.RegionInsert(data);

            if (Response?.ID > 0)
            {
                var resp = RegionGet(new RegionDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
                Observers.ObserverStates.RegionAdd state = new Observers.ObserverStates.RegionAdd
                {
                    Region = resp ?? Response,
                    User = User,
                };
                Notify(state);
                if (resp != null)
                    Response = resp;
            }

            ResponseStatus = RegionDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += RegionDA.ErrorMessage;
                return null;
            }

            return Response;
        }
        /// <summary>
        /// Insert List of Region In Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<RegionDTO> RegionInsert(List<RegionDTO> data)
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
            var Response = RegionDA.RegionInsert(data);

            List<RegionDTO> respList = new List<RegionDTO>();
            foreach (var val in Response)
            {
                var resp = RegionGet(new RegionDTO { ID = val?.ID ?? 0 })?.FirstOrDefault();
                Observers.ObserverStates.RegionAdd state = new Observers.ObserverStates.RegionAdd
                {
                    Region = resp ?? val,
                    User = User,
                };
                Notify(state);
                respList.Add(resp);
            }

            ResponseStatus = RegionDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += RegionDA.ErrorMessage;
                return null;
            }

            return respList ?? Response;
        }
        /// <summary>
        /// Update Region
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public RegionDTO RegionUpdate(RegionDTO data)
        {
            if (!(data.ID > 0))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                ErrorMessage = "Entered Region is Mistake";
                return null;
            }
            var Response = RegionDA.RegionUpdate(data);

            var resp = RegionGet(new RegionDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
            Observers.ObserverStates.RegionEdit state = new Observers.ObserverStates.RegionEdit
            {
                Region = resp ?? Response,
                User = User,
            };
            Notify(state);

            ResponseStatus = RegionDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += RegionDA.ErrorMessage;
                return null;
            }
            return resp ?? Response;
        }
        /// <summary>
        /// Delete Logicly
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public RegionDTO RegionDelete(RegionDTO data)
        {
            //Search For Use This Item Before Delete
            if (!DeletePermission(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }
            data.IsDeleted = true;
            var Response = RegionDA.RegionUpdate(data);

            var resp = RegionGet(new RegionDTO { ID = Response?.ID ?? 0, IsDeleted = true })?.FirstOrDefault();
            Observers.ObserverStates.RegionDelete state = new Observers.ObserverStates.RegionDelete
            {
                Region = resp ?? Response,
                User = User,
            };
            Notify(state);

            ResponseStatus = RegionDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += RegionDA.ErrorMessage;
                return null;
            }

            return resp ?? Response;
        }
        /// <summary>
        /// Delete physically
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public RegionDTO RegionDeleteComplete(RegionDTO data)
        {
            if (!DeletePermission(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }

            var Response = RegionDA.RegionDelete(data);

            var resp = RegionGet(new RegionDTO { ID = Response?.ID ?? 0, IsDeleted = true })?.FirstOrDefault();
            Observers.ObserverStates.RegionDelete state = new Observers.ObserverStates.RegionDelete
            {
                Region = resp ?? Response,
                User = User,
            };
            Notify(state);

            ResponseStatus = RegionDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += RegionDA.ErrorMessage;
                return null;
            }
            return resp ?? Response;
        }
    }
}
