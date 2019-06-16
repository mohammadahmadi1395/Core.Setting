using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using His.Reception.DTO.Message;
using His.Reception.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace His.Reception.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public ResourceController(IStringLocalizer<SharedResource> stringLocalizer)
        {
            _sharedLocalizer = stringLocalizer;
        }

        [HttpGet("{lang}")]
        public IActionResult GetResource(string lang)
        { 
            var resourceSet = _sharedLocalizer.WithCulture(new CultureInfo(lang))
                                                        .GetAllStrings().Select(x =>new  {
                                                            Name=x.Name,
                                                            Value=x.Value
                                                        });

            return Ok(new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = resourceSet
            });
        }
    }
}