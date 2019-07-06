using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Setting.DTO;
using Alsahab.Setting.BL;
using Alsahab.Setting.Entities.Models;
using Alsahab.Setting.Data.Interfaces;
using Alsahab.Common;

namespace Alsahab.Setting.BL
{
    public class GeneratedFormBL: BaseBL<GeneratedForm, GeneratedFormDTO, GeneratedFormFilterDTO>
    {
        private IBaseDL<GeneratedForm, GeneratedFormDTO, GeneratedFormFilterDTO> _GeneratedFormDL;
        private IBaseDL<Entities.Models.Log, LogDTO, LogFilterDTO> _LogDL;
        public GeneratedFormBL(IBaseDL<GeneratedForm, GeneratedFormDTO, GeneratedFormFilterDTO> generateFormDL,
                                IBaseDL<Entities.Models.Log, LogDTO, LogFilterDTO> logDL) :base(generateFormDL, logDL)
        {
            _GeneratedFormDL = generateFormDL;
            _LogDL = logDL;
        }

        //TODO: پرسیده شود که چیست؟        
        // public GeneratedFormDTO GenerateForm(long FormTypeID)
        // {
        //     var Response = GeneratedFormDA.GenerateForm(new FormTypeDTO {ID=FormTypeID });

        //     ResponseStatus = GeneratedFormDA.ResponseStatus;
        //     if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
        //     {
        //         ErrorMessage += GeneratedFormDA.ErrorMessage;
        //         return null;
        //     }
        //     return Response;

        // }
    }
}
