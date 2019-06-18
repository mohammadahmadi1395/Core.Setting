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
    // public class BranchController : ControllerBase // BaseController
    public class CrudController<TDto, TFilteDto> : ControllerBase
        where TDto : class
        where TFilteDto : TDto
    {
        private readonly IBaseBL<TDto, TFilteDto> _TBL;
        public CrudController(IBaseBL<TDto, TFilteDto> tBL)
        {
            _TBL = tBL;
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
        public virtual async Task<ApiResult<TDto>> Create(TDto dto, CancellationToken cancellationToken)
        {
            // var entity = dto.ToEntity();
            var resultDto = await _TBL.InsertAsync(dto, cancellationToken);
            // var resultDto = await _repository.TableNoTracking.ProjectTo<TDto>()
            //     .SingleOrDefaultAsync(s => s.Id.Equals(entity.Id), cancellationToken);
            return resultDto;
        }

        [Route("Get")]
        [HttpPost]
        public virtual async Task<ApiResult<IList<TDto>>> Get(TFilteDto filter, CancellationToken cancellationToken)
        {
            var result = await _TBL.Get(filter, cancellationToken);
            return Ok(result);
        }

        [HttpPut]
        public virtual async Task<ApiResult<TDto>> Update(TDto dto, CancellationToken cancellationToken)
        {
            return await _TBL.UpdateAsync(dto, cancellationToken);
        }

        [HttpDelete]
        public virtual async Task<ApiResult<TDto>> Delete(TDto dto, CancellationToken cancellationToken)
        {
            return await _TBL.DeleteAsync(dto, cancellationToken);
        }

        // [HttpDelete("{id:guid}")]
        // public virtual async Task<ApiResult> Delete(Guid id, CancellationToken cancellationToken)
        // {
        //     var model = await _repository.GetByIdAsync(cancellationToken, id);
        //     await _repository.DeleteAsync(model, cancellationToken);
        //     return Ok();
        // }
    }
}