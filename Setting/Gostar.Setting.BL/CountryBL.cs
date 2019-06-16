using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;
using Gostar.Setting.DA;

namespace Gostar.Setting.BL
{
    public class CountryBL : BaseBusiness
    {
        CountryDA CountryDA = new CountryDA();
        /// <summary>
        /// Check Data For Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool Validate(CountryDTO data)
        {

            return Validate<Validation.CountryValidator,CountryDTO>(data ?? new CountryDTO());
            //if (string.IsNullOrWhiteSpace(data.Name))
            //{
            //    ErrorMessage = "Country Name Not Entered\n";
            //    return false;
            //}
           
            //if (data.IsDeleted == true)
            //{
            //    ErrorMessage = "Country Not yet Save in Database\n";
            //    return false;
            //}
            //var CountryList = CountryGet(new CountryDTO { Name = data?.Name }, null);
            //var CheckCountry = CountryList.Where(s=>s.Name==data?.Name)?.Count();
            //if (CheckCountry > 0)
            //{
            //    ErrorMessage = "This Country Is Exist\n";
            //    return false;
            //}

            //return true;
        }
        /// <summary>
        /// Check Data For Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool DeletePermission(CountryDTO data)
        {
            if (!(data.ID > 0))
            {
                ErrorMessage = "Entered Country is Mistake";
                return false;
            }
            CityDA CityDA= new CityDA();
            var CityCountryIDCheck = CityDA.CityGet(new CityDTO{ CountryID= data.ID },null).Count();
            if ((CityCountryIDCheck> 0))
            {
                ErrorMessage = "This Country use in another Tables,Please Delete  them First";
                return false;
            }
            return true;
        }
        /// <summary>
        /// Get List of Country 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<CountryDTO> CountryGet(CountryDTO data,CountryFilterDTO filter =null)
        {

            var Response = CountryDA.CountryGet(data,filter,PagingInfo);
            ResultCount = CountryDA.ResultCount;
            ResponseStatus = CountryDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += CountryDA.ErrorMessage;
                return null;
            }
            return Response;

        }
        /// <summary>
        /// Insert Country in Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public CountryDTO CountryInsert(CountryDTO data)
        {
            if (!Validate(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }

            data.CreateDate = DateTime.Now;
            var Response = CountryDA.CountryInsert(data);

            if (Response?.ID > 0)
            {
                var resp = CountryGet(new CountryDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
                Observers.ObserverStates.CountryAdd state = new Observers.ObserverStates.CountryAdd
                {
                    Country = resp ?? Response,
                    User = User,
                };
                Notify(state);
                if (resp != null)
                    Response = resp;
            }

            ResponseStatus = CountryDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += CountryDA.ErrorMessage;
                return null;
            }
            return Response;
        }
        /// <summary>
        /// Insert List of Country In Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<CountryDTO> CountryInsert(List<CountryDTO> data)
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
            var Response = CountryDA.CountryInsert(data);

            List<CountryDTO> respList = new List<CountryDTO>();
            foreach (var val in Response)
            {
                var resp = CountryGet(new CountryDTO { ID = val?.ID ?? 0 })?.FirstOrDefault();
                Observers.ObserverStates.CountryAdd state = new Observers.ObserverStates.CountryAdd
                {
                    Country = resp ?? val,
                    User = User,
                };
                Notify(state);
                respList.Add(resp);
            }

            ResponseStatus = CountryDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += CountryDA.ErrorMessage;
                return null;
            }

            return respList ?? Response;

        }
        /// <summary>
        /// Update Country
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public CountryDTO CountryUpdate(CountryDTO data)
        {
            if (!(data.ID > 0))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                ErrorMessage = "Entered Country is Mistake";
                return null;
            }
            var Response = CountryDA.CountryUpdate(data);

            var resp = CountryGet(new CountryDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
            Observers.ObserverStates.CountryEdit state = new Observers.ObserverStates.CountryEdit
            {
                Country = resp ?? Response,
                User = User,
            };
            Notify(state);

            ResponseStatus = CountryDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += CountryDA.ErrorMessage;
                return null;
            }
            return resp ?? Response;
        }
        /// <summary>
        /// Delete Logicly
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public CountryDTO CountryDelete(CountryDTO data)
        {
            //Search For Use This Item Before Delete
            if (!DeletePermission(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }

            data.IsDeleted = true;
            var Response = CountryDA.CountryUpdate(data);

            var resp = CountryGet(new CountryDTO { ID = Response?.ID ?? 0, IsDeleted = true })?.FirstOrDefault();
            Observers.ObserverStates.CountryDelete state = new Observers.ObserverStates.CountryDelete
            {
                Country = resp ?? Response,
                User = User,
            };
            Notify(state);
            
            ResponseStatus = CountryDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += CountryDA.ErrorMessage;
                return null;
            }
            return resp ?? Response;

        }
        /// <summary>
        /// Delete physically
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public CountryDTO CountryDeleteComplete(CountryDTO data)
        {            
            if (!DeletePermission(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }
            var Response = CountryDA.CountryDelete(data);

            var resp = CountryGet(new CountryDTO { ID = Response?.ID ?? 0, IsDeleted = true })?.FirstOrDefault();
            Observers.ObserverStates.CountryDelete state = new Observers.ObserverStates.CountryDelete
            {
                Country = resp ?? Response,
                User = User,
            };
            Notify(state);

            ResponseStatus = CountryDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += CountryDA.ErrorMessage;
                return null;
            }
            return resp ?? Response;
        }
    }
}
