using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Alsahab.Setting.Data.Contracts;
using Alsahab.Setting.Entities;
using Alsahab.Setting.WebFramework.Api;
using Alsahab.Setting.WebFramework.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Alsahab.Setting.WebFramework.Api
{
    // [ApiController]
    // [AllowAnonymous]
    // [ApiResultFilter]
    // [Route("api/v{version:apiVersion}/[controller]")] //api/v1/[controller]
    [ApiVersion("1")]
    public class CrudController<TDto, TEntity, TKey> : BaseController//ControllerBase
        where TDto : BaseDto<TDto, TEntity, TKey>, new()
        where TEntity : BaseEntity<TKey>, new()
    {
        private readonly IRepository<TEntity> _repository;
        public CrudController(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public virtual async Task<ActionResult<List<TDto>>> Get(CancellationToken cancellationToken)
        {
            var entityList = _repository.TableNoTracking;
            var dtoList = await entityList.ProjectTo<TDto>()
                .ToListAsync(cancellationToken);
            return Ok(dtoList);
        }

        [HttpGet("{id:guid}")]
        public virtual async Task<ActionResult<TDto>> Get(Guid id, CancellationToken cancellationToken)
        {
            var dto = await _repository.TableNoTracking.ProjectTo<TDto>().SingleOrDefaultAsync(s=>s.Id.Equals(id), cancellationToken);
            if (dto == null)
                return NotFound();
            return dto;
        }
        #region mapping examples
        //var postDto = new PostDto();
        //Create: (dto => new entity)
        //var post = postDto.ToEntity();
        //Update: (dto => exist entity)
        //var post = postDto.ToEntity(entity);
        //GetById: (entity => dto)            
        //var newDto = PostDto.FromEntity(entity); (Note: mehod is static (PostDto not postDto))
        #endregion
        [HttpPost]
        public virtual async Task<ApiResult<TDto>> Create(TDto dto, CancellationToken cancellationToken)
        {
            var entity = dto.ToEntity();
            await _repository.AddAsync(entity, cancellationToken);
            var resultDto = await _repository.TableNoTracking.ProjectTo<TDto>()
                .SingleOrDefaultAsync(s => s.Id.Equals(entity.Id), cancellationToken);
            return resultDto;
        }

        [HttpPut]
        public virtual async Task<ApiResult<TDto>> Update(TDto dto, CancellationToken cancellationToken)
        {
            var model = await _repository.GetByIdAsync(cancellationToken, dto.Id);
            if (model == null)
                return NotFound();
            model = dto.ToEntity(model);
            await _repository.UpdateAsync(model, cancellationToken);
            var resultDto = await _repository.TableNoTracking.ProjectTo<TDto>()
                .SingleOrDefaultAsync(q => q.Id.Equals(dto.Id), cancellationToken);

            return resultDto;
        }

        [HttpDelete("{id:guid}")]
        public virtual async Task<ApiResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var model = await _repository.GetByIdAsync(cancellationToken, id);
            await _repository.DeleteAsync(model, cancellationToken);
            return Ok();
        }
    }

    public class CrudController<TDto, TEntity> : CrudController<TDto, TEntity, int>
        where TDto : BaseDto<TDto, TEntity, int>, new()
        where TEntity : BaseEntity<int>, new()
    {
        public CrudController(IRepository<TEntity> repository) : base(repository)
        {
        }
    }
}