using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using His.Reception.Api.Infrastructure;
using His.Reception.Application.Interface.Base;
using His.Reception.Application.Mapper;
using His.Reception.DTO;
using His.Reception.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using UAParser;

namespace His.Reception.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IBloodGroupService _bloodGroupService;

        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IStringLocalizer<ValuesController> _valuesControllerLocalizer;
        public ValuesController(IBloodGroupService bloodGroupService, IStringLocalizer<SharedResource> sharedLocalizer,
            IStringLocalizer<ValuesController> valuesControllerLocalizer)
        {
            _bloodGroupService = bloodGroupService;
            _sharedLocalizer = sharedLocalizer;
            _valuesControllerLocalizer = valuesControllerLocalizer;

        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            
            ////var val2=_valuesControllerLocalizer["Error"];
            ////var val = _sharedLocalizer["Error"];
            ////var ddd=_bloodGroupService.GetAll().GetAwaiter().GetResult();
            ////var sss= _valuesControllerLocalizer.GetAllStrings().Select(x => x.Name);
            //var userAgent = HttpContext.Request.Headers["User-Agent"].FirstOrDefault();
            ////string uaString = "Mozilla/5.0 (iPhone; CPU iPhone OS 5_1_1 like Mac OS X) AppleWebKit/534.46 (KHTML, like Gecko) Version/5.1 Mobile/9B206 Safari/7534.48.3";

            // get a parser with the embedded regex patterns
           // var uaParser = Parser.GetDefault();

            // get a parser using externally supplied yaml definitions
            // var uaParser = Parser.FromYaml(yamlString);

           // ClientInfo c = uaParser.Parse(userAgent);
            //////var map = (Job)BaseMapper.Map(new Job(),new BaseDto
            //////{
            //////    Code1 = "1",
            //////    Code2 = "2",
            //////    Id = 1,
            //////    Note = "",
            //////    Title = "t",
            //////    TitleLang2 = "t2",
            //////    IsAdmin=true


            //////});            
            
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        //[CustomAuthorization]
        public void Post([FromForm] string value)
        {
            var login = UserInfo.GetCurrentUser(HttpContext);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost("SetLanguage")]
        public IActionResult SetLanguage([FromBody] string culture)
        {

            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);

            _sharedLocalizer.WithCulture(new CultureInfo(culture));
            var val = _sharedLocalizer["Error"];
            return Ok("Success");


        }
    }
}
