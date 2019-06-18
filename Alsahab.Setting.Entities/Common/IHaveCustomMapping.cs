using System;
using AutoMapper;

namespace Alsahab.Setting.Entities.Common//.CustomMapping
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile profile);
    }
}