using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;
using Gostar.Setting.DA;

namespace Gostar.Setting.BL
{
    public class ExchangeRateBL:BaseBL
    {
        ExchangeRateDA ExchangeRateDL = new ExchangeRateDA();
        private bool Validate(ExchangeRateDTO data)
        {

            return Validate<Validation.ExchangeRateValidator,ExchangeRateDTO>(data ?? new ExchangeRateDTO());
            //if (!(data?.FromCurrencyID>0))
            //{
            //    ErrorMessage = "This Currency Is Mistake \n";
            //    return false;
            //}
            //if (!(data?.ToCurrencyID > 0))
            //{
            //    ErrorMessage = "This Currency Is Mistake \n";
            //    return false;
            //}
            //if(String.IsNullOrWhiteSpace(data?.Ratio.ToString()))
            //{
            //    ErrorMessage = "The Ratio Isn't Valid \n";
            //    return false;
            //}
            //if(!(data?.Year>DateTime.MinValue))
            //{
            //    ErrorMessage = "Entered Year Is't Valid \n";
            //    return false;
            //}
            //var Res = ExchangeRateGet(new ExchangeRateDTO { FromCurrencyID = data.FromCurrencyID ,ToCurrencyID=data.ToCurrencyID,Year=data.Year }, null).Count;
            //if(Res>0)
            //{
            //    ErrorMessage = "This ExchangeRate For This Year Is Exist \n";
            //    return false;
            //}

            //return true;
        }
        private bool DeletePermision(ExchangeRateDTO data)
        {

            return true;
        }
        public List<ExchangeRateDTO> ExchangeRateGet(ExchangeRateDTO data, ExchangeRateFilterDTO filter = null)
        {
            var Response = ExchangeRateDL.ExchangeRateGet(data, filter);

            ResponseStatus = ExchangeRateDL.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += ExchangeRateDL.ErrorMessage;
                return null;
            }
            return Response;
        }
        public ExchangeRateDTO ExchangeRateInsert(ExchangeRateDTO data)
        {
            if (!Validate(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }
            data.CreateDate = DateTime.Now;
            var Response = ExchangeRateDL.ExchangeRateInsert(data);

            if (Response?.ID > 0)
            {
                var resp = ExchangeRateGet(new ExchangeRateDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
                Observers.ObserverStates.ExchangeRateAdd state = new Observers.ObserverStates.ExchangeRateAdd
                {
                    ExchangeRate = resp ?? Response,
                    User = User,
                };
                Notify(state);
                if (resp != null)
                    Response = resp;
            }

            ResponseStatus = ExchangeRateDL.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += ExchangeRateDL.ErrorMessage;
                return null;
            }
            return Response;
        }
        public List<ExchangeRateDTO> ExchangeRateInsert(List<ExchangeRateDTO> data)
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
            var Response = ExchangeRateDL.ExchangeRateInsert(data);

            List<ExchangeRateDTO> respList = new List<ExchangeRateDTO>();
            foreach (var val in Response)
            {
                var resp = ExchangeRateGet(new ExchangeRateDTO { ID = val?.ID ?? 0 })?.FirstOrDefault();
                Observers.ObserverStates.ExchangeRateAdd state = new Observers.ObserverStates.ExchangeRateAdd
                {
                    ExchangeRate = resp ?? val,
                    User = User,
                };
                Notify(state);
                respList.Add(resp);
            }

            ResponseStatus = ExchangeRateDL.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += ExchangeRateDL.ErrorMessage;
                return null;
            }
            return respList ?? Response;

        }
        public ExchangeRateDTO ExchangeRateUpdate(ExchangeRateDTO data)
        {
            if (!(data.ID > 0))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                ErrorMessage = "Entered ExchangeRate is Mistake";
                return null;
            }
            var Response = ExchangeRateDL.ExchangeRateUpdate(data);

            var resp = ExchangeRateGet(new ExchangeRateDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
            Observers.ObserverStates.ExchangeRateEdit state = new Observers.ObserverStates.ExchangeRateEdit
            {
                ExchangeRate = resp ?? Response,
                User = User,
            };
            Notify(state);

            ResponseStatus = ExchangeRateDL.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += ExchangeRateDL.ErrorMessage;
                return null;
            }
            return resp ?? Response;
        }
        public ExchangeRateDTO ExchangeRateDelete(ExchangeRateDTO data)
        {
            //Search For Use This Item Before Delete
            if (!DeletePermision(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }
            data.IsDeleted = true;
            var Response = ExchangeRateDL.ExchangeRateUpdate(data);

            var resp = ExchangeRateGet(new ExchangeRateDTO { ID = Response?.ID ?? 0, IsDeleted = true })?.FirstOrDefault();
            Observers.ObserverStates.ExchangeRateDelete state = new Observers.ObserverStates.ExchangeRateDelete
            {
                ExchangeRate = resp ?? Response,
                User = User,
            };
            Notify(state);

            ResponseStatus = ExchangeRateDL.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += ExchangeRateDL.ErrorMessage;
                return null;
            }
            return resp ?? Response;
        }
    }
}
