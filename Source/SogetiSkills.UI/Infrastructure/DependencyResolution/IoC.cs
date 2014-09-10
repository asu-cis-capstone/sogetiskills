using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using StructureMap.Graph;
using SogetiSkills.Models;
using StructureMap.Web;
using FluentValidation.Mvc;
using SogetiSkills.UI.Controllers;

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
                        scan.AssemblyContainingType<SogetiSkillsDataContext>();
                        scan.WithDefaultConventions();
                        scan.With(new ControllerConvention());
                    });
                config.For<SogetiSkillsDataContext>().HttpContextScoped().Use<SogetiSkillsDataContext>();
                foreach(var type in FluentValidation.AssemblyScanner.FindValidatorsInAssemblyContaining<HomeController>())
                {
                    config.For(type.InterfaceType).Use(type.ValidatorType);
                }
            });
            return container;
        }
    }
}