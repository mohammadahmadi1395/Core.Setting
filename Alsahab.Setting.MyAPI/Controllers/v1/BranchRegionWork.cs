using Microsoft.AspNetCore.Mvc;
using Alsahab.Setting.BL;
using Alsahab.Setting.DTO;
using Alsahab.Setting.WebFramework.Filter;
using Microsoft.AspNetCore.Authorization;
using Alsahab.Setting.Entities.Models;

namespace Alsahab.Setting.WebFramework.Api
{
    /// <summary>
    /// کنترلر مربوط به نواحی کاری شعبه‌ها
    /// </summary>
    [ApiController]
    [ApiResultFilter]
    [AllowAnonymous]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    // public class BranchController : ControllerBase
    public class BranchRegionWorkController : CrudController<BranchRegionWork, BranchRegionWorkDTO, BranchRegionWorkFilterDTO>
    {
        /// <summary>
        /// سازنده کنترلر نواحی کاری شعبه‌ها
        /// </summary>
        /// <param name="tBL"></param>
        /// <returns></returns>
        public BranchRegionWorkController(IBaseBL<BranchRegionWork, BranchRegionWorkDTO, BranchRegionWorkFilterDTO> tBL) : base(tBL)
        {
        }
    }
}