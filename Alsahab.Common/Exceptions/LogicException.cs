using System;
using Alsahab.Common;
using Alsahab.Common.Api;

namespace Alsahab.Common.Exceptions
{
    public class LogicException : AppException
    {
        public LogicException()
            : base(ResponseStatus.ServerError)
        {
        }

        public LogicException(string message)
            : base(message, ResponseStatus.ServerError)
        {
        }

        public LogicException(object additionalData)
            : base(ResponseStatus.ServerError, additionalData)
        {            
        }

        public LogicException(string message, object additionalData)
            : base(ResponseStatus.ServerError, message, additionalData)
        {            
        }

        public LogicException(string message, Exception exception, object additionalData)
            : base(ResponseStatus.ServerError, message, exception, additionalData)
        {            
        }
        public LogicException(string message, Exception exception)
            : base(ResponseStatus.ServerError, message, exception)
        {            
        }
    }
}