using StructureMap.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap.Web;
using StructureMap.TypeRules;

namespace SogetiSkills.UI.Infrastructure.DependencyResolution
{
    public class ValidatorConvention : IRegistrationConvention
    {
        public void Process(Type type, StructureMap.Configuration.DSL.Registry registry)
        {
            if (type.CanBeCastTo<FluentValidation.IValidator>())
            {
                registry.For(type).HttpContextScoped().Use(type);
            }
        }
    }
}