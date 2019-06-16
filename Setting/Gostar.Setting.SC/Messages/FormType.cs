using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;

namespace Gostar.Setting.SC.Messages
{

    public class FormTypeRequest : BaseRequest<FormTypeDTO>
    {
        public FormTypeFilterDTO FormTypeFilter { get; set; }
    }
    public class FormTypeResponse : BaseResponse<FormTypeDTO>
    {

    }
}
