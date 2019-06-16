using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;
using Gostar.Setting.DA;

namespace Gostar.Setting.BL
{
    public class CurrencyBL: BaseBusiness
    {
        CurrencyDA CurrencyDL = new CurrencyDA();
        private bool Validate(CurrencyDTO data)
        {

            return Validate<Validation.CurrencyValidator,CurrencyDTO>( data ?? new CurrencyDTO());
            //if(String.IsNullOrWhiteSpace(data?.Title))
            //{
            //    ErrorMessage = "Currency Title is Empty \n";
            //    return false;
            //}
            //if(String.IsNullOrWhiteSpace(data?.Symbol))
            //{
            //    ErrorMessage = "Currency Symbol is Empty \n";
            //    return false;
            //}
         

            //var res = CurrencyGet(new CurrencyDTO { Title = data.Title }, null).Count;
            //if(res>0)
            //{
            //    ErrorMessage = "This Currency Is Exist \n";
            //    return false;
            //}

            //return true;

        }
        private bool DeletePermision(CurrencyDTO data)
        {
            ExchangeRateBL ExchangeRateBL = new ExchangeRateBL();
            if (data?.ID ==(long)210) // Dinar , In Use in Code(For ExchangeRate From x To Dinar)
            {
                ErrorMessage = "This Currency Type is Non Deleteable.";
                return false;
            }
            if (ExchangeRateBL.ExchangeRateGet(new ExchangeRateDTO { FromCurrencyID =data?.ID},null).Count>0)
            {
                ErrorMessage = "This data is in use in other tables, first delete them.";
                return false;
            }
            if(ExchangeRateBL.ExchangeRateGet(new ExchangeRateDTO { ToCurrencyID = data?.ID},null).Count>0)
            {
                ErrorMessage = "This data is in use in other tables, first delete them.";
                return false;
            }
          
            return true;
        }
        public List<CurrencyDTO> CurrencyGet(CurrencyDTO data, CurrencyFilterDTO filter = null)
        {
            var Response = CurrencyDL.CurrencyGet(data, filter);

            ResponseStatus = CurrencyDL.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += CurrencyDL.ErrorMessage;
                return null;
            }
            return Response;
        }
        public CurrencyDTO CurrencyInsert(CurrencyDTO data)
        {
            if (!Validate(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }
            data.CreateDate = DateTime.Now;
            var Response = CurrencyDL.CurrencyInsert(data);

            if (Response?.ID > 0)
            {
                var resp = CurrencyGet(new CurrencyDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
                Observers.ObserverStates.CurrencyAdd state = new Observers.ObserverStates.CurrencyAdd
                {
                    Currency = resp ?? Response,
                    User = User,
                };
                Notify(state);
                if (resp != null)
                    Response = resp;
            }

            ResponseStatus = CurrencyDL.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += CurrencyDL.ErrorMessage;
                return null;
            }
            return Response;
        }
        public List<CurrencyDTO> CurrencyInsert(List<CurrencyDTO> data)
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
            var Response = CurrencyDL.CurrencyInsert(data);

            List<CurrencyDTO> respList = new List<CurrencyDTO>();
            foreach (var val in Response)
            {
                var resp = CurrencyGet(new CurrencyDTO { ID = val?.ID ?? 0 })?.FirstOrDefault();
                Observers.ObserverStates.CurrencyAdd state = new Observers.ObserverStates.CurrencyAdd
                {
                    Currency = resp ?? val,
                    User = User,
                };
                Notify(state);
                respList.Add(resp);
            }

            ResponseStatus = CurrencyDL.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += CurrencyDL.ErrorMessage;
                return null;
            }
            return respList ?? Response;

        }
        public CurrencyDTO CurrencyUpdate(CurrencyDTO data)
        {
            if (!(data.ID > 0))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                ErrorMessage = "Entered Currency is Mistake";
                return null;
            }
            var Response = CurrencyDL.CurrencyUpdate(data);

            var resp = CurrencyGet(new CurrencyDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
            Observers.ObserverStates.CurrencyEdit state = new Observers.ObserverStates.CurrencyEdit
            {
                Currency = resp ?? Response,
                User = User,
            };
            Notify(state);
            
            ResponseStatus = CurrencyDL.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += CurrencyDL.ErrorMessage;
                return null;
            }
            return resp ?? Response;
        }
        public CurrencyDTO CurrencyDelete(CurrencyDTO data)
        {
            //Search For Use This Item Before Delete
            if (!DeletePermision(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }
            data.IsDeleted = true;
            var Response = CurrencyDL.CurrencyUpdate(data);

            var resp = CurrencyGet(new CurrencyDTO { ID = Response?.ID ?? 0, IsDeleted = true })?.FirstOrDefault();
            Observers.ObserverStates.CurrencyDelete state = new Observers.ObserverStates.CurrencyDelete
            {
                Currency = resp ?? Response,
                User = User,
            };
            Notify(state);

            ResponseStatus = CurrencyDL.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += CurrencyDL.ErrorMessage;
                return null;
            }
            return resp ?? Response;
        }
    }
}
