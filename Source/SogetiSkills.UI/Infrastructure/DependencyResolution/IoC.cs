using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using StructureMap.Graph;
using SogetiSkills.Core.Models;
using StructureMap.Web;
using FluentValidation.Mvc;
using SogetiSkills.UI.Controllers;
using SogetiSkills.Core.Managers;

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
                        scan.AssemblyContainingType<UserManager>();
                        scan.WithDefaultConventions();
                        scan.With(new ControllerConvention());
                    });
                foreach(var type in FluentValidation.AssemblyScanner.FindValidatorsInAssemblyContaining<HomeController>())
                {
                    config.For(type.InterfaceType).Use(type.ValidatorType);
                }
            });
            return container;
        }
    }
}