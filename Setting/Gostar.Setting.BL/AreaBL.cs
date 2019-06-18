using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;
using Gostar.Setting.DA;

namespace Gostar.Setting.BL
{
    public class AreaBL : BaseBL
    {
        AreaDA AreaDA = new AreaDA();
        /// <summary>
        /// Check Data For Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool Validate(AreaDTO data)
        {

            return Validate<Validation.AreaValidator, AreaDTO>(data ?? new AreaDTO());
            //ValidatorOptions.LanguageManager = new Gostar.Common.Validation.ErrorLanguageManager();
            //ValidatorOptions.LanguageManager.Culture = Culture;

            //var validator = new Validation.AreaValidator();
            //ValidationResult result = validator.Validate(data ?? new AreaDTO());
            //ValidationErrors = result.Errors;
            //return result.IsValid;
            //if (string.IsNullOrWhiteSpace(data.Name))
            //{
            //    ErrorMessage = "Area Name Not Entered\n";
            //    return false;
            //}

            //if (!(data.Code > 0))
            //{
            //    ErrorMessage = "Area Code Not Entered\n";
            //    return false;
            //}
            //if (data.IsDeleted == true)
            //{
            //    ErrorMessage = "Area Not yet Save in Database\n";
            //    return false;
            //}
            //if (!(data.CityID > 0))
            //{
            //    ErrorMessage = "City is Not Defined\n";
            //    return false;
            //}
            //else
            //{
            //    CityDA CityDA = new CityDA();
            //    var CityExist = CityDA.CityGet(new CityDTO { ID = data.CityID ?? 0 }, null)?.Count();
            //    if (!(CityExist > 0))
            //    {
            //        ErrorMessage = "This City Not Exist\n";
            //        return false;
            //    }
            //}
            //var AreaList = AreaGet(new AreaDTO { },null)?.ToList();
            //var CheckArea = AreaList.Where(s=>s.Name==data?.Name )?.Count();
            //if (CheckArea > 0)
            //{
            //    ErrorMessage = "This Area Is Exist\n";
            //    return false;
            //}
            //else
            //{
            //    var checkCode = AreaList.Where(s => s.CityID == data?.CityID && s.Code == data?.Code);
            //    if (checkCode.Count() > 0)
            //    {
            //        ErrorMessage = "This Code Is Exist And Area Name is"+checkCode.FirstOrDefault().Name+"\n";
            //        return false;
            //    }
            //}

            return true;
        }
        /// <summary>
        /// Check Data For Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool DeletePermission(AreaDTO data)
        {
            if (!(data.ID > 0))
            {
                ErrorMessage = "Entered Area is Mistake";
                return false;
            }
            RegionDA RegionDA = new RegionDA();
            var RegionAreaIDCheck = RegionDA.RegionGet(new RegionDTO { AreaID = data.ID }, null).Count();
            if ((RegionAreaIDCheck > 0))
            {
                ErrorMessage = "This Area use in another Tables,Please Delete  them First";
                return false;
            }
            return true;
        }
        /// <summary>
        /// Get List of Area From Database Whith DTO
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<AreaDTO> AreaGet(AreaDTO data, AreaFilterDTO filter = null)
        {
            var Response = AreaDA.AreaGet(data, filter);

            ResponseStatus = AreaDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += AreaDA.ErrorMessage;
                return null;
            }

            return Response;
        }
        /// <summary>
        /// Insert Area In Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public AreaDTO AreaInsert(AreaDTO data)
        {
            //validate data
            if (!Validate(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }
            data.CreateDate = DateTime.Now;
            var Response = AreaDA.AreaInsert(data);

            if (Response?.ID > 0)
            {
                var resp = AreaGet(new AreaDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
                Observers.ObserverStates.AreaAdd state = new Observers.ObserverStates.AreaAdd
                {
                    Area = resp ?? Response,
                    User = User,
                };
                Notify(state);
                if (resp != null)
                    Response = resp;
            }

            ResponseStatus = AreaDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += AreaDA.ErrorMessage;
                return null;
            }

            return Response;
        }
        /// <summary>
        /// Insert List of Area In Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<AreaDTO> AreaInsert(List<AreaDTO> data)
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
            var Response = AreaDA.AreaInsert(data);

            List<AreaDTO> respList = new List<AreaDTO>();
            foreach (var val in Response)
            {
                var resp = AreaGet(new AreaDTO { ID = val?.ID ?? 0 })?.FirstOrDefault();
                Observers.ObserverStates.AreaAdd state = new Observers.ObserverStates.AreaAdd
                {
                    Area = resp ?? val,
                    User = User,
                };
                Notify(state);
                respList.Add(resp);
            }
            
            ResponseStatus = AreaDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += AreaDA.ErrorMessage;
                return null;
            }

            return respList ?? Response;

        }
        /// <summary>
        /// AreaUpdate
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public AreaDTO AreaUpdate(AreaDTO data)
        {
            if (!(data.ID > 0))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                ErrorMessage = "Entered Area is Mistake";
                return null;
            }
            var Response = AreaDA.AreaUpdate(data);

            var resp = AreaGet(new AreaDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
            Observers.ObserverStates.AreaEdit state = new Observers.ObserverStates.AreaEdit
            {
                Area = resp ?? Response,
                User = User,
            };
            Notify(state);
            
            ResponseStatus = AreaDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += AreaDA.ErrorMessage;
                return null;
            }
            return resp ?? Response;
        }
        /// <summary>
        /// Delete Logicly
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public AreaDTO AreaDelete(AreaDTO data)
        {
            //Search For Use This Item Before Delete
            if (!DeletePermission(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }
            data.IsDeleted = true;
            var Response = AreaDA.AreaUpdate(data);

            var resp = AreaGet(new AreaDTO { ID = Response?.ID ?? 0, IsDeleted = true })?.FirstOrDefault();
            Observers.ObserverStates.AreaDelete state = new Observers.ObserverStates.AreaDelete
            {
                Area = resp ?? Response,
                User = User,
            };
            Notify(state);
            
            ResponseStatus = AreaDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += AreaDA.ErrorMessage;
                return null;
            }
            return resp ?? Response;
        }
        /// <summary>
        /// Delete physically
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public AreaDTO AreaDeleteComplete(AreaDTO data)
        {
            if (!DeletePermission(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }
            var Response = AreaDA.AreaDelete(data);

            var resp = AreaGet(new AreaDTO { ID = Response?.ID ?? 0, IsDeleted = true })?.FirstOrDefault();
            Observers.ObserverStates.AreaDelete state = new Observers.ObserverStates.AreaDelete
            {
                Area = resp ?? Response,
                User = User,
            };
            Notify(state);

            ResponseStatus = AreaDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += AreaDA.ErrorMessage;
                return null;
            }
            return resp ?? Response;
        }
    }
}
