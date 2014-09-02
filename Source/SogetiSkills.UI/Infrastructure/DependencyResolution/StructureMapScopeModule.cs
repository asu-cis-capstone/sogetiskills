namespace SogetiSkills.UI.Infrastructure.DependencyResolution
{
    using System.Web;

    using SogetiSkills.UI;

    using StructureMap.Web.Pipeline;

    public class StructureMapScopeModule : IHttpModule {
        #region Public Methods and Operators

        public void Dispose() {
        }

        public void Init(HttpApplication context) {
            context.BeginRequest += (sender, e) => StructuremapConfig.StructureMapDependencyScope.CreateNestedContainer();
            context.EndRequest += (sender, e) => {
                HttpContextLifecycle.DisposeAndClearAll();
                StructuremapConfig.StructureMapDependencyScope.DisposeNestedContainer();
            };
        }

        #endregion
    }
}