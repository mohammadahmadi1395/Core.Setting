using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace His.Reception.Api.Infrastructure
{
    public class ValidateFilterAttribute : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);

            //model valid not pass  
            if (!context.ModelState.IsValid)
            {
                var entry = context.ModelState.Values.FirstOrDefault();

                var message = entry.Errors.FirstOrDefault().ErrorMessage;

                //modify the result  
                context.Result = new BadRequestObjectResult(new
                {
                    code = -1,
                    data = new JObject(),
                    msg = message,
                });
            }
        }
    }
}
