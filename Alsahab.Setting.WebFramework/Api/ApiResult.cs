using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Alsahab.Common;
using Alsahab.Common.Api;
using Alsahab.Common.Utilities;
using Alsahab.Setting.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Alsahab.Setting.WebFramework.Api
{
    public class ApiResult
    {
        public ApiResult(bool isSuccess, ResponseStatus responseStatus, string errorMessage = null)
        {
            ResponseStatus = responseStatus;
            IsSuccess = isSuccess;
            // StatusCode = statusCode;
            ErrorMessage = errorMessage ?? responseStatus.ToDisplay();
        }
        public bool IsSuccess { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
        // public ApiResultStatusCode StatusCode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorMessage { get; set; }

        #region implicit operators
        public static implicit operator ApiResult(OkResult result)
        {
            return new ApiResult(true, ResponseStatus.Successful);
        }

        public static implicit operator ApiResult(BadRequestResult result)
        {
            return new ApiResult(false, ResponseStatus.BadRequest);
        }

        public static implicit operator ApiResult(BadRequestObjectResult result)
        {
            var message = result.Value.ToString();
            if (result.Value is SerializableError errors)
            {
                var errorMessage = errors.SelectMany(p => (string[])p.Value).Distinct();
                message = string.Join(" | ", errorMessage);
            }
            return new ApiResult(false, ResponseStatus.BadRequest, message);
        }

        public static implicit operator ApiResult(ContentResult result)
        {
            return new ApiResult(true, ResponseStatus.Successful, result.Content);
        }

        public static implicit operator ApiResult(NotFoundResult result)
        {
            return new ApiResult(false, ResponseStatus.NotFound);
        }
    }
    #endregion
    public class ApiResult<T> : ApiResult
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public T ResponseDTO { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public T ResponseDTOList { get; set; }

        public ApiResult(bool isSuccess, ResponseStatus statusCode, T responseDTO, T responseDTOList = default(T), string message = null)
        : base(isSuccess, statusCode, message)
        {
            ResponseDTO = responseDTO;
            ResponseDTOList = responseDTOList;
        }

        #region implicit operators
        public static implicit operator ApiResult<T>(T data)
        {
            try
            {
                var typeOfData = data.GetType().GetTypeInfo().GenericTypeArguments[0];
                return new ApiResult<T>(true, ResponseStatus.Successful, default(T), (T)data);
            }
            catch
            {
                return new ApiResult<T>(true, ResponseStatus.Successful, data);
            }
        }
        public static implicit operator ApiResult<T>(OkResult result)
        {
            return new ApiResult<T>(true, ResponseStatus.Successful, default(T));
        }

        public static implicit operator ApiResult<T>(OkObjectResult result)
        {
            try
            {
                var typeOfData = result.Value.GetType().GetTypeInfo().GenericTypeArguments[0];
                return new ApiResult<T>(true, ResponseStatus.Successful, default, (T)(result.Value));
            }
            catch// (Exception ex)
            {
                return new ApiResult<T>(true, ResponseStatus.Successful, (T)(result.Value));
            }

            // return new ApiResult<TData>(true, ResponseStatus.Successful, (TData)result.Value);
        }

        public static implicit operator ApiResult<T>(BadRequestResult result)
        {
            return new ApiResult<T>(false, ResponseStatus.BadRequest, default(T));
        }

        public static implicit operator ApiResult<T>(BadRequestObjectResult result)
        {
            var message = result.Value.ToString();
            if (result.Value is SerializableError errors)
            {
                var errorMessage = errors.SelectMany(p => (string[])p.Value).Distinct();
                message = string.Join(" | ", errorMessage);
            }
            return new ApiResult<T>(false, ResponseStatus.BadRequest, default(T), default(T), message);
        }

        public static implicit operator ApiResult<T>(ContentResult result)
        {
            return new ApiResult<T>(true, ResponseStatus.Successful, default(T), default(T), result.Content);
        }

        public static implicit operator ApiResult<T>(NotFoundResult result)
        {
            return new ApiResult<T>(false, ResponseStatus.NotFound, default(T));
        }

        public static implicit operator ApiResult<T>(NotFoundObjectResult result)
        {
            return new ApiResult<T>(false, ResponseStatus.NotFound, (T)result.Value);
        }
    }
    #endregion
}

