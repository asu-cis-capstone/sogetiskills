using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using StructureMap.Graph;
using SogetiSkills.UI.SogetiSkillsService;

namespace SogetiSkills.UI.Infrastructure.DependencyResolution
{
    public static class IoC
    {
        public static IContainer CreateContainer()
        {
            IContainer container = new Container(config =>
            {
                config.Scan(
                    scan =>
                    {
                        scan.TheCallingAssembly();
                        scan.WithDefaultConventions();
                        scan.With(new ControllerConvention());
                    });
                config.For<ISogetiSkillsService>().Use(() => new SogetiSkillsServiceClient());
            });
#if DEBUG
            container.AssertConfigurationIsValid();
#endif
            return container;
        }
    }
}