using System;
using System.Net;
using Alsahab.Common;
using Alsahab.Common.Api;

namespace Alsahab.Common.Exceptions
{
    public class AppException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public ResponseStatus ApiStatusCode { get; set; }
        public object AdditionalData { get; set; }

        public AppException(ResponseStatus statusCode, string message, HttpStatusCode httpStatusCode, Exception exception, object additionalData)
            : base(message, exception)
        {
            ApiStatusCode = statusCode;
            HttpStatusCode = httpStatusCode;
            AdditionalData = AdditionalData;
        }

        public AppException(ResponseStatus statusCode, string message, HttpStatusCode httpStatusCode, Exception exception)
            : this(statusCode, message, httpStatusCode, exception, null)
        {            
        }
        public AppException(ResponseStatus statusCode, string message, Exception exception, object additionalData)
            : this(statusCode, message, HttpStatusCode.InternalServerError, exception, additionalData)
        {            
        }

        public AppException(ResponseStatus statusCode, string message, HttpStatusCode httpStatusCode)
            : this(statusCode, message, httpStatusCode, null)
        {            
        }
        public AppException(ResponseStatus statusCode, object additionalData)
            : this(statusCode, null, additionalData)
        {            
        }

        public AppException(ResponseStatus statusCode, string message, object additionalData)
            : this(statusCode, message, HttpStatusCode.InternalServerError, null, additionalData)
        {            
        }

        public AppException(ResponseStatus statusCode, string message, Exception exception)
            : this(statusCode, message, exception, null)
        {            
        }

        public AppException(ResponseStatus statusCode, string message)
            : this(statusCode, message, HttpStatusCode.InternalServerError)
        {
        }

        public AppException(string message)
            : this(ResponseStatus.ServerError, message)
        {
        }
        public AppException(ResponseStatus statusCode)
            : this(statusCode, null)
        {
        }
        public AppException()
            : this(ResponseStatus.ServerError)
        {

        }

        public AppException(string message, ResponseStatus statusCode)
            : this(statusCode, message)
        {
        }
    }
}