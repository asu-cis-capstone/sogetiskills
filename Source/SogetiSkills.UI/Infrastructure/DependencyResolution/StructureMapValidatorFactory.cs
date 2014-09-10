using FluentValidation;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SogetiSkills.UI.Infrastructure.DependencyResolution
{
    public class StructureMapValidatorFactory : FluentValidation.ValidatorFactoryBase
    {
        private readonly IContainer _container;

        public StructureMapValidatorFactory(IContainer container)
        {
            _container = container;
        }

        public override FluentValidation.IValidator CreateInstance(Type validatorType)
        {
            return _container.GetInstance(validatorType) as IValidator;
        }
    }
}