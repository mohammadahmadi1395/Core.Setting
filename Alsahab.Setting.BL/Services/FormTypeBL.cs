using System.Threading.Tasks;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Entities.Models;
using Alsahab.Setting.DL.Interfaces;
using System.Threading;
using Alsahab.Common.Exceptions;
using Alsahab.Common;

namespace Alsahab.Setting.BL
{
    public class FormTypeBL : BaseBL<FormType, FormTypeDTO, FormTypeFilterDTO>
    {
        private readonly IBaseDL<FormType, FormTypeDTO, FormTypeFilterDTO> _FormTypeDL;
        public FormTypeBL(IBaseDL<FormType, FormTypeDTO, FormTypeFilterDTO> formTypeDL,
                                IBaseDL<Entities.Models.Log, LogDTO, LogFilterDTO> logDL) : base(formTypeDL, logDL)
        {
            _FormTypeDL = formTypeDL;
        }

        public async override Task CheckDeletePermisionAsync(FormTypeDTO data, CancellationToken cancellationToken)
        {
            await base.CheckDeletePermisionAsync(data, cancellationToken);

            if (_FormTypeDL.GetById(data.ID).Enum != null)
                throw new AppException(ResponseStatus.LoginError, "This Type Is Non Deleteable");
        }
    }
}
