// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StructuremapMvc.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using SogetiSkills.UI;
using WebActivatorEx;
using System.Web.Mvc;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using SogetiSkills.UI.Infrastructure.DependencyResolution;
using StructureMap;
using StructureMap.Graph;
using FluentValidation.Mvc;

[assembly: PreApplicationStartMethod(typeof(StructuremapConfig), "Start")]
[assembly: ApplicationShutdownMethod(typeof(StructuremapConfig), "End")]

namespace SogetiSkills.UI 
{
	public static class StructuremapConfig
    {
        public static StructureMapDependencyScope StructureMapDependencyScope { get; set; }

		public static void End() 
        {
            StructureMapDependencyScope.Dispose();
        }
		
        public static void Start() 
        {
            IContainer container = IoC.CreateContainer();
            StructureMapDependencyScope = new StructureMapDependencyScope(container);
            DependencyResolver.SetResolver(StructureMapDependencyScope);
            DynamicModuleUtility.RegisterModule(typeof(StructureMapScopeModule));
            FluentValidationModelValidatorProvider.Configure(config =>
            {
                config.ValidatorFactory = new StructureMapValidatorFactory(container);
            });
        }
    }
}