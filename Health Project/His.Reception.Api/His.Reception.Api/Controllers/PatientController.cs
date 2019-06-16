using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using His.Reception.Api.Infrastructure;
using His.Reception.Application.Interface;
using His.Reception.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace His.Reception.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost("ListPatient")]
        [CustomAuthorization()]
        public async Task<IActionResult> GetPatient(FilterPatientDto filterPatientDto)
        {
            var result = await _patientService.GetListPatient(filterPatientDto);

            return new JsonResult(result);
        }

        [HttpPost("FindPatient")]
        [CustomAuthorization(Role = "")]
        public async Task<IActionResult> FindPatient(SearchPatientDto searchPatientDto)
        {
            var result = await _patientService.FindPatient(searchPatientDto);

            return new JsonResult(result);
        }

        [HttpGet("PatientBaseInfo")]
        [CustomAuthorization()]
        public async Task<IActionResult> PatientBaseInfo()
        {
            var result = await _patientService.GetPatientBaseInfo();

            return new JsonResult(result);
        }

        [HttpGet("{id}")]
        [CustomAuthorization(Role = "")]
        public async Task<IActionResult> GetPatient(long id)
        {
            var result =await _patientService.GetPatientByID(id);

            if (result.Status==DTO.Message.ResponseStatus.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }

        }

        [HttpPost]
        [CustomAuthorization(Role = "InsertPatient")]
        public async Task<IActionResult> AddPatient(PatientDto patientdto)
        {
            try
            {
                var result = await _patientService.AddAsync(patientdto);

                if (result.Status == DTO.Message.ResponseStatus.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        [HttpPut]
        [CustomAuthorization(Role = "")]
        public async Task<IActionResult> EditPatient(PatientDto patientdto)
        {
            try
            {
                var result = await _patientService.EditAsync(patientdto);

                if (result.Status == DTO.Message.ResponseStatus.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return null;

            }
        }
    }
}