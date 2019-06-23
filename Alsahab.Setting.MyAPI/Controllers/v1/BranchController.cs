using Microsoft.AspNetCore.Mvc;
using Alsahab.Setting.BL;
using Alsahab.Setting.DTO;
using Alsahab.Setting.WebFramework.Filter;
using Microsoft.AspNetCore.Authorization;
using Alsahab.Setting.Entities.Models;

namespace Alsahab.Setting.WebFramework.Api
{
    [ApiController]
    [ApiResultFilter]
    [AllowAnonymous]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")] // api/v1/post
    // public class BranchController : ControllerBase // BaseController
    public class BranchController : CrudController<Branch, BranchDTO, BranchFilterDTO>
    {
        public BranchController(IBaseBL<Branch, BranchDTO, BranchFilterDTO> tBL) : base(tBL)
        {
        }
    }
}