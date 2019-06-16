using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using His.Reception.Api.Infrastructure;
using His.Reception.Application.Interface;
using His.Reception.DTO;
using His.Reception.DTO.Message;
using His.Reception.DTO.User;
using His.Reception.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace His.Reception.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserManagerService _userManager;
        //private readonly IRedisCacheService _distributedCache;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        public AccountController(IUserManagerService userManager/*, IRedisCacheService distributedCache*/,
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _userManager = userManager;
            // _distributedCache = distributedCache;
            _sharedLocalizer = sharedLocalizer;
        }

        [HttpGet]
        public IActionResult TestRedis()
        {
            //_distributedCache.AddAsync("rediscondddd", "world").Wait();
            //var valueFromRedis = _distributedCache.GetAsync("helloFromRedis").Result;
            return null;
        }

        [HttpPost("Login")]
        //[custem]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
           var val = _sharedLocalizer["Not Select Language"];
           var result= await _userManager.Login(loginDto);

            if (result.Status==ResponseStatus.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("Logout")]
        
        public async Task<IActionResult> Logout([FromBody] Guid token)
        {
            
            var result = await _userManager.Logout(token);

            if (result.Status == ResponseStatus.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("CheckToken")]
        [CustomAuthorization()]

        public async  Task<IActionResult> CheckToken([FromForm] string token)
        {
            return Ok(new BaseResponseDto {
                Status=ResponseStatus.Success,
                Message="Token Is Valid"
            });
        }

        //public IActionResult CheckToken(Guid token)
        //{
        //    return null;
        //}

    }
}