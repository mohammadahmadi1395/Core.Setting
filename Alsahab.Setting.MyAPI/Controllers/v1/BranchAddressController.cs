using Microsoft.AspNetCore.Mvc;
using Alsahab.Setting.BL;
using Alsahab.Setting.DTO;
using Alsahab.Setting.WebFramework.Filter;
using Microsoft.AspNetCore.Authorization;
using Alsahab.Setting.Entities.Models;

namespace Alsahab.Setting.WebFramework.Api
{
    /// <summary>
    /// کنترلر مربوط به آدرس شعبه‌ها
    /// </summary>
    [ApiController]
    [ApiResultFilter]
    [AllowAnonymous]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    // public class BranchController : ControllerBase
    public class BranchAddressController : CrudController<BranchAddress, BranchAddressDTO, BranchAddressFilterDTO>
    {
        /// <summary>
        /// سازنده کنترلر شعبه‌ها
        /// </summary>
        /// <param name="tBL"></param>
        /// <returns></returns>
        public BranchAddressController(IBaseBL<BranchAddress, BranchAddressDTO, BranchAddressFilterDTO> tBL) : base(tBL)
        {
        }
    }
}