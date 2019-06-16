using System;
using Alsahab.Setting.WebFramework.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alsahab.Setting.WebFramework.Api
{
    [ApiController]
    [ApiResultFilter]
    // [AllowAnonymous]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")] // api/v1/post
    // [Route("api/[controller]")] // api/post
    public class BaseController : ControllerBase
    {
        public virtual bool UserIsAuthenticated => HttpContext.User.Identity.IsAuthenticated;
    }
}