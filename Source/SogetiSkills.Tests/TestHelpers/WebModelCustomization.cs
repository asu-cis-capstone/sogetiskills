using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SogetiSkills.Tests.TestHelpers
{
    public class WebModelCustomization : CompositeCustomization
    {
        internal WebModelCustomization()
            : base(
                new MvcCustomization(),
                new AutoMoqCustomization())
        {
        }

        private class MvcCustomization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                fixture.Customize<ControllerContext>(c => c
                    .Without(x => x.DisplayMode));
            }
        }
    }
}
