using System;
using System.Linq;
using Alsahab.Common;
using Alsahab.Common.Api;
using Alsahab.Setting.WebFramework.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Alsahab.Setting.WebFramework.Filter
{
    public class ApiResultFilterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is OkObjectResult okObjectResult)
            {
                var apiResult = new ApiResult<Object>(true, ResponseStatus.Successful, okObjectResult.Value);
                context.Result = new JsonResult(apiResult);
            }
            else if (context.Result is OkResult okResult)
            {
                var apiResult = new ApiResult(true, ResponseStatus.Successful);
                context.Result = new JsonResult(apiResult);
            }
            else if (context.Result is BadRequestObjectResult badRequestObjectResult)
            {
                var message = badRequestObjectResult.Value.ToString();
                if (badRequestObjectResult.Value is SerializableError error)
                {
                    var errorMessages = error.SelectMany(p=>(string[])p.Value).Distinct();
                    message = string.Join(" | ", errorMessages);
                }
                var apiResult = new ApiResult<Object>(false, ResponseStatus.BadRequest, message);
                context.Result = new JsonResult(apiResult);
            }
            else if (context.Result is BadRequestResult badRequestResult)
            {
                var apiResult = new ApiResult(false, ResponseStatus.BadRequest);
                context.Result = new JsonResult(apiResult);
            }
            else if (context.Result is ContentResult contentResult)
            {
                var apiResult = new ApiResult(true, ResponseStatus.Successful);
                context.Result = new JsonResult(apiResult);
            }
            else if (context.Result is NotFoundResult notFoundResult)
            {
                var apiResult = new ApiResult(false, ResponseStatus.NotFound);
                context.Result = new JsonResult(apiResult);
            }
            else if (context.Result is NotFoundObjectResult notFoundObjectResult)
            {
                var apiResult = new ApiResult<Object>(false, ResponseStatus.NotFound, notFoundObjectResult.Value);
                context.Result = new JsonResult(apiResult);
            }
            else if (context.Result is ObjectResult objectResult && objectResult.StatusCode == null && !(objectResult.Value is ApiResult))
            {
                var apiResult = new ApiResult<Object>(true, ResponseStatus.Successful, objectResult.Value);
                context.Result = new JsonResult(apiResult);
            }
            base.OnResultExecuting(context);
        }

    }
}