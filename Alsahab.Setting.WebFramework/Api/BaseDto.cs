// using System;
// using System.ComponentModel.DataAnnotations;
// using AutoMapper;
// using Alsahab.Setting.Entities;
// using Alsahab.Setting.WebFramework.CustomMapping;

// namespace Alsahab.Setting.WebFramework.Api
// {
//     public abstract class BaseDTO<TDto, TEntity, TKey> : IHaveCustomMapping
//         where TDto : class, new()
//         where TEntity : BaseEntity<TKey>, new()
//     {
//         public TKey Id { get; set; }
//         public DateTime? CreateDate { get; set; }
//         public bool? IsDeleted { get; set; }
//         private TDto CastToDerivedClass(BaseDTO<TDto, TEntity, TKey> baseInstance)
//         {
//             return Mapper.Map<TDto>(baseInstance);
//         }
//         // یک دی تی او را به موجودیت جدید تبدیل می‌کند
//         public TEntity ToEntity()
//         {
//             return Mapper.Map<TEntity>(CastToDerivedClass(this));
//         }
//         // یک دی تی او را به موجودیت موجود تبدیل می‌کند، یعنی خروجی همان ورودی تغییریافته است
//         public TEntity ToEntity(TEntity entity)
//         {
//             return Mapper.Map(CastToDerivedClass(this), entity);
//         }

//         public static TDto FromEntity(TEntity model)
//         {
//             return Mapper.Map<TDto>(model);
//         }

//         public void CreateMappings(Profile profile)
//         {
//             var mappingExpression = profile.CreateMap<TDto, TEntity>();
//             var dtoType = typeof(TDto);
//             var entityType = typeof(TEntity);
//             //Ignore any property of source (like Post.Author) that does not contain in destination
//             foreach(var property in entityType.GetProperties())
//             {
//                 if (dtoType.GetProperty(property.Name) == null)
//                     mappingExpression.ForMember(property.Name, opt=>opt.Ignore());
//             }
//             CustomMappings(mappingExpression.ReverseMap());
//         }

//         public virtual void CustomMappings(IMappingExpression<TEntity, TDto> mapping)
//         {            
//         }
//     }

//     public abstract class BaseDTO<TDto, TEntity> : BaseDTO<TDto, TEntity, long>
//         where TDto : class, new()
//         where TEntity : BaseEntity<long>, new()
//     {

//     }
// }