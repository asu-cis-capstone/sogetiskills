using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap;
using StructureMap.Graph;
using StructureMap.Web;

namespace SogetiSkills.API.Infrastructure.DependencyResoluitgon
{
    public static class StructureMapConfig
    {
        public static void Configure()
        {
            ObjectFactory.Container.Configure(config =>
            {
                config.Scan(scan =>
                    {
                        scan.TheCallingAssembly();
                        scan.WithDefaultConventions();
                    });
                config.For<Models.SogetiSkillsDataContext>().Use<Models.SogetiSkillsDataContext>();
            });
        }
    }
}