// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading;
// using System.Threading.Tasks;
// using AutoMapper;
// using AutoMapper.QueryableExtensions;
// using Alsahab.Setting.DL.Contracts;
// using Alsahab.Setting.Entities;
// using Alsahab.Setting.MyAPI.Models;
// using Alsahab.Setting.WebFramework.Api;
// using Alsahab.Setting.WebFramework.Filter;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;

// namespace Alsahab.Setting.MyAPI.Controllers.v1
// {
//     [ApiController]
//     [AllowAnonymous]
//     [ApiResultFilter]
//     [Route("api/[controller]")]
//     [ApiVersion("1")]
//     public class OldPostController : ControllerBase
//     {
//         private readonly IRepository<Post> _repository;
//         public OldPostController(IRepository<Post> repository)
//         {
//             _repository = repository;
//         }

//         [HttpGet]
//         public async Task<ActionResult<List<PostDto>>> Get(CancellationToken cancellationToken)
//         {
//             var entityList = _repository.TableNoTracking;
//             // var dtoList = await entityList.Select(p => AutoMapper.Mapper.Map<PostDto>(p)).ToListAsync(cancellationToken);
//             //روش بهتر  در اینجا استفاده از 
//             // ToProject
//             // است که باعث می‌شود هم نگاشت انجام شود و هم 
//             // navigation property
//             // ها را به صورت هوشمند اینکلود می‌کند و فیلدهای اضافی دیگر را اینکلود نمی‌کند.
//             var dtoList = await entityList.ProjectTo<PostDto>()
//                 //Where(dto=>dto.CategoryName.Contains("test"))
//                 .ToListAsync(cancellationToken);
//             return Ok(dtoList);

//             #region  oldcode
//             // var list = await _repository.TableNoTracking.Select(p => new PostDto
//             // {
//             //     Id = p.Id,
//             //     AuthorId = p.AuthorId,
//             //     AuthorFullName = p.Author.FullName,
//             //     CategoryId = p.CategoryId,
//             //     CategoryName = p.Category.Name,
//             //     Description = p.Description,
//             //     Title = p.Title
//             // }).ToListAsync(cancellationToken);
//             // return Ok(list);
//             #endregion

//         }

//         [HttpGet("{id:guid}")]
//         public async Task<ActionResult<PostDto>> Get(Guid id, CancellationToken cancellationToken)
//         {
//             // این روش، 
//             // navigation properties
//             // را نمی‌دهد
//             // var post = await _repository.GetByIdAsync(cancellationToken, id);
            
//             // روش زیر هم همه فیلدهای
//             // navigation properties
//             // را می‌آورد که مطلوب نیست
//             // var post = await _repository.TableNoTracking
//             //     .Include(p => p.Category)
//             //     .Include(q => q.Author)
//             //     .SingleOrDefaultAsync(s => s.Id == id, cancellationToken);


//             var dto = await _repository.TableNoTracking.ProjectTo<PostDto>().SingleOrDefaultAsync(s=>s.Id == id, cancellationToken);
//             if (dto == null)
//                 return NotFound();
//             return dto;
            
//             #region old code
//             // return new PostDto
//             // {
//             //     Id = post.Id,
//             //     AuthorId = post.AuthorId,
//             //     AuthorFullName = post.Author.FullName,
//             //     CategoryId = post.CategoryId,
//             //     CategoryName = post.Category.Name,
//             //     Description = post.Description,
//             //     Title = post.Title
//             // };
//             #endregion
//         }


//         #region mapping examples
//         //var postDto = new PostDto();
//         //Create: (dto => new entity)
//         //var post = postDto.ToEntity();
//         //Update: (dto => exist entity)
//         //var post = postDto.ToEntity(entity);
//         //GetById: (entity => dto)            
//         //var newDto = PostDto.FromEntity(entity); (Note: mehod is static (PostDto not postDto))
//         #endregion
//         [HttpPost]
//         public async Task<ApiResult<PostDto>> CreateAsync(PostDto dto, CancellationToken cancellationToken)
//         {
//             // var post = Mapper.Map<Post>(dto);
//             var post = dto.ToEntity();

//             #region old code
//             // Post post = new Post
//             // {
//             //     AuthorId = dto.AuthorId,
//             //     CategoryId = dto.CategoryId,
//             //     Description = dto.Description,
//             //     Title = dto.Title
//             // };
//             #endregion

//             await _repository.AddAsync(post, cancellationToken);

//             #region new code1            
//             // post = await _repository.TableNoTracking
//             //     .Include(p => p.Category)
//             //     .Include(q => q.Author)
//             //     .SingleOrDefaultAsync(s => s.Id == post.Id);
//             // var resultDto = Mapper.Map<PostDto>(post);
//             #endregion

//             var resultDto = await _repository.TableNoTracking.ProjectTo<PostDto>()
//                 .SingleOrDefaultAsync(s => s.Id == post.Id, cancellationToken);

//             #region old code
//             // var resultDto = new PostDto
//             // {
//             //     AuthorId = post.AuthorId,
//             //     AuthorFullName = post.Author.FullName,
//             //     CategoryId = post.CategoryId,
//             //     CategoryName = post.Category.Name,
//             //     Description = post.Description,
//             //     Id = post.Id,
//             //     Title = post.Title,
//             // };
//             #endregion

//             return resultDto;
//         }

//         [HttpPut]
//         public async Task<ApiResult<PostDto>> Update(PostDto dto, CancellationToken cancellationToken)
//         {
//             var model = await _repository.GetByIdAsync(cancellationToken, dto.Id);
//             if (model == null)
//                 return NotFound();
//             #region  old code
//             // model.Title = dto.Title;
//             // model.AuthorId = dto.AuthorId;
//             // model.CategoryId = dto.CategoryId;
//             // model.Description = dto.Description;
//             #endregion

//             //نکته: با توجه به این که ما می‌خواهیم تغییرات در خود 
//             // model
//             // انجام شود، باید تابع مپر را به صورت زیر به کار ببریم
//             // Mapper.Map(dto, model);
//             model = dto.ToEntity(model);

//             await _repository.UpdateAsync(model, cancellationToken);

//             #region old code
//             // var resultDto = await _repository.TableNoTracking.Select(p => new PostDto
//             // {
//             //     Id = model.Id,
//             //     AuthorId = model.AuthorId,
//             //     AuthorFullName = model.Author.FullName,
//             //     CategoryId = model.CategoryId,
//             //     CategoryName = model.Category.Name,
//             //     Title = model.Title,
//             //     Description = model.Description
//             // }).SingleOrDefaultAsync(q => q.Id == dto.Id);
//             #endregion

//             #region new code1
//             // var resultDto = await _repository.TableNoTracking
//             //     .Select(p => Mapper.Map<PostDto>(p))
//             //     .SingleOrDefaultAsync(q => q.Id == dto.Id);
//             #endregion

//             var resultDto = await _repository.TableNoTracking.ProjectTo<PostDto>()
//                 .SingleOrDefaultAsync(q => q.Id == dto.Id, cancellationToken);

//             return resultDto;
//         }

//         [HttpDelete("{id:guid}")]
//         public async Task<ApiResult> Delete(Guid id, CancellationToken cancellationToken)
//         {
//             var model = await _repository.GetByIdAsync(cancellationToken, id);
//             await _repository.DeleteAsync(model, cancellationToken);

//             return Ok();
//         }



//     }
// }