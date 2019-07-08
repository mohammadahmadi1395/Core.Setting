using Microsoft.AspNetCore.Mvc;
using Alsahab.Setting.BL;
using Alsahab.Setting.DTO;
using Alsahab.Setting.WebFramework.Filter;
using Microsoft.AspNetCore.Authorization;
using Alsahab.Setting.Entities.Models;

namespace Alsahab.Setting.WebFramework.Api
{
    /// <summary>
    /// کنترلر مربوط به عبارات ترجمه
    /// </summary>
    [ApiController]
    [ApiResultFilter]
    [AllowAnonymous]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    // public class StatementController : ControllerBase
    public class StatementController : CrudController<Statement, StatementDTO, StatementFilterDTO>
    {
        /// <summary>
        /// سازنده کنترلر
        /// </summary>
        /// <param name="tBL"></param>
        /// <returns></returns>
        public StatementController(IBaseBL<Statement, StatementDTO, StatementFilterDTO> tBL) : base(tBL)
        {
        }
    }
}