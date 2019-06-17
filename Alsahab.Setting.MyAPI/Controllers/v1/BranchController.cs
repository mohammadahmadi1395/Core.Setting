using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Alsahab.Setting.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alsahab.Setting.BL;
using Alsahab.Setting.DTO;
using Alsahab.Setting.WebFramework.Filter;
using Microsoft.AspNetCore.Authorization;

namespace Alsahab.Setting.WebFramework.Api
{
    [ApiController]
    [ApiResultFilter]
    [AllowAnonymous]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")] // api/v1/post
    public class BranchController : ControllerBase // BaseController
    {
        private readonly IBranchBL _BranchBL;
        public BranchController(IBranchBL branchBL)
        {
            _BranchBL = branchBL;
        }

        // [HttpGet]
        // public virtual async Task<ActionResult<List<TDto>>> Get(CancellationToken cancellationToken)
        // {
        //     var entityList = _repository.TableNoTracking;
        //     var dtoList = await entityList.ProjectTo<TDto>()
        //         .ToListAsync(cancellationToken);
        //     return Ok(dtoList);
        // }

        // [HttpGet("{id:guid}")]
        // public virtual async Task<ActionResult<TDto>> Get(Guid id, CancellationToken cancellationToken)
        // {
        //     var dto = await _repository.TableNoTracking.ProjectTo<TDto>().SingleOrDefaultAsync(s=>s.Id.Equals(id), cancellationToken);
        //     if (dto == null)
        //         return NotFound();
        //     return dto;
        // }
        #region mapping examples
        //var postDto = new PostDto();
        //Create: (dto => new entity)
        //var post = postDto.ToEntity();
        //Update: (dto => exist entity)
        //var post = postDto.ToEntity(entity);
        //GetById: (entity => dto)            
        //var newDto = PostDto.FromEntity(entity); (Note: mehod is static (PostDto not postDto))
        #endregion

        // private TDto CastToDerivedClass(BaseDTO baseInstance)
        // {
        //     return Mapper.Map<TDto>(baseInstance);
        // }


        // // یک دی تی او را به موجودیت جدید تبدیل می‌کند
        // public TEntity ToEntity()
        // {
        //     return Mapper.Map<TEntity>(CastToDerivedClass(this));
        // }

        // // یک دی تی او را به موجودیت موجود تبدیل می‌کند، یعنی خروجی همان ورودی تغییریافته است
        // public TEntity ToEntity(TEntity entity)
        // {
        //     return Mapper.Map(CastToDerivedClass(this), entity);
        // }

        [HttpPost]
        public async Task<ApiResult<BranchDTO>> Create(BranchDTO dto, CancellationToken cancellationToken)
        {
            // var entity = dto.ToEntity();
            var resultDto = await _BranchBL.InsertAsync(dto, cancellationToken);
            // var resultDto = await _repository.TableNoTracking.ProjectTo<TDto>()
            //     .SingleOrDefaultAsync(s => s.Id.Equals(entity.Id), cancellationToken);
            return resultDto;
        }

        // [HttpPut]
        // public virtual async Task<ApiResult<TDto>> Update(TDto dto, CancellationToken cancellationToken)
        // {
        //     var model = await _repository.GetByIdAsync(cancellationToken, dto.Id);
        //     if (model == null)
        //         return NotFound();
        //     model = dto.ToEntity(model);
        //     await _repository.UpdateAsync(model, cancellationToken);
        //     var resultDto = await _repository.TableNoTracking.ProjectTo<TDto>()
        //         .SingleOrDefaultAsync(q => q.Id.Equals(dto.Id), cancellationToken);

        //     return resultDto;
        // }

        // [HttpDelete("{id:guid}")]
        // public virtual async Task<ApiResult> Delete(Guid id, CancellationToken cancellationToken)
        // {
        //     var model = await _repository.GetByIdAsync(cancellationToken, id);
        //     await _repository.DeleteAsync(model, cancellationToken);
        //     return Ok();
        // }
    }
}