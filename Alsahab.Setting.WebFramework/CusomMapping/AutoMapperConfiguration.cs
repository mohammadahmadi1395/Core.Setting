using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Alsahab.Setting.Entities;
using Alsahab.Setting.Data.Repositories;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Entities.Models;

namespace Alsahab.Setting.WebFramework.CustomMapping
{
    public static class AutoMapperConfiguration
    {
        public static void InitializeAutoMapper()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                // config.CreateMap<Post, PostDto>()
                //     .ReverseMap()
                //     //دو خط زیر برای این هستند که هنگام تبدیل 
                //     // PostDto to Post
                //     // navigation properties
                //     // تبدیل نشود تا به مشکلی برخورد نکنیم
                //     .ForMember(p => p.Author, opt => opt.Ignore())
                //     .ForMember(p => p.Category, opt => opt.Ignore());

                // config.AddCustomMappingProfile();
                config.CreateMap<Branch, BranchDTO>().ReverseMap()
                    .ForMember(p=>p.BranchAddress, opt=>opt.Ignore())
                    .ForMember(p=>p.BranchRegionWork, opt=>opt.Ignore());
            });

            //Compile Mapping after configuration to boost map speed
            AutoMapper.Mapper.Configuration.CompileMappings();
        }

        public static void AddCustomMappingProfile(this IMapperConfigurationExpression config)
        {
            //منظور از لایه 
            // EntryAssembly
            // همان لایه 
            // Api
            // است
            var entityAssembly = typeof(BaseEntity).Assembly;
            var dataAssembly = typeof(BaseDL<,>).Assembly;
            // config.AddCustomMappingProfile(Assembly.GetEntryAssembly());
            config.AddCustomMappingProfile(entityAssembly, dataAssembly);
        }

        public static void AddCustomMappingProfile(this IMapperConfigurationExpression config, params Assembly[] assemblies)
        {
            var allTypes = assemblies.SelectMany(a => a.ExportedTypes);

            var list = allTypes.Where(type => type.IsClass && !type.IsAbstract &&
                type.GetInterfaces().Contains(typeof(IHaveCustomMapping)))
                .Select(type => (IHaveCustomMapping)Activator.CreateInstance(type));

            config.AddProfile(new CustomMappingProfile(list));
        }
    }
}
