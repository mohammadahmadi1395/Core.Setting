using His.Reception.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace His.Reception.Api.Infrastructure
{
    public class UserInfo
    {
        public static Login GetCurrentUser(HttpContext httpContext)
        {
          return(Login) httpContext.GetRouteValue("UserLoginInfo");
        }
    }
}
