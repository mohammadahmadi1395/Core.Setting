using System;
using Alsahab.Common;
using Alsahab.Common.Api;

namespace Alsahab.Common.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException()
            : base(ResponseStatus.NotFound)
        {
        }

        public NotFoundException(string message)
            : base(message, ResponseStatus.NotFound)
        {
        }

        public NotFoundException(object additionalData)
            : base(ResponseStatus.NotFound, additionalData)
        {            
        }

        public NotFoundException(string message, object additionalData)
            : base(ResponseStatus.NotFound, message, additionalData)
        {            
        }

        public NotFoundException(string message, Exception exception, object additionalData)
            : base(ResponseStatus.NotFound, message, exception, additionalData)
        {            
        }
        public NotFoundException(string message, Exception exception)
            : base(ResponseStatus.NotFound, message, exception)
        {            
        }
    }
}