using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using His.Reception.Application.Interface;
using His.Reception.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace His.Reception.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly IStringLocalizer<ValuesController> _valuesControllerLocalizer;
        public DoctorController(IDoctorService doctorService,IStringLocalizer<ValuesController> valuesControllerLocalizer)
        {
            _doctorService = doctorService;
            _valuesControllerLocalizer = valuesControllerLocalizer;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctor(int id)
        {
            var val2 = _valuesControllerLocalizer["Error"];
            var result = await _doctorService.GetDoctoryById(id);

            if (result.Status == DTO.Message.ResponseStatus.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddDoctor(DoctorDto doctorDto)
        {
            var result=await _doctorService.AddAsync(doctorDto);

            if (result.Status == DTO.Message.ResponseStatus.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditDoctor(DoctorDto doctorDto)
        {
            var result = await _doctorService.EditAsync(doctorDto);

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