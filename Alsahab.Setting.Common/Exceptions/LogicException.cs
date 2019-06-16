using System;
using Alsahab.Setting.Common.Api;

namespace Alsahab.Setting.Common.Exceptions
{
    public class LogicException : AppException
    {
        public LogicException()
            : base(ApiResultStatusCode.LogicError)
        {
        }

        public LogicException(string message)
            : base(message, Api.ApiResultStatusCode.LogicError)
        {
        }

        public LogicException(object additionalData)
            : base(ApiResultStatusCode.LogicError, additionalData)
        {            
        }

        public LogicException(string message, object additionalData)
            : base(ApiResultStatusCode.LogicError, message, additionalData)
        {            
        }

        public LogicException(string message, Exception exception, object additionalData)
            : base(ApiResultStatusCode.LogicError, message, exception, additionalData)
        {            
        }
        public LogicException(string message, Exception exception)
            : base(ApiResultStatusCode.LogicError, message, exception)
        {            
        }
    }
}