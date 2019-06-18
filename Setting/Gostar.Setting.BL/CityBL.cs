using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;
using Gostar.Setting.DA;

namespace Gostar.Setting.BL
{
    public class CityBL : BaseBL
    {
        CityDA CityDA = new CityDA();
        /// <summary>
        /// Check Data For Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool Validate(CityDTO data)
        {


            return Validate<Validation.CityValidator,CityDTO>(data ?? new CityDTO());
            //if (string.IsNullOrWhiteSpace(data.Name))
            //{
            //    ErrorMessage = "City Name Not Entered\n";
            //    return false;
            //}
            //if (!(data?.Code > 0))
            //{
            //    ErrorMessage = "City Code Not Entered\n";
            //    return false;
            //}

            //if (data.IsDeleted == true)
            //{
            //    ErrorMessage = "City Not yet Save in Database\n";
            //    return false;
            //}
            //if (!(data.CountryID > 0))
            //{
            //    ErrorMessage = "Country is Not Defined\n";
            //    return false;
            //}
            //else
            //{
            //    CountryDA CountryDA = new CountryDA();
            //    var CountryExist = CountryDA.CountryGet(new CountryDTO { ID = data.CountryID?? 0 },null)?.Count();
            //    if (!(CountryExist > 0))
            //    {
            //        ErrorMessage = "This Country Not Exist\n";
            //        return false;
            //    }
            //}
            //var CityList = CityGet(new CityDTO { CountryID = data.CountryID}, null);
            //var CheckCity = CityList.Where(s => s.Name == data?.Name)?.Count();
            //if (CheckCity> 0)
            //{
            //    ErrorMessage = "This City Is Exist\n";
            //    return false;
            //}
            //else
            //{
            //    var checkCode = CityList.Where(s => s.CountryID == data?.CountryID && s.Code == data?.Code);
            //    if (checkCode?.Count() > 0)
            //    {
            //        ErrorMessage = "This Code Is Exist And City Name is" + checkCode.FirstOrDefault().Name + "\n";
            //        return false;
            //    }
            //}

            //return true;
        } 
        /// <summary>
        /// check Data For Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool DeletePermission(CityDTO data)
        {
            if (!(data.ID > 0))
            {
                ErrorMessage = "Entered Area is Mistake";
                return false;
            }
            AreaDA AreaDA = new AreaDA();
            var AreaCityIDCheck = AreaDA.AreaGet(new AreaDTO { CityID = data.ID },null).Count();
            if ((AreaCityIDCheck > 0))
            {
                ErrorMessage = "This City use in another Tables,Please Delete them First";
                return false;
            }
            return true;
        }
        /// <summary>
        /// Get List of City 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<CityDTO> CityGet(CityDTO data,CityFilterDTO filter = null)
        {
            var Response = CityDA.CityGet(data,filter);

            ResponseStatus = CityDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += CityDA.ErrorMessage;
                return null;
            }
            return Response;
        }
        /// <summary>
        /// Insert City in Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public CityDTO CityInsert(CityDTO data)
        {
            if (!Validate(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }

            data.CreateDate = DateTime.Now;
            var Response = CityDA.CityInsert(data);

            if (Response?.ID > 0)
            {
                var resp = CityGet(new CityDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
                Observers.ObserverStates.CityAdd state = new Observers.ObserverStates.CityAdd
                {
                    City = resp ?? Response,
                    User = User,
                };
                Notify(state);
                if (resp != null)
                    Response = resp;
            }

            ResponseStatus = CityDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += CityDA.ErrorMessage;
                return null;
            }

            return Response;
        }
        /// <summary>
        /// Insert List of City In Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<CityDTO> CityInsert(List<CityDTO> data)
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
            var Response = CityDA.CityInsert(data);

            List<CityDTO> respList = new List<CityDTO>();
            foreach (var val in Response)
            {
                var resp = CityGet(new CityDTO { ID = val?.ID ?? 0 })?.FirstOrDefault();
                Observers.ObserverStates.CityAdd state = new Observers.ObserverStates.CityAdd
                {
                    City = resp ?? val,
                    User = User,
                };
                Notify(state);
                respList.Add(resp);
            }

            ResponseStatus = CityDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += CityDA.ErrorMessage;
                return null;
            }

            return respList ?? Response;
        }
        /// <summary>
        /// Update City
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public CityDTO CityUpdate(CityDTO data)
        {

            if (!(data.ID > 0))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                ErrorMessage = "Entered City is Mistake";
                return null;
            }
            var Response = CityDA.CityUpdate(data);

            var resp = CityGet(new CityDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
            Observers.ObserverStates.CityEdit state = new Observers.ObserverStates.CityEdit
            {
                City = resp ?? Response,
                User = User,
            };
            Notify(state);

            ResponseStatus = CityDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += CityDA.ErrorMessage;
                return null;
            }
            return resp ?? Response;
        }
        /// <summary>
        /// Delete Logicly
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public CityDTO CityDelete(CityDTO data)
        {
            //Search For Use This Item Before Delete
            if (!DeletePermission(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }

            data.IsDeleted = true;
            var Response = CityDA.CityUpdate(data);

            var resp = CityGet(new CityDTO { ID = Response?.ID ?? 0, IsDeleted = true })?.FirstOrDefault();
            Observers.ObserverStates.CityDelete state = new Observers.ObserverStates.CityDelete
            {
                City = resp ?? Response,
                User = User,
            };
            Notify(state);
            
            ResponseStatus = CityDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += CityDA.ErrorMessage;
                return null;
            }

            return resp ?? Response;
        }
        /// <summary>
        /// Delete physically
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public CityDTO CityDeleteComplete(CityDTO data)
        {
            if (!DeletePermission(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }
            var Response = CityDA.CityDelete(data);

            var resp = CityGet(new CityDTO { ID = Response?.ID ?? 0, IsDeleted = true })?.FirstOrDefault();
            Observers.ObserverStates.CityDelete state = new Observers.ObserverStates.CityDelete
            {
                City = resp ?? Response,
                User = User,
            };
            Notify(state);

            ResponseStatus = CityDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += CityDA.ErrorMessage;
                return null;
            }
            return resp ?? Response;
        }
    }
}
