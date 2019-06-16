using System;
using Alsahab.Setting.Common.Api;

namespace Alsahab.Setting.Common.Exceptions
{
    public class BadRequestException : AppException
    {
        public BadRequestException()
            : base(ApiResultStatusCode.BadRequest)
        {
        }

        public BadRequestException(string message)
            : base(message, Api.ApiResultStatusCode.BadRequest)
        {
        }

        public BadRequestException(object additionalData)
            : base(ApiResultStatusCode.BadRequest, additionalData)
        {            
        }

        public BadRequestException(string message, object additionalData)
            : base(ApiResultStatusCode.BadRequest, message, additionalData)
        {            
        }

        public BadRequestException(string message, Exception exception, object additionalData)
            : base(ApiResultStatusCode.BadRequest, message, exception, additionalData)
        {            
        }
        public BadRequestException(string message, Exception exception)
            : base(ApiResultStatusCode.BadRequest, message, exception)
        {            
        }
    }
}