using Microsoft.AspNetCore.Mvc;
using Alsahab.Setting.BL;
using Alsahab.Setting.DTO;
using Alsahab.Setting.WebFramework.Filter;
using Microsoft.AspNetCore.Authorization;
using Alsahab.Setting.Entities.Models;

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
    public class GeneratedFormController : CrudController<GeneratedForm, GeneratedFormDTO, GeneratedFormFilterDTO>
    {
        /// <summary>
        /// سازنده کنترلر شعبه‌ها
        /// </summary>
        /// <param name="tBL"></param>
        /// <returns></returns>
        public GeneratedFormController(IBaseBL<GeneratedForm, GeneratedFormDTO, GeneratedFormFilterDTO> tBL) : base(tBL)
        {
        }
    }
}