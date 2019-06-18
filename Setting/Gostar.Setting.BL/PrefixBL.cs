using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;
using Gostar.Setting.DA;

namespace Gostar.Setting.BL
{
    public class PrefixBL : BaseBL
    {
        PrefixDA PrefixDA = new PrefixDA();
        private bool Validate(PrefixDTO data)
        {

            return Validate<Validation.PrefixValidator,PrefixDTO>(data?? new PrefixDTO());
          //  var validator = new Validation.PrefixValidator();
            //return  Validate(new Validation.PrefixValidator(), data ?? new PrefixDTO());


            //ValidationResult result = validator.Validate(data ?? new PrefixDTO());
            //ValidationErrors = result.Errors;
            //return result.IsValid;

            //if (String.IsNullOrWhiteSpace(data?.Title))
            //{
            //    ErrorMessage += "Prefix Title Is Empty !";
            //    return false;
            //}
            //if (String.IsNullOrWhiteSpace(data?.IsDefault?.ToString()))
            //{
            //    ErrorMessage += "Prefix Is Default Is Empty !";
            //    return false;
            //}
            //if (data?.IsDefault == true)
            //{
            //    var Count = PrefixGet(new PrefixDTO { IsDefault = true })?.Count();
            //    if(Count>0)
            //    {
            //        ErrorMessage += "Default Prefix Is Exist !";
            //        return false;
            //    }
            //}
            //if (!(data?.ID > 0))
            //{
            //    var Count1 = PrefixGet(new PrefixDTO { Title = data?.Title })?.Count();
            //    if (Count1 > 0)
            //    {
            //        ErrorMessage += "This Prefix Is Exist !";
            //        return false;
            //    }
            //}

            //return true;
        }
        private bool DeletePermision(PrefixDTO data)
        {
        
            return true;
        }
        public List<PrefixDTO> PrefixGet(PrefixDTO data, PrefixFilterDTO filter = null)
        {
            var Response = PrefixDA.GetPrefixs(data, filter,PagingInfo);
            ResultCount = PrefixDA.ResultCount;
            ResponseStatus = PrefixDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += PrefixDA.ErrorMessage;
                return null;
            }
            return Response;
        }
        public PrefixDTO PrefixInsert(PrefixDTO data)
        {
            if (!Validate(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }
            data.CreateDate = DateTime.Now;
            var Response = PrefixDA.PrefixInsert(data);

            if (Response?.ID > 0)
            {
                var resp = PrefixGet(new PrefixDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
                Observers.ObserverStates.PrefixAdd state = new Observers.ObserverStates.PrefixAdd
                {
                    Prefix = resp ?? Response,
                    User = User,
                };
                Notify(state);
                if (resp != null)
                    Response = resp;
            }

            ResponseStatus = PrefixDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += PrefixDA.ErrorMessage;
                return null;
            }
            return Response;
        }
        public List<PrefixDTO> PrefixInsert(List<PrefixDTO> data)
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
            var Response = PrefixDA.PrefixInsert(data);

            List<PrefixDTO> respList = new List<PrefixDTO>();
            foreach (var val in Response)
            {
                var resp = PrefixGet(new PrefixDTO { ID = val?.ID ?? 0 })?.FirstOrDefault();
                Observers.ObserverStates.PrefixAdd state = new Observers.ObserverStates.PrefixAdd
                {
                    Prefix = resp ?? val,
                    User = User,
                };
                Notify(state);
                respList.Add(resp);
            }
            
            ResponseStatus = PrefixDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += PrefixDA.ErrorMessage;
                return null;
            }
            return respList ?? Response;

        }
        public PrefixDTO PrefixUpdate(PrefixDTO data)
        {
            if (!(data.ID > 0))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                ErrorMessage = "Entered Prefix is Mistake";
                return null;
            }
            if (!Validate(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }
            var Response = PrefixDA.PrefixUpdate(data);

            var resp = PrefixGet(new PrefixDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
            Observers.ObserverStates.PrefixEdit state = new Observers.ObserverStates.PrefixEdit
            {
                Prefix = resp ?? Response,
                User = User,
            };
            Notify(state);
            
            ResponseStatus = PrefixDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += PrefixDA.ErrorMessage;
                return null;
            }
            return resp ?? Response;
        }
        public PrefixDTO PrefixDelete(PrefixDTO data)
        {
            //Search For Use This Ithem Before Delete
            if (!DeletePermision(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }
            data.IsDeleted = true;
            var Response = PrefixDA.PrefixUpdate(data);

            var resp = PrefixGet(new PrefixDTO { ID = Response?.ID ?? 0, IsDeleted = true })?.FirstOrDefault();
            Observers.ObserverStates.PrefixDelete state = new Observers.ObserverStates.PrefixDelete
            {
                Prefix = resp ?? Response,
                User = User,
            };
            Notify(state);

            ResponseStatus = PrefixDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += PrefixDA.ErrorMessage;
                return null;
            }
            return resp ?? Response;
        }
    }
}
