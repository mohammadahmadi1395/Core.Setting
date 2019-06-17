// using System;
// using Alsahab.Setting.Entities;
// using Alsahab.Setting.WebFramework.Api;

// namespace Alsahab.Setting.DTO
// {
//     public class PostDto : BaseDto<PostDto, Post, Guid>
//     {
//         // public Guid Id { get; set; }
//         public string Title { get; set; }
//         public string Description { get; set; }
//         public int CategoryId { get; set; }
//         public int AuthorId { get; set; }
//         public string FullTitle { get; set; } // => FullTitle = "Title (Category.Name)"

//         public string CategoryName { get; set; }
//         public string AuthorFullName { get; set; }

//         public override void CustomMappings(AutoMapper.IMappingExpression<Post, PostDto> mapping)
//         {
//             mapping.ForMember(
//                 dest => dest.FullTitle,
//                 config => config.MapFrom(src => $"{src.Title} ({src.Category.Name})")
//             );
//         }
//     }
// }using System;
// using Alsahab.Setting.Entities;
// using Alsahab.Setting.WebFramework.Api;

// namespace Alsahab.Setting.DTO
// {
//     public class PostDto : BaseDto<PostDto, Post, Guid>
//     {
//         // public Guid Id { get; set; }
//         public string Title { get; set; }
//         public string Description { get; set; }
//         public int CategoryId { get; set; }
//         public int AuthorId { get; set; }
//         public string FullTitle { get; set; } // => FullTitle = "Title (Category.Name)"

//         public string CategoryName { get; set; }
//         public string AuthorFullName { get; set; }

//         public override void CustomMappings(AutoMapper.IMappingExpression<Post, PostDto> mapping)
//         {
//             mapping.ForMember(
//                 dest => dest.FullTitle,
//                 config => config.MapFrom(src => $"{src.Title} ({src.Category.Name})")
//             );
//         }
//     }
// }