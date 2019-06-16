using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;
using System.Runtime.Serialization;
using Gostar.Common;

namespace Gostar.Setting.SC.Messages
{
    public class GeneratedFormRequest
    {
        [DataMember]
        public UserInfoDTO User { get; set; }

        [DataMember]
        public Gostar.Common.ActionType? ActionType { get; set; }

        [DataMember]
        public GeneratedFormDTO RequestDTO { get; set; }

        [DataMember]
        public long FormTypeID { get; set; }
    }

    [DataContract]
    public class GeneratedFormResponse
    {
        [DataMember]
        public string ErrorMessage { get; set; }
        [DataMember]
        public Gostar.Common.ResponseStatus ResponseStatus { get; set; }
        [DataMember]
        public GeneratedFormDTO GeneratedForm { get; set; }
        [DataMember]
        public List<GeneratedFormDTO> ResponseDTOList { get; set; }


    }
}
