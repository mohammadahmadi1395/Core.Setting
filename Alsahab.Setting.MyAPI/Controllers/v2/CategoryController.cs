// using System;
// using System.Collections.Generic;
// using System.Threading;
// using System.Threading.Tasks;
// using Alsahab.Setting.Data.Contracts;
// using Alsahab.Setting.Entities;
// using Alsahab.Setting.MyAPI.Models;
// using Alsahab.Setting.WebFramework.Api;
// using Microsoft.AspNetCore.Mvc;

// namespace Alsahab.Setting.MyAPI.Controllers.v2
// {
//     [ApiVersion("2")]
//     public class CategoryController : v1.CategoryController
//     {
//         public CategoryController(IRepository<Category> repository) : base(repository)
//         {
//         }

//         public override Task<ActionResult<List<CategoryDto>>> Get(CancellationToken cancellationToken)
//         {
//             return base.Get(cancellationToken);
//         }

//         public override Task<ActionResult<CategoryDto>> Get(Guid id, CancellationToken cancellationToken)
//         {
//             return base.Get(id, cancellationToken);
//         }
        
//         public override Task<ApiResult<CategoryDto>> Create(CategoryDto dto, CancellationToken cancellationToken)
//         {
//             return base.Create(dto, cancellationToken);
//         }

//         public override Task<ApiResult<CategoryDto>> Update(CategoryDto dto, CancellationToken cancellationToken)
//         {
//             return base.Update(dto, cancellationToken);
//         }

//         public override Task<ApiResult> Delete(Guid id, CancellationToken cancellationToken)
//         {
//             return base.Delete(id, cancellationToken);
//         }

//     }
// }