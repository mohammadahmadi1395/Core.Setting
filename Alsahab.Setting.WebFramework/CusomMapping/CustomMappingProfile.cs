using System;
using System.Collections.Generic;
using Alsahab.Setting.Entities.Common;
using AutoMapper;

namespace Alsahab.Setting.WebFramework.CustomMapping
{
    public class CustomMappingProfile : Profile
    {
        public CustomMappingProfile(IEnumerable<IHaveCustomMapping> haveCustomeMappings)        
        {
            foreach (var item in haveCustomeMappings)            
                item.CreateMappings(this);
        }
    }
}