using Microsoft.AspNetCore.Mvc;
using Alsahab.Setting.BL;
using Alsahab.Setting.DTO;
using Alsahab.Setting.WebFramework.Filter;
using Microsoft.AspNetCore.Authorization;
using Alsahab.Setting.Entities.Models;

namespace Alsahab.Setting.WebFramework.Api
{
    /// <summary>
    /// کنترلر مربوط به چارت سازمانی
    /// </summary>
    [ApiController]
    [ApiResultFilter]
    [AllowAnonymous]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    // public class BranchController : ControllerBase
    public class OrganizationalChartController : CrudController<OrganizationalChart, OrganizationalChartDTO, OrganizationalChartFilterDTO>
    {
        /// <summary>
        /// سازنده کنترلر
        /// </summary>
        /// <param name="tBL"></param>
        /// <returns></returns>
        public OrganizationalChartController(IBaseBL<OrganizationalChart, OrganizationalChartDTO, OrganizationalChartFilterDTO> tBL) : base(tBL)
        {
        }
    }
}