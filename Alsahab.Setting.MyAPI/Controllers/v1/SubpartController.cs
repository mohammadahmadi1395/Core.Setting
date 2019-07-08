using Microsoft.AspNetCore.Mvc;
using Alsahab.Setting.BL;
using Alsahab.Setting.DTO;
using Alsahab.Setting.WebFramework.Filter;
using Microsoft.AspNetCore.Authorization;
using Alsahab.Setting.Entities.Models;

namespace Alsahab.Setting.WebFramework.Api
{
    /// <summary>
    /// کنترلر مربوط به زیربخش‌ها
    /// </summary>
    [ApiController]
    [ApiResultFilter]
    [AllowAnonymous]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    // public class SubpartController : ControllerBase
    public class SubpartController : CrudController<Subpart, SubpartDTO, SubpartFilterDTO>
    {
        /// <summary>
        /// سازنده کنترلر
        /// </summary>
        /// <param name="tBL"></param>
        /// <returns></returns>
        public SubpartController(IBaseBL<Subpart, SubpartDTO, SubpartFilterDTO> tBL) : base(tBL)
        {
        }
    }
}