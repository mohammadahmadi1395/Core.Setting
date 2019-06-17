// using System;
// using AutoMapper;

// namespace Alsahab.Setting.WebFramework.CustomMapping
// {
//     public class HaveCustomMapping<TDto, TEntity> : IHaveCustomMapping<TDto, TEntity>
//     where TDto : class
//     where TEntity : class
//     {
//         public void CreateMappings(Profile profile)
//         {
//             var mappingExpression = profile.CreateMap<TDto, TEntity>();
//             var dtoType = typeof(TDto);
//             var entityType = typeof(TEntity);
//             //Ignore any property of source (like Post.Author) that does not contain in destination
//             foreach (var property in entityType.GetProperties())
//             {
//                 if (dtoType.GetProperty(property.Name) == null)
//                     mappingExpression.ForMember(property.Name, opt => opt.Ignore());
//             }
//             CustomMappings(mappingExpression.ReverseMap());
//         }

//         public virtual void CustomMappings(IMappingExpression<TEntity, TDto> mapping)
//         {
//         }
//     }
// }