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
using Alsahab.Setting.MyAPI;
using Alsahab.Setting.Common.Exceptions;

namespace Alsahab.Setting.WebFramework.Api
{
    ///
    /// کنترلر پایه که می‌تواند توسط همه کنترلرها ارث‌بری شود
    /// 
    [ApiController]
    [ApiResultFilter]
    [AllowAnonymous]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")] // api/v1/post
    public class CrudController<TEntity, TDto, TFilteDto> : ControllerBase
        where TEntity : BaseEntity<TEntity, TDto, long>, IEntity
        where TDto : BaseDTO//class
        where TFilteDto : TDto
        // where TEntity : class, IEntity
        // where TDto : BaseDTO
        // where TFilteDto : TDto
    {
        private readonly IBaseBL<TEntity, TDto, TFilteDto> _TBL;

        /// <summary>
        /// تابع سازنده کنترلر پایه
        /// </summary>
        /// <param name="tBL"></param>
        public CrudController(IBaseBL<TEntity, TDto, TFilteDto> tBL)
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

        [Route("Create")]
        [HttpPost]
        public virtual async Task<ApiResult<TDto>> Create(BaseRequest<TDto> request, CancellationToken cancellationToken)//TDto dto, CancellationToken cancellationToken)
        {
            if (request.ActionType != Alsahab.Common.ActionType.Insert)
                throw new AppException(Common.Api.ApiResultStatusCode.BadRequest, "ActionType of Request is not valid");
            // var entity = dto.ToEntity();
            var resultDto = await _TBL.InsertAsync(request.RequestDto, cancellationToken);
            // var resultDto = await _repository.TableNoTracking.ProjectTo<TDto>()
            //     .SingleOrDefaultAsync(s => s.Id.Equals(entity.Id), cancellationToken);
            return Ok(resultDto);
        }

        [Route("Get")]
        [HttpPost]
        public virtual async Task<ApiResult<IList<TDto>>> Get(BaseRequest<TDto, TFilteDto> request, CancellationToken cancellationToken)
        {
            if (request.ActionType != Alsahab.Common.ActionType.Select)
                throw new BadRequestException("ActionType of Request is not valid");
            IList<TDto> result;
            if (request.RequestFilterDto == null)
                result = await _TBL.GetAllAsync(cancellationToken);
            else
                result = await _TBL.GetAsync(request.RequestFilterDto, cancellationToken);
            return Ok(result);
        }

        [Route("GetByFilter")]
        [HttpPost]
        public virtual async Task<ApiResult<IList<TDto>>> Get(BaseRequest<TFilteDto> request, CancellationToken cancellationToken)
        {
            if (request.ActionType != Alsahab.Common.ActionType.Select)
                throw new BadRequestException("ActionType of Request is not valid");
            IList<TDto> result;
            if (request.RequestFilterDto == null)
                result = await _TBL.GetAllAsync(cancellationToken);
            else
                result = await _TBL.GetAsync(request.RequestFilterDto, cancellationToken);
            return Ok(result);
        }

        [Route("Update")]
        // [HttpPut]
        [HttpPost]
        public virtual async Task<ApiResult<TDto>> Update(BaseRequest<TDto> request, CancellationToken cancellationToken)//TDto dto, CancellationToken cancellationToken)
        {
            if (request.ActionType != Alsahab.Common.ActionType.Update)
                throw new BadRequestException("ActionType of Request is not valid");
            return await _TBL.UpdateAsync(request.RequestDto, cancellationToken);
        }

        [Route("Delete")]
        // [HttpDelete]
        [HttpPost]
        public virtual async Task<ApiResult<TDto>> Delete(BaseRequest<TDto> request, CancellationToken cancellationToken)//TDto dto, CancellationToken cancellationToken)
        {
            if (request.ActionType != Alsahab.Common.ActionType.Delete)
                throw new BadRequestException("ActionType of Request is not valid");
            TDto data = Activator.CreateInstance(typeof(TDto), new object[] { null, null }) as TDto;
            if (request.RequestID > 0)
                data.ID = request.RequestID;
            else 
                data = request.RequestDto;
            data.IsDeleted = true;
            return await _TBL.UpdateAsync(request.RequestDto, cancellationToken);
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