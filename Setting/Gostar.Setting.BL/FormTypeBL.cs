using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;
using Gostar.Setting.DA;

namespace Gostar.Setting.BL
{
    public class FormTypeBL : BaseBusiness
    {
        FormTypeDA FormTypeDL = new FormTypeDA();
        private bool Validate(FormTypeDTO data)
        {


            return Validate<Validation.FormTypeValidator,FormTypeDTO>(data ?? new FormTypeDTO());
            //    if (String.IsNullOrWhiteSpace(data?.Title))
            //    {
            //        ErrorMessage = "FormType Title is Empty \n";
            //        return false;
            //    }
            //    if (!(data?.SubSystemID > 0))
            //    {
            //        ErrorMessage = "FormType Sub System is Incorrect \n";
            //        return false;
            //    }



            //    var res = FormTypeGet(new FormTypeDTO { PublicCode = data?.PublicCode, SubSystemID = data?.SubSystemID }, null).Count;
            //    if (res > 0)
            //    {
            //        ErrorMessage = "This FormType Is Exist \n";
            //        return false;
            //    }

            //    return true;

        }
        private bool DeletePermision(FormTypeDTO data)
        {
            if (!(data?.ID > 0))
            {
                ErrorMessage += "Mistake Date\n";
                return false;
            }
            var formtype = FormTypeGet(data)?.FirstOrDefault();
            if (formtype?.Enum != null)
            {
                ErrorMessage += "This Type Is Non Deleteable\n";
                return false;
            }

            return true;
        } 
        public List<FormTypeDTO> FormTypeGet(FormTypeDTO data, FormTypeFilterDTO filter = null)
        {
            var Response = FormTypeDL.FormTypeGet(data, filter);

            ResponseStatus = FormTypeDL.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += FormTypeDL.ErrorMessage;
                return null;
            }
            return Response;
        }
        public FormTypeDTO FormTypeInsert(FormTypeDTO data)
        {
            if (!Validate(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }
            data.CreateDate = DateTime.Now;
            var Response = FormTypeDL.FormTypeInsert(data);

            if (Response?.ID > 0)
            {
                var resp = FormTypeGet(new FormTypeDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
                Observers.ObserverStates.FormTypeAdd state = new Observers.ObserverStates.FormTypeAdd
                {
                    FormType = resp ?? Response,
                    User = User,
                };
                Notify(state);
                if (resp != null)
                    Response = resp;
            }

            ResponseStatus = FormTypeDL.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += FormTypeDL.ErrorMessage;
                return null;
            }
            return Response;
        }
        public List<FormTypeDTO> FormTypeInsert(List<FormTypeDTO> data)
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
            var Response = FormTypeDL.FormTypeInsert(data);

            List<FormTypeDTO> respList = new List<FormTypeDTO>();
            foreach (var val in Response)
            {
                var resp = FormTypeGet(new FormTypeDTO { ID = val?.ID ?? 0 })?.FirstOrDefault();
                Observers.ObserverStates.FormTypeAdd state = new Observers.ObserverStates.FormTypeAdd
                {
                    FormType = resp ?? val,
                    User = User,
                };
                Notify(state);
                respList.Add(resp);
            }

            ResponseStatus = FormTypeDL.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += FormTypeDL.ErrorMessage;
                return null;
            }
            return respList ?? Response;

        }
        public FormTypeDTO FormTypeUpdate(FormTypeDTO data)
        {
            if (!(data.ID > 0))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                ErrorMessage = "Entered FormType is Mistake";
                return null;
            }
            var res = FormTypeGet(new FormTypeDTO { PublicCode = data?.PublicCode, SubSystemID = data?.SubSystemID }, null).Count;
            if (res > 0)
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                ErrorMessage = "This Public Code Is Exist \n";
                return null;
            }
            var Response = FormTypeDL.FormTypeUpdate(data);

            var resp = FormTypeGet(new FormTypeDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
            Observers.ObserverStates.FormTypeEdit state = new Observers.ObserverStates.FormTypeEdit
            {
                FormType = resp ?? Response,
                User = User,
            };
            Notify(state);

            ResponseStatus = FormTypeDL.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += FormTypeDL.ErrorMessage;
                return null;
            }
            return resp ?? Response;
        }
        public FormTypeDTO FormTypeDelete(FormTypeDTO data)
        {
            //Search For Use This Item Before Delete
            if (!DeletePermision(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }
            data.IsDeleted = true;
            var Response = FormTypeDL.FormTypeUpdate(data);

            var resp = FormTypeGet(new FormTypeDTO { ID = Response?.ID ?? 0, IsDeleted = true })?.FirstOrDefault();
            Observers.ObserverStates.FormTypeDelete state = new Observers.ObserverStates.FormTypeDelete
            {
                FormType = resp ?? Response,
                User = User,
            };
            Notify(state);

            ResponseStatus = FormTypeDL.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += FormTypeDL.ErrorMessage;
                return null;
            }
            return resp ?? Response;
        }
    }
}
