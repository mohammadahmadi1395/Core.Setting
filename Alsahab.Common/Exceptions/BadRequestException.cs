using System;
using Alsahab.Common;
using Alsahab.Common.Api;

namespace Alsahab.Common.Exceptions
{
    public class BadRequestException : AppException
    {
        public BadRequestException()
            : base(ResponseStatus.BadRequest)
        {
        }

        public BadRequestException(string message)
            : base(message, ResponseStatus.BadRequest)
        {
        }

        public BadRequestException(object additionalData)
            : base(ResponseStatus.BadRequest, additionalData)
        {            
        }

        public BadRequestException(string message, object additionalData)
            : base(ResponseStatus.BadRequest, message, additionalData)
        {            
        }

        public BadRequestException(string message, Exception exception, object additionalData)
            : base(ResponseStatus.BadRequest, message, exception, additionalData)
        {            
        }
        public BadRequestException(string message, Exception exception)
            : base(ResponseStatus.BadRequest, message, exception)
        {            
        }
    }
}