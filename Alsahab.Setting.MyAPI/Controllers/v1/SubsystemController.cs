using Microsoft.AspNetCore.Mvc;
using Alsahab.Setting.BL;
using Alsahab.Setting.DTO;
using Alsahab.Setting.WebFramework.Filter;
using Microsoft.AspNetCore.Authorization;
using Alsahab.Setting.Entities.Models;

namespace Alsahab.Setting.WebFramework.Api
{
    /// <summary>
    /// کنترلر مربوط به زیرسیستم‌ها
    /// </summary>
    [ApiController]
    [ApiResultFilter]
    [AllowAnonymous]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    // public class SubsystemController : ControllerBase
    public class SubsystemController : CrudController<Subsystem, SubsystemDTO, SubsystemFilterDTO>
    {
        /// <summary>
        /// سازنده کنترلر
        /// </summary>
        /// <param name="tBL"></param>
        /// <returns></returns>
        public SubsystemController(IBaseBL<Subsystem, SubsystemDTO, SubsystemFilterDTO> tBL) : base(tBL)
        {
        }
    }
}