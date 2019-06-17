// using System;
// using AutoMapper;
// using Alsahab.Setting.Entities;
// using Alsahab.Setting.WebFramework.CustomMapping;

// namespace Alsahab.Setting.DTO
// {
//     public class PostCustomMapping : IHaveCustomMapping
//     {
//         public void CreateMappings(Profile profile)
//         {
//             profile.CreateMap<Post, PostDto>().ReverseMap()
//                 .ForMember(p => p.Author, opt => opt.Ignore())
//                 .ForMember(p => p.Category, opt => opt.Ignore());
//         }
//     }

//     public class CategoryCustomMapping : IHaveCustomMapping
//     {
//         public void CreateMappings(Profile profile)
//         {
//             profile.CreateMap<Category, CategoryDto>().ReverseMap();
//         }
//     }

//     public class UserCustionMapping : IHaveCustomMapping
//     {
//         public void CreateMappings(Profile profile)
//         {
//             profile.CreateMap<User, UserDto>().ReverseMap();
//         }
//     }
// }