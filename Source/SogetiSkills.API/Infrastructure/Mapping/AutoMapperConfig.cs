using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SogetiSkills.API.Infrastructure.Mapping
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.CreateMap<Models.SkillCategory, Contracts.DataContracts.SkillCategory>();
            Mapper.CreateMap<Models.Skill, Contracts.DataContracts.Skill>();

#if DEBUG
            //Mapper.AssertConfigurationIsValid();
#endif
        }
    }
}