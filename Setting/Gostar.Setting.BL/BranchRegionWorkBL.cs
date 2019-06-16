using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DA;
using Gostar.Setting.DTO;

namespace Gostar.Setting.BL
{
    public class BranchRegionWorkBL : BaseBusiness
    {
        BranchRegionWorkDA BranchRegionWorkDA = new BranchRegionWorkDA();
        private bool Validate(BranchRegionWorkDTO data)
        {

            return Validate<Validation.BranchRegionWorkValidator,BranchRegionWorkDTO>(data ?? new BranchRegionWorkDTO());
            ////if(!(data.ZoneID>0))
            ////{
            ////    return false;
            ////}
            ////if (!(data.BranchID > 0))
            ////{
            ////    return false;
            ////}
            ////var Regions = BranchRegionWorkDA.BranchRegionWorkGet(new BranchRegionWorkDTO { BranchID = data.BranchID, ZoneID = data.ZoneID })?.Count;
            ////if (Regions > 0)
            ////{
            ////    //  ErrorMessage = "This Region(s) Is Exists ! \n"
            ////    return false;
            ////}
            //var Branch = new BranchBL().BranchGet(new BranchDTO { IsCentral = true })?.FirstOrDefault();
            //if (!(Branch.ID == data.BranchID))
            //{
            //    var AllRegions = BranchRegionWorkGet(new BranchRegionWorkDTO());
            //    foreach (var Reg in AllRegions)
            //    {
            //        if (Reg.BranchID == Branch.ID)
            //            continue;
            //        if (Reg.BranchID != data.BranchID && Reg.ZoneAndParents.Contains(data.ZoneID ?? 0))
            //            return false;
            //        if (Reg.BranchID != data.BranchID && Reg.ZoneAndChilds.Contains(data.ZoneID ?? 0))
            //            return false;
            //    }
            //}
            //return true;
        }
        public List<BranchRegionWorkDTO> BranchRegionWorkGet(BranchRegionWorkDTO data)
        {
            var Response = BranchRegionWorkDA.BranchRegionWorkGet(data);
            var ResponseZone = new ZoneBL().ZoneGet(new ZoneDTO());

            var result = (from Rw in Response
                          join Zone in ResponseZone on Rw.ZoneID equals Zone.ID
                          select new BranchRegionWorkDTO
                          {
                              ID = Rw.ID,
                              BranchID = Rw.BranchID,
                              ZoneID = Rw.ZoneID,
                              CreateDate = Rw.CreateDate,
                              IsDeleted = Rw.IsDeleted,
                              ZoneAndChilds = Zone.ZoneAndChilds,
                              ZoneAndParents = Zone.ZoneAndParents
                          })?.ToList();

            ResponseStatus = BranchRegionWorkDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += BranchRegionWorkDA.ErrorMessage;
                return null;
            }
            return result;
        }
        public BranchRegionWorkDTO BranchRegionWorkInsert(BranchRegionWorkDTO data)
        {
            if (!Validate(data))
                return null;
            data.CreateDate = DateTime.Now;
            var Response = BranchRegionWorkDA.BranchRegionWorkInsert(data);

            if (Response?.ID > 0)
            {
                var resp = BranchRegionWorkGet(new BranchRegionWorkDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
                Observers.ObserverStates.BranchRegionWorkAdd state = new Observers.ObserverStates.BranchRegionWorkAdd
                {
                    BranchRegionWork = resp ?? Response,
                    User = User,
                };
                Notify(state);
                if (resp != null)
                    Response = resp;
            }

            ResponseStatus = BranchRegionWorkDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += BranchRegionWorkDA.ErrorMessage;
                return null;
            }

            return Response;
        }
        public List<BranchRegionWorkDTO> BranchRegionWorkInsert(List<BranchRegionWorkDTO> data)
        {
            List<BranchRegionWorkDTO> Temp = new List<BranchRegionWorkDTO>();
            if (data.Count == 1 && data.FirstOrDefault().ZoneID == null)
            {
                var rd = BranchRegionWorkDA.BranchRegionWorkDeleteByBranchID(data?.FirstOrDefault().BranchID);
                if (rd != Common.ResponseStatus.Successful)
                {
                    ErrorMessage += "Cant Update !";
                    return null;
                }
                else
                {
                    ResponseStatus = Common.ResponseStatus.Successful;
                    return data;
                }
            }

            foreach (var val in data)
            {
                val.CreateDate = DateTime.Now;
                if (Validate(val))
                    Temp.Add(val);
            }
            data = Temp;
            if (!(data.Count > 0))
            {
                ErrorMessage += "This Region(s) Is Exists ! \n";
                return null;
            }
            var ResponseDelete = BranchRegionWorkDA.BranchRegionWorkDeleteByBranchID(data?.FirstOrDefault().BranchID);
            if (ResponseDelete != Common.ResponseStatus.Successful)
            {
                ErrorMessage += "Cant Update !";
                return null;
            }
            var Response = BranchRegionWorkDA.BranchRegionWorkInsert(data);

            List<BranchRegionWorkDTO> respList = new List<BranchRegionWorkDTO>();
            foreach (var val in Response)
            {
                var resp = BranchRegionWorkGet(new BranchRegionWorkDTO { ID = val?.ID ?? 0 })?.FirstOrDefault();
                Observers.ObserverStates.BranchRegionWorkAdd state = new Observers.ObserverStates.BranchRegionWorkAdd
                {
                    BranchRegionWork = resp ?? val,
                    User = User,
                };
                Notify(state);
                respList.Add(resp);
            }

            ResponseStatus = BranchRegionWorkDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += BranchRegionWorkDA.ErrorMessage;
                return null;
            }

            return respList ?? Response;
        }
        public BranchRegionWorkDTO BranchRegionWorkUpdate(BranchRegionWorkDTO data)
        {
            if (!(data.BranchID > 0))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                ErrorMessage = "Entered BranchRegionWork is Mistake";
                return null;
            }
            var Response = BranchRegionWorkDA.BranchRegionWorkUpdate(data);

            var resp = BranchRegionWorkGet(new BranchRegionWorkDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
            Observers.ObserverStates.BranchRegionWorkEdit state = new Observers.ObserverStates.BranchRegionWorkEdit
            {
                BranchRegionWork = resp ?? Response,
                User = User,
            };
            Notify(state);
            
            ResponseStatus = BranchRegionWorkDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += BranchRegionWorkDA.ErrorMessage;
                return null;
            }

            return resp ?? Response;
        }
        public BranchRegionWorkDTO BranchRegionWorkDelete(BranchRegionWorkDTO data)
        {

            data.IsDeleted = true;
            var Response = BranchRegionWorkDA.BranchRegionWorkUpdate(data);

            var resp = BranchRegionWorkGet(new BranchRegionWorkDTO { ID = Response?.ID ?? 0, IsDeleted = true })?.FirstOrDefault();
            Observers.ObserverStates.BranchRegionWorkDelete state = new Observers.ObserverStates.BranchRegionWorkDelete
            {
                BranchRegionWork = resp ?? Response,
                User = User,
            };
            Notify(state);

            ResponseStatus = BranchRegionWorkDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += BranchRegionWorkDA.ErrorMessage;
                return null;
            }

            return resp ?? Response;
        }
    }
}
