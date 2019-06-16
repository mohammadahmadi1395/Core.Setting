using His.Reception.Application.Interface;
using His.Reception.Application.Service;
using His.Reception.DTO;
using His.Reception.DTO.User;
using His.Reception.Entities.Models;
using His.Reception.Infrastructure;
using His.Reception.Infrastructure.Caching;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace His.Reception.Api.Infrastructure
{
    public class CustomAuthorization : ActionFilterAttribute
    {
        public string Role { get; set; }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var redisCacheService =(IRedisCacheService) context.HttpContext.RequestServices.GetRequiredService(typeof(IRedisCacheService));
            var userManagerService = (IUserManagerService)context.HttpContext.RequestServices.GetRequiredService(typeof(IUserManagerService));
            var sharedLocalizer = (IStringLocalizer<SharedResource>)context.HttpContext.RequestServices.GetRequiredService(typeof(IStringLocalizer<SharedResource>));
            

            var token = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ","");
            var sectionId = context.HttpContext.Request.Headers["SectionId"].ToString();
            var lang = context.HttpContext.Request.Headers["Accept-Language"].ToString();
            //context.HttpContext.Request.Headers.Add("Accept-Language", "fa-ir");
            

            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedObjectResult("Token Not Send"); //new JsonResult(new { HttpStatusCode.Unauthorized });
                return;
            }

            if (string.IsNullOrEmpty(sectionId))
            {
                context.Result = new UnauthorizedObjectResult("Section Not Send"); //new JsonResult(new { HttpStatusCode.Unauthorized });
                return;
            }
            //string userName= userManagerService.ValidToken(token).GetAwaiter().GetResult();

            //if (string.IsNullOrEmpty(userName))
            //    context.Result = new JsonResult(new { HttpStatusCode.Unauthorized });

            //if (string.IsNullOrEmpty(userName))
            //{
            //    var jsonRefreshId = redisCacheService.GetAsync(token).GetAwaiter().GetResult();
            //    var refreshDto = JsonConvert.DeserializeObject<RefreshIdDto>(jsonRefreshId);

            //    if (refreshDto.ExpDate > DateTime.Now)
            //    {
            //        userManagerService.RefreshToken(refreshDto.RefreshID).GetAwaiter();
            //    }
            //    else
            //    {
            //        context.Result = new JsonResult(new { HttpStatusCode.Unauthorized });
            //    }
            //}

            #region check Token and get User

            var result = userManagerService.CheckToken(Guid.Parse(token)).GetAwaiter().GetResult();

            if(result is null)
            {
                context.Result = new UnauthorizedObjectResult("Token Expire or Miss"); //new JsonResult(new { HttpStatusCode.Unauthorized });
                return;
            }

            #endregion

            #region Set language

            if (!string.IsNullOrEmpty(lang))
            {
                var cultureInfo = CultureInfo.GetCultureInfo(result.Language);

                Thread.CurrentThread.CurrentCulture = cultureInfo;
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
            }

            #endregion

            #region Get Access user

            if (!string.IsNullOrEmpty(Role))
            {
                var resultJson = redisCacheService.GetAsync(token).GetAwaiter().GetResult();
                var userPermission = JsonConvert.DeserializeObject<List<PermissionDto>>(resultJson);
                var findPermission = userPermission.Where(up => up.SectionId ==Convert.ToInt32(sectionId) && up.UserId == result.UserId && up.PermissionName == Role).FirstOrDefault();

                if (findPermission is null)
                {
                    context.Result = new UnauthorizedObjectResult("Not Access To Action"); //new JsonResult(new { HttpStatusCode.Unauthorized });
                    return;
                }
            }

            #endregion

            #region set Context User Identity

            context.RouteData.Values.Add("UserLoginInfo", result);
           
            var identity = (ClaimsIdentity)context.HttpContext.User.Identity;
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            identity.AddClaim(new Claim(ClaimTypes.Name, result.Id.ToString()));
            identity.AddClaim(new Claim("Token", result.Token.ToString()));
            identity.AddClaim(new Claim("Language", result.Language.ToString()));

            #endregion

            base.OnActionExecuting(context);
        }
    }
}
