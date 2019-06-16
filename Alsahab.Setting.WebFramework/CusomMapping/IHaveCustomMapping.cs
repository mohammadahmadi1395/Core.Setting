using System;
using AutoMapper;

namespace Alsahab.Setting.WebFramework.CustomMapping
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile profile);
    }
}