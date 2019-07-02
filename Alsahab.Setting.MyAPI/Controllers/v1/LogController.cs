using Microsoft.AspNetCore.Mvc;
using Alsahab.Setting.BL;
using Alsahab.Setting.DTO;
using Alsahab.Setting.WebFramework.Filter;
using Microsoft.AspNetCore.Authorization;
using Alsahab.Setting.Entities.Models;
using Alsahab.Common;

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
    public class LogController : CrudController<Log, LogDTO, LogFilterDTO>
    {
        /// <summary>
        /// سازنده کنترلر شعبه‌ها
        /// </summary>
        /// <param name="tBL"></param>
        /// <returns></returns>
        public LogController(IBaseBL<Log, LogDTO, LogFilterDTO> tBL) : base(tBL)
        {
        }
    }
}