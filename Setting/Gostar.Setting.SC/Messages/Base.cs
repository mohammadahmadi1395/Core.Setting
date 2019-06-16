using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Gostar.Setting.DTO;
using Gostar.Common;

namespace Gostar.Setting.SC.Messages
{
    [DataContract]
    public class Base
    {
    }

    [DataContract]
    public class BaseRequest<T>
    {
        [DataMember]
        public UserInfoDTO User { get; set; }
        [DataMember]
        public long? RequestID { get; set; }
        [DataMember]
        public Gostar.Common.ActionType ActionType { get; set; }
        [DataMember]
        public T RequestDto { get; set; }
        [DataMember]
        public List<T> RequestDtoList { get; set; }

        [DataMember]
        public Language? Language { get; set; }

        [DataMember]
        public PagingInfoDTO PagingInfo { get; set; }
        // [DataMember]
        // public Gostar.Common.Language Language { get; set; } = Language.ar_IQ;
    }

    [DataContract]
    public class BaseResponse<T>
    {
        [DataMember]
        public string ErrorMessage { get; set; }
        [DataMember]
        public Gostar.Common.ResponseStatus ResponseStatus { get; set; }
        [DataMember]
        public T ResponseDto { get; set; }
        [DataMember]
        public List<T> ResponseDtoList { get; set; }

        [DataMember]
        public IList<Gostar.Common.Validation.Results.ValidationFailure> ValidationErrors { get; set; }
        [DataMember]
        public int? ResultCount { get; set; }
    }
}


