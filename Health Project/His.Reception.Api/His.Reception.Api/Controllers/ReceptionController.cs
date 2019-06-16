using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using His.Reception.Application.Interface;
using His.Reception.DTO;
using Microsoft.AspNetCore.Mvc;

namespace His.Reception.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReceptionController : Controller
    {
        private readonly IReceptionsService _receptionService;
        public ReceptionController(IReceptionsService receptionService)
        {
            _receptionService = receptionService;
        }


        [HttpPost]
        public async Task<IActionResult> Add(ReceptionDto receptionDto)
        {
           var result=await _receptionService.AddAsync(receptionDto);

            if(result.Status==DTO.Message.ResponseStatus.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("ListReception")]
        public async Task<IActionResult> ListReception(FilterReceptionDto filterReceptionDto)
        {
            var result = await _receptionService.GetListReception(filterReceptionDto);

            if (result.Status == DTO.Message.ResponseStatus.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}