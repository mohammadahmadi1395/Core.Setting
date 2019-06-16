using System;
using Alsahab.Setting.Data.Contracts;
using Alsahab.Setting.DTO.Models;
using Alsahab.Setting.Entities;
using Alsahab.Setting.Entities.Models;
using Alsahab.Setting.WebFramework.Api;
using Microsoft.AspNetCore.Mvc;

namespace Alsahab.Setting.MyAPI.Controllers.v1
{
    [ApiVersion("1")]
    public class SubsystemController : CrudController<SubsystemDTO, Subsystem>
    {
        public SubsystemController(IRepository<Subsystem> repository) : base(repository)
        {
        }
    }
}