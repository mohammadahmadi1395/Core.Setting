using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;
using Gostar.Setting.DA;

namespace Gostar.Setting.BL
{
    public class GeneratedFormBL : BaseBL
    {
        GeneratedFormDA GeneratedFormDA = new GeneratedFormDA();
        private bool Validate(long FormTypeID)
        {
            if(!(FormTypeID > 0))
            {
                ErrorMessage = "Mistake Form Type ID";
                return false;
            }

            //FormTypeBL FormType = new FormTypeBL();
            //var Count = FormType.FormTypeGet(new FormTypeDTO { ID = FormTypeID })?.Count;
            //if(Count>0)
            //{
            //    Form = FormType.FormTypeGet(new FormTypeDTO { ID = FormTypeID })?.FirstOrDefault();
            //}
            //else
            //{
            //    ErrorMessage = "This Form Type Not Exist";
            //    return false;
            //}

            return true;
        }
        public List<GeneratedFormDTO> GeneratedFormGet(GeneratedFormDTO data)
        {
            var Response = GeneratedFormDA.GeneratedFormGet(data);

            ResponseStatus = GeneratedFormDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += GeneratedFormDA.ErrorMessage;
                return null;
            }

            return Response;
        }
        public GeneratedFormDTO GenerateForm(long FormTypeID)
        {
            if (!Validate(FormTypeID))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }
            var Response = GeneratedFormDA.GenerateForm(new FormTypeDTO {ID=FormTypeID });

            ResponseStatus = GeneratedFormDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += GeneratedFormDA.ErrorMessage;
                return null;
            }
            return Response;

        }
    }
}
