using Microsoft.AspNetCore.Mvc;
using Alsahab.Setting.BL;
using Alsahab.Setting.DTO;
using Alsahab.Setting.WebFramework.Filter;
using Microsoft.AspNetCore.Authorization;
using Alsahab.Setting.Entities.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading.Tasks;
using System.Collections.Generic;
using Alsahab.Setting.MyAPI;
using System.Threading;
using System;

namespace Alsahab.Setting.WebFramework.Api
{
    /// <summary>
    /// کنترلر مربوط به شعبه‌ها
    /// </summary>
    [ApiController]
    [ApiResultFilter]
    [AllowAnonymous]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    // public class BranchController : ControllerBase
    public class BranchController : CrudController<Branch, BranchDTO, BranchFilterDTO>
    {
        private readonly IDistributedCache _DistributedCache;
        
        /// <summary>
        /// سازنده کنترلر شعبه‌ها
        /// </summary>
        /// <param name="tBL"></param>
        /// <param name="distributedCache"></param>
        /// <returns></returns>
        public BranchController(IBaseBL<Branch, BranchDTO, BranchFilterDTO> tBL, IDistributedCache distributedCache) : base(tBL)
        {
            _DistributedCache = distributedCache;
        }

        /// <summary>
        /// test of redis cache
        /// </summary>
        /// <returns></returns>
        [Route("Test")]
        [HttpPost]
        public string Test()
        {
            var cachKey = "time";
            var existingTime = _DistributedCache.GetString(cachKey);
            if (!string.IsNullOrEmpty(existingTime))
            {
                return "Fetched from cache : " + existingTime;
            }
            else
            {
                existingTime = DateTime.UtcNow.ToString();
                _DistributedCache.SetString(cachKey, existingTime);
                return "Added to cache : " + existingTime;
            }
        }

    }
}